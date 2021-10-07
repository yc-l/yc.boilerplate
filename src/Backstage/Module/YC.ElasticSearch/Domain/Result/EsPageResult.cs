using Nest;
using System.Collections.Generic;

namespace YC.ElasticSearch.Domain
{
    /// <summary>
    /// 分页信息输出
    /// </summary>
    public class EsPageResult<T> where T : class,new()
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public long Total { get; set; } = 0;

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> List { get; set; }


        public IReadOnlyCollection<IHit<T>> Hits { get; set; }
    }
}
