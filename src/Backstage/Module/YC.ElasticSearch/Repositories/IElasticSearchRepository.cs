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
        Task<IEnumerable<T>> GetByQueryAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector = null);
        Task<Tuple<IEnumerable<T>, AggregateDictionary>> GetByQueryAggregationsAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector
           );
        Task<IEnumerable<T>> GetPageByQueryAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, int currentPage, int pageSize = 10, Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector = null);
        Task<IEnumerable<T>> GetByQueryIdsAsync(List<string> ids);
        Task<IEnumerable<T>> GetAllAsync(Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector = null);
        Task<OprateResult<Id>> CreateAsync(T input, bool isCeated = false);
        Task<BulkResponse> CreateListAsync(List<T> input, bool isCeated = false);

        Task<OprateResult<int>> CreateListByExistValidateAsync(List<T> input);

        Task<CreateIndexResponse> CreateIndexAsync();
        Task<OprateResult<T>> UpdateAsync(Id id, T input);


        Task<DeleteResponse> DeleteByIdAsync(Id id);
        Task<DeleteByQueryResponse> DeleteAllAsync();
        Task<DeleteByQueryResponse> DeleteByQueryAsync(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector);
       
    }
}
