using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.ElasticSearch
{
   public interface IElasticSearchRepository<T> where T : class, new()
    {
        Task<IndexResponse> CreateAsync(T input);
        Task<BulkResponse> CreateListAsync(List<T> input);
        Task<MultiGetResponse> MultiGetAsync();
    }
}
