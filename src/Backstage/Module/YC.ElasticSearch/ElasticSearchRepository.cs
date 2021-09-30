using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Core;

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
        /// 分页查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ISearchResponse<T>> GetByQueryAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query)
        {

            var result = await _elasticSearchDbContext.Client.SearchAsync<T>(s => s
                             .Index(this.MappingName)
                            .Query(query));
            return result;
            //q => q.Match(mq => mq.Field(f => f.BookName).Query("万族123").Operator(Operator.And)
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询 lambda </param>
        /// <param name="currentPage">当前页面</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        public async Task<ISearchResponse<T>> GetPageByQueryAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, int currentPage, int pageSize = 10)
        {

            var result = await _elasticSearchDbContext.Client.SearchAsync<T>(s => s
                             .Index(this.MappingName) //or specify index via settings.DefaultIndex("mytweetindex");
                            .From((currentPage - 1) * pageSize)
                            .Size(pageSize)
                            .Query(query));
            return result;
            //q => q.Match(mq => mq.Field(f => f.BookName).Query("万族123").Operator(Operator.And)
        }

        /// <summary>
        /// 新增，model 需要有一个id字段，采用我们自定义
        /// es 得到  {"_id" : "61552023fea479424cd276df"}= 我们业务id {"id" : "61552023fea479424cd276df"}
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OprateResult<Id>> CreateAsync(T input)
        {

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
        public async Task<BulkResponse> CreateListAsync(List<T> input)
        {

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


    }
}
