using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.ElasticSearch;
using YC.ElasticSearch.Models;
using YC.FreeSqlFrameWork;
using YC.Micro.BookWebService;

namespace YC.Micro.BookWebService
{
    [Authorize]
    public class BookService : IBookService.IBookServiceBase
    {
        private IElasticSearchRepository<Book> _elasticSearchRepository;
        private readonly IMapper _mapper;

        public BookService(IElasticSearchRepository<Book> elasticSearchRepository, IMapper mapper)
        {
            _elasticSearchRepository = elasticSearchRepository;
            _mapper = mapper;
        }

        public override async Task<BookDtoList> GetBookList(BookFormRequest request, ServerCallContext context)
        {
            Func<QueryContainerDescriptor<Book>, QueryContainer> query = null;
            //全字匹配+ 分词查询 double 不能直接用string 丢进去查询
            query = q => q.Term(t => t.BookName, request.QueryFilterString) ||
                  //q.Term(t => t.Price, "23") ||
                  q.Match(mq => mq.Field(f => f.BookContent).Query(request.QueryFilterString).Operator(Operator.And)) ||
                   q.Match(mq => mq.Field(f => f.Auther).Query(request.QueryFilterString).Operator(Operator.And));

            //多项高亮结果显示
            Func<HighlightDescriptor<Book>, IHighlight> highlight = h => h.PreTags("<b class='key' style='color:red'>")
              .PostTags("</b>").Fields(f => f.Field(ff => ff.BookName), f => f.Field(ff => ff.BookContent),
              f => f.Field(ff => ff.Auther));
            var esPageResult = await _elasticSearchRepository.GetPageByQueryAsync(query, request.CurrentPage, request.PageSize, null, highlight);
            List<Book> list = esPageResult.List.ToList();
            long total = esPageResult.Total >= 10000 ? 10000 : esPageResult.Total;//查询总数,如果大于10000 默认显示10000。这是es深度分页需要处理，或使用searchAfter

            #region 高亮数据处理

            if (list.Count > 0)
            {
                list.ForEach(
                    x =>
                    {
                        var tempHighlight = esPageResult.Hits.Where(t => t.Id.Contains(x.Id.ToString())).FirstOrDefault().Highlight;
                        IReadOnlyCollection<string> bookNameHighlightList;
                        tempHighlight.TryGetValue("bookName", out bookNameHighlightList);
                        if (bookNameHighlightList?.Count > 0)
                        {
                            x.BookName = "";//获取值不为空，那么原有的内容用新的替代
                            bookNameHighlightList.ToList().ForEach(v =>
                            {
                                x.BookName += v;
                            });
                        }

                        IReadOnlyCollection<string> bookContentHighlightList;
                        tempHighlight.TryGetValue("bookContent", out bookContentHighlightList);
                        if (bookContentHighlightList?.Count > 0)
                        {
                            x.BookContent = "";//获取值不为空，那么原有的内容用新的替代
                            bookContentHighlightList.ToList().ForEach(v =>
                            {
                                x.BookContent += v;
                            });
                        }

                        IReadOnlyCollection<string> autherHighlightList;
                        tempHighlight.TryGetValue("auther", out autherHighlightList);
                        if (autherHighlightList?.Count > 0)
                        {
                            x.Auther = "";//获取值不为空，那么原有的内容用新的替代
                            autherHighlightList.ToList().ForEach(v =>
                            {
                                x.Auther += v;
                            });
                        }
                    }
                    );
            }

            #endregion 高亮数据处理

            var result = _mapper.Map<BookDtoList>(list);

            return result;
        }
    }
}