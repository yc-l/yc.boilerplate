
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using YC.Model;
using YC.Core;
using AutoMapper;
using System.Linq.Expressions;
using YC.Core.Attribute;
using YC.Core.Domain;
using YC.Core.Autofac;
using YC.Common.ShareUtils;
using YC.Core.Cache;
using YC.ApplicationService.DefaultConfigure;
using YC.FreeSqlFrameWork;
using YC.Core.DynamicApi;
using YC.Core.DynamicApi.Attributes;
using YC.Model.SysDbEntity;
using YC.ApplicationService.Dto;
using YC.Core.Domain.Output;
using YC.ElasticSearch.Models;
using YC.ElasticSearch;
using Nest;
using YC.ApplicationService.ApplicationService.BookAppService.Dto;
using YC.ApplicationService.Base;
using System.IO;

namespace YC.ApplicationService
{
    /// <summary>
    ///  业务实现接口
    /// </summary>
    [DynamicWebApi]
    public class BookAppService : FreeSqlEntityApplicationService<Book,string>, IBookAppService, IDynamicWebApi
    {

        private IElasticSearchRepository<Book> _elasticSearchRepository;
        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public BookAppService(
        IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager, IFreeSqlRepository<Book, string> entityFreeSqlRepository, IMapper mapper, IElasticSearchRepository<Book>  elasticSearchRepository) : base(httpContextAccessor, entityFreeSqlRepository, mapper, cacheManager)
        {
            _elasticSearchRepository = elasticSearchRepository;
        }

        public async Task<FileStreamResult> GetFileAsync() {

            string filePath = System.Environment.CurrentDirectory + "//DefaultConfig.json";
            FileStreamResult fileStreamResult=
                  new FileStreamResult(new FileStream(filePath, FileMode.Open), "application/octet-stream") { FileDownloadName = "测试.json" };
            return fileStreamResult; 
        }

        /// <summary>
        /// 查查默认1页10条
        /// </summary>
        /// <returns>返回数据集合</returns>

        public async Task<ApiResult<List<BookAddOrEditDto>>> GetAllAsync()
        {
            var res = new ApiResult<List<BookAddOrEditDto>>();
            var data = await _elasticSearchRepository.GetAllAsync();

            var entityDtoList = _mapper.Map<List<BookAddOrEditDto>>(data);
            return res.Ok(entityDtoList);
        }

        /// <summary>
        /// 分页查询,高亮处理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IApiResult> GetPageBookListAsync(BookPageInput<PageInputDto> input)
        {
            Expression<Func<Book, bool>> exp = null;
            Func<QueryContainerDescriptor<Book>, QueryContainer> query = null;

            if (input.Filter != null && input.Filter?.QueryString != "")
            {
                if (input.PublishDateRange != null)
                {
                    if (!string.IsNullOrWhiteSpace(input.PublishDateRange[0]) && !string.IsNullOrWhiteSpace(input.PublishDateRange[1]))
                    {
                        var startDate = DateTime.Parse(input.PublishDateRange[0]).ToLocalTime();
                        var stopDate = DateTime.Parse(input.PublishDateRange[1]).ToLocalTime();
                        //全字匹配+ 分词查询 这种也可以
                        query = q => q.Bool(b =>
                         //should 必须要和must 一起用，must 中用and 操作
                         b.Must(m => m.Bool(mb => mb.Should(mbs => mbs.DateRange(mq =>
                             mq.Field(f => f.PublishDate).GreaterThanOrEquals(startDate).LessThan(stopDate))))
                         , m => m.Bool(mb => mb.Should(s => s.Term(t => t.BookName, input.Filter.QueryString), s => s.Term(t => t.Price, 23)
                             , s => s.Match(mq => mq.Field(f => f.BookContent).Query(input.Filter.QueryString).Operator(Operator.And))
                             , s => s.Match(mq => mq.Field(f => f.Auther).Query(input.Filter.QueryString).Operator(Operator.And))
                           ).MinimumShouldMatch(1)))
                         );
                    }
                }
                else
                {
                    //全字匹配+ 分词查询 double 不能直接用string 丢进去查询
                    query = q => q.Term(t => t.BookName, input.Filter.QueryString) ||
                           //q.Term(t => t.Price, "23") ||
                          q.Match(mq => mq.Field(f => f.BookContent).Query(input.Filter.QueryString).Operator(Operator.And)) ||
                           q.Match(mq => mq.Field(f => f.Auther).Query(input.Filter.QueryString).Operator(Operator.And));
                }

            }
            else
            {
                if (input.PublishDateRange != null)
                {
                    if (!string.IsNullOrWhiteSpace(input.PublishDateRange[0]) && !string.IsNullOrWhiteSpace(input.PublishDateRange[1]))
                    {
                        var startDate = DateTime.Parse(input.PublishDateRange[0]).ToLocalTime();
                        var stopDate = DateTime.Parse(input.PublishDateRange[1]).ToLocalTime();
                        query = q => q.DateRange(mq =>
                       mq.Field(f => f.PublishDate).GreaterThanOrEquals(startDate).LessThan(stopDate));
                    }
                }
            }
            //多项高亮结果显示
            Func<HighlightDescriptor<Book>, IHighlight> highlight = h => h.PreTags("<b class='key' style='color:red'>")
              .PostTags("</b>").Fields(f => f.Field(ff => ff.BookName), f => f.Field(ff => ff.BookContent),
              f => f.Field(ff => ff.Auther));

            var result = await _elasticSearchRepository.GetPageByQueryAsync(query, input.CurrentPage, input.PageSize, null, highlight);
            List<Book> list = result.List.ToList();
            long total = result.Total >= 10000 ? 10000 : result.Total;//查询总数,如果大于10000 默认显示10000。这是es深度分页需要处理，或使用searchAfter
            #region 高亮数据处理
            if (list.Count > 0)
            {
                list.ForEach(
                    x =>
                    {
                        var tempHighlight = result.Hits.Where(t => t.Id.Contains(x.Id.ToString())).FirstOrDefault().Highlight;
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
            #endregion
            //返回数据必须是明确实体，要不然可能存在json映射死循环
            var data = new PageOutput<BookAddOrEditDto>()
            {
                List = _mapper.Map<List<BookAddOrEditDto>>(list),
                Total = total
            };

            return ApiResult.Ok(data);
        }


    }
}
