using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Core;

namespace YC.ElasticSearch
{
    public class ElasticSearchRepository<T> : IElasticSearchRepository<T> where T:class,new()
    {
        public IElasticSearchDbContext _elasticSearchDbContext;
        public ITenant _tenant;
        public ElasticSearchRepository(IElasticSearchDbContext elasticSearchDbContext, ITenant tenant)
        {
            _elasticSearchDbContext = elasticSearchDbContext;
            _tenant= tenant;
        }

        //
        public string MappingTableName(Type type)
        {

            string name = "";
            if (_tenant==null||_tenant?.TenantId==0)
            {
                name = $"{type.Name.ToLower()}s";//如果不开启多租户，按照名称拆分
            }
            else {//默认开启一个集群，采用集群 多租户表名称区分
                name = $"tenant_{_tenant?.TenantId}_{type.Name.ToLower()}s";
            }

            return name;
        }

        public async Task<IndexResponse> CreateAsync(T input)
        {
            var name= MappingTableName(typeof(T));
            var response =await _elasticSearchDbContext.Client.IndexAsync(input, idx => idx.Index(name));
            return response;
        }

        public async Task<BulkResponse> CreateListAsync(List<T> input)
        {
            var name = MappingTableName(typeof(T));
            var response = await _elasticSearchDbContext.Client.IndexManyAsync(input, name);
            return response;
        }

        public Task<MultiGetResponse> MultiGetAsync()
        {
            var response = _elasticSearchDbContext.Client.MultiGetAsync(x=>x.Index(MappingTableName(typeof(T)))); // returns an IGetResponse mapped 1-to-1 with the Elasticsearch JSON response
           return  response;  //the original document
           
        }
    }
}
