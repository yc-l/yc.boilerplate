using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Core;
using YC.ElasticSearch.Domain;

namespace YC.ElasticSearch
{
    public class ElasticSearchRepository<T> : IElasticSearchRepository<T> where T : class, new()
    {
        public IElasticSearchDbContext _elasticSearchDbContext;
        public ITenant _tenant;
        public string MappingName
        {

            get
            {
                return this.MappingIndexName(typeof(T));
            }
        }

        public IElasticSearchDbContext ElasticSearchDbContext
        {

            get
            {
                return this._elasticSearchDbContext;
            }
        }

    
        public ElasticSearchRepository(IElasticSearchDbContext elasticSearchDbContext, ITenant tenant)
        {
            _elasticSearchDbContext = elasticSearchDbContext;
            _tenant = tenant;
           
        }

        //
        public string MappingIndexName(Type type)
        {

            string name = "";
            if (_tenant == null || _tenant?.TenantId == 0)
            {
                name = $"{type.Name.ToLower()}s";//如果不开启多租户，按照名称拆分
            }
            else
            {//默认开启一个集群，采用集群 多租户表名称区分
                name = $"tenant_{_tenant?.TenantId}_{type.Name.ToLower()}s";
            }

            return name;
        }



        /// <summary>
        /// 通过指定 _id 获取对应的document
        /// </summary>
        /// <param name="id">es 对应数据库中其中某一条记录的_id</param>
        /// <returns>返回_id 对应的一条记录</returns>
        public async Task<T> GetAsync(Id id)
        {

            var response = await _elasticSearchDbContext.Client.GetAsync<T>(id, idx => idx.Index(this.MappingName));
            var source = response.Source; // the original document
            return source;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetByQueryAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query,
            Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector = null)
        {

            var result = await _elasticSearchDbContext.Client.SearchAsync<T>(s => s
                             .Index(this.MappingName)
                            .Query(query).Sort(selector));
            return result.Documents;
            //q => q.Match(mq => mq.Field(f => f.BookName).Query("万族123").Operator(Operator.And)
        }

        /// <summary>
        /// 通过Id集合查询列表
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetByQueryIdsAsync(List<string> ids)
        {
            List<T> list = new List<T>();
            var response = await _elasticSearchDbContext.Client.GetManyAsync<T>(ids, this.MappingName);

            foreach (var multiGetHit in response)
            {
                if (multiGetHit.Found)
                {
                    list.Add(multiGetHit.Source);
                }
            }
            return list;
        }

        /// <summary>
        /// 查询所有，默认返回10条数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync(
            Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector = null)
        {

            var result = await _elasticSearchDbContext.Client.SearchAsync<T>(s => s
                             .Index(this.MappingName).TrackTotalHits(true)
                            .Query(q => q.MatchAll()).Sort(selector));
          
            return result.Documents;
           
        }


        /// <summary>
        /// 查询index 的总数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync()
        {

            var result = await _elasticSearchDbContext.Client.CountAsync<T>(s => s
                              .Index(this.MappingName));

            return result.Count;
            
        }
       



        /// <summary>
        /// 分页查询,es 默认1w条，超过了有深度查询问题，超过要使用search after等操作
        /// </summary>
        /// <param name="query">查询 lambda </param>
        /// <param name="currentPage">当前页面</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        public async Task<EsPageResult<T>> GetPageByQueryAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, int currentPage, int pageSize = 10,
             Func<SortDescriptor<T>, IPromise<IList<ISort>>> sort = null, Func<HighlightDescriptor<T>, IHighlight> highlight=null)
        {

            var result = await _elasticSearchDbContext.Client.SearchAsync<T>(s => s
                             .Index(this.MappingName).TrackTotalHits(true) //or specify index via settings.DefaultIndex("mytweetindex");
                            .From((currentPage - 1) * pageSize)
                            .Size(pageSize)
                            .Query(query).Sort(sort).Highlight(highlight));
            EsPageResult<T> pageResult = new EsPageResult<T>();
            pageResult.Total = result.Total;
            pageResult.List = result.Documents.ToList();
            pageResult.Hits = result.Hits;
            return pageResult;
            //q => q.Match(mq => mq.Field(f => f.BookName).Query("万族123").Operator(Operator.And)
        }
       
        /// <summary>
        /// 创建Index，在第一次时候，需要创建数据库，必要的字段和model 需要映射，这个使用要先调用此方法
        /// 否则直接插入，特定的数据类型都会变为默认的
        /// </summary>
        /// <returns></returns>
        public async Task<CreateIndexResponse> CreateIndexAsync()
        {
            //
            var createIndexResponse = await _elasticSearchDbContext.Client.Indices.CreateAsync(this.MappingName, c => c
                .Map<T>(m => m.AutoMap())
            );
            return createIndexResponse;
    }

        /// <summary>
        /// 新增，model 需要有一个id字段，采用我们自定义
        /// es 得到  {"_id" : "61552023fea479424cd276df"}= 我们业务id {"id" : "61552023fea479424cd276df"}
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OprateResult<Id>> CreateAsync(T input,bool isCeated=false)
        {
            if (isCeated) {
              await  CreateIndexAsync();
            }

            var createIndexResponse = _elasticSearchDbContext.Client.Indices.Create(this.MappingName, c => c
               .Map<T>(m => m.AutoMap())
           );
            var prId = input.GetType().GetProperty("Id");
            Id id = new Id(prId.GetValue(input).ToString());
            var result = await _elasticSearchDbContext.Client.DocumentExistsAsync<T>(id, idx => idx.Index(this.MappingName));
            if (result.Exists)
            {
                return OprateResult<Id>.NoOk("数据已经存在！");
            }
            var response = await _elasticSearchDbContext.Client.IndexAsync(input, idx => idx.Index(this.MappingName));
            if (response.Result == Result.Created)
            {
                return OprateResult<Id>.Ok("添加成功！", new Id(response.Id));
            }
            else
            {

                return OprateResult<Id>.NoOk("添加失败！", new Id(response.Id));
            }


        }

        /// <summary>
        /// model 需要有一个id字段，采用我们自定义,如果_id 已经存在，就会覆盖,并修改
        /// 默认_id =id
        /// </summary>
        /// <param name="input">新增数据集合</param>
        /// <returns></returns>
        public async Task<BulkResponse> CreateListAsync(List<T> input, bool isCeated = false)
        {
            if (isCeated)
            {
                await CreateIndexAsync();
            }
            var response = await _elasticSearchDbContext.Client.IndexManyAsync(input, this.MappingName);
            return response;
        }

        /// <summary>
        /// 批量添加，需要检验执行的数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OprateResult<int>> CreateListByExistValidateAsync(List<T> input)
        {

            int existCount = 0;
            foreach (var i in input)
            {
                var prId = i.GetType().GetProperty("Id");
                Id id = new Id(prId.GetValue(i).ToString());
                var result = await _elasticSearchDbContext.Client.DocumentExistsAsync<T>(id, idx => idx.Index(this.MappingName));
                if (result.Exists)
                {
                    existCount++;
                }
            }
            if (existCount > 0)
            {

                return OprateResult<int>.NoOk("数据已经存在！", existCount);
            }
            var response = await _elasticSearchDbContext.Client.IndexManyAsync(input, this.MappingName);
            var itemResult = response.Items.Where(x => x.Result == Nest.Result.Created.ToString().ToLower()).ToList();
            if (itemResult.Count() == input.Count())
            {
                return OprateResult<int>.Ok("添加成功！", itemResult.Count());
            }
            else
            {
                var errorCount = response.Items.Where(x => x.Result != Nest.Result.Created.ToString().ToLower()).Count();
                return OprateResult<int>.NoOk("添加失败！", errorCount);
            }

        }

        /// <summary>
        /// update 操作
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<OprateResult<T>> UpdateAsync(Id id, T input)
        {
            var result = await _elasticSearchDbContext.Client.DocumentExistsAsync<T>(id, idx => idx.Index(this.MappingName));
            if (result.Exists)//存在才修改
            {
                var response = await _elasticSearchDbContext.Client.IndexAsync<T>(input, idx => idx.Index(this.MappingName)
                );
                if (response.Result == Nest.Result.Updated)
                {
                    return OprateResult<T>.Ok("修改成功！", input);
                }
                else
                {
                    return OprateResult<T>.NoOk("修改失败！当前操作类别为：" + response.Result.ToString());
                }
            }
            else//不存在
            {

                return OprateResult<T>.NoOk("不存在指定的数据！");
            }


        }


        /// <summary>
        /// 删除指定id数据
        /// </summary>
        /// <returns></returns>
        public async Task<DeleteResponse> DeleteByIdAsync(Id id)
        {
            var name = MappingIndexName(typeof(T));
            var result = await _elasticSearchDbContext.Client.DeleteAsync<T>(id, x => x.Index(name));

            
            return result;
        }
        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<DeleteByQueryResponse> DeleteAllAsync()
        {
            var name = MappingIndexName(typeof(T));
            var result = await _elasticSearchDbContext.Client.DeleteByQueryAsync<T>(x => x.Index(name).IgnoreUnavailable()
              .Query(t => t.MatchAll()));
            return result;
        }

        /// <summary>
        /// 按照条件删除
        /// </summary>
        /// <returns></returns>
        public async Task<DeleteByQueryResponse> DeleteByQueryAsync(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector)
        {
            var name = MappingIndexName(typeof(T));
            var result = await _elasticSearchDbContext.Client.DeleteByQueryAsync<T>(selector);
            return result;
        
        }


        #region 高级方案

        /// <summary>
        /// 聚合查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<Tuple<IEnumerable<T>, AggregateDictionary>> GetByQueryAggregationsAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query,
            Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector)
        {
            Tuple<IEnumerable<T>, AverageAggregation> tuple;
            var result = await _elasticSearchDbContext.Client.SearchAsync<T>(s => s
                             .Index(this.MappingName)
                            .Query(query).Aggregations(aggregationsSelector));

            return new Tuple<IEnumerable<T>, AggregateDictionary>(result.Documents, result.Aggregations);
         
        }

        /// <summary>
        /// 深度分页查询方案 searchAfter 只有每一页数据数量和下一页
        /// </summary>
        /// <param name="query"></param>
        /// <param name="selector"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchAfter">上一个查询获取的</param>
        /// <returns></returns>
        public async Task<SearchAfterResult<T>> GetPageByQuerySearchAfterAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, Func<SortDescriptor<T>, IPromise<IList<ISort>>> sort,
            int pageSize = 10, IEnumerable<object> searchAfter = null, Func<HighlightDescriptor<T>, IHighlight> highlight = null)
        {

            var result = await _elasticSearchDbContext.Client.SearchAsync<T>(s => s
                             .Index(this.MappingName).TrackTotalHits(true) //or specify index via settings.DefaultIndex("mytweetindex");
                            .Size(pageSize)
                            .Query(query).Sort(sort).Highlight(highlight).SearchAfter(searchAfter));
            SearchAfterResult<T> pageResult = new SearchAfterResult<T>();
            pageResult.SearchAfter = result.Hits.LastOrDefault().Sorts;//获取最后一页的标识
            pageResult.List = result.Documents.ToList();
            pageResult.Total = result.Total;
            return pageResult;
         
        }
       
        #endregion

    }
}
