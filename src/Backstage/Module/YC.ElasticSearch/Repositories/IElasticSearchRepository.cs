using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ElasticSearch.Domain;

namespace YC.ElasticSearch
{
    public interface IElasticSearchRepository<T> where T : class, new()
    {
        IElasticSearchDbContext ElasticSearchDbContext { get; }
         string MappingName { get;  }

        Task<long> GetCountAsync();
        Task<T> GetAsync(Id id);
        Task<IEnumerable<T>> GetByQueryAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, Func<SortDescriptor<T>, IPromise<IList<ISort>>> sort = null);
       
        Task<EsPageResult<T>> GetPageByQueryAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, int currentPage, int pageSize = 10,
            Func<SortDescriptor<T>, IPromise<IList<ISort>>> sort = null, Func<HighlightDescriptor<T>, IHighlight> highlight = null);

        Task<IEnumerable<T>> GetByQueryIdsAsync(List<string> ids);
        Task<IEnumerable<T>> GetAllAsync(Func<SortDescriptor<T>, IPromise<IList<ISort>>> sort = null);
        Task<OprateResult<Id>> CreateAsync(T input, bool isCeated = false);
        Task<BulkResponse> CreateListAsync(List<T> input, bool isCeated = false);

        Task<OprateResult<int>> CreateListByExistValidateAsync(List<T> input);

        Task<CreateIndexResponse> CreateIndexAsync();
        Task<OprateResult<T>> UpdateAsync(Id id, T input);


        Task<DeleteResponse> DeleteByIdAsync(Id id);
        Task<DeleteByQueryResponse> DeleteAllAsync();
        Task<DeleteByQueryResponse> DeleteByQueryAsync(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> selector);



        #region 高级方案

        Task<Tuple<IEnumerable<T>, AggregateDictionary>> GetByQueryAggregationsAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector
          );
    
        Task<SearchAfterResult<T>> GetPageByQuerySearchAfterAsync(Func<QueryContainerDescriptor<T>, QueryContainer> query, Func<SortDescriptor<T>, IPromise<IList<ISort>>> sort,
             int pageSize = 10, IEnumerable<object> searchAfter = null, Func<HighlightDescriptor<T>, IHighlight> highlight = null); 
        #endregion
    }
}
