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
        public string MappingName { get;  }

        Task<T> GetAsync(Id id);
        Task<ISearchResponse<T>> GetByQueryAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query);
        Task<ISearchResponse<T>> GetPageByQueryAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, int currentPage, int pageSize = 10);


        Task<OprateResult<Id>> CreateAsync(T input);
        Task<BulkResponse> CreateListAsync(List<T> input);
        Task<OprateResult<int>> CreateListByExistValidateAsync(List<T> input);

        Task<OprateResult<T>> UpdateAsync(Id id, T input);


        Task<DeleteResponse> DeleteByIdAsync(Id id);
        Task<DeleteByQueryResponse> DeleteAllAsync();
        Task<DeleteByQueryResponse> DeleteByQueryAsync(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector);
       
    }
}
