using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.ElasticSearch.Domain
{
  public  class ScrollResult<T> where T : class, new()
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public long Total { get; set; } = 0;
        /// <summary>
        /// 上一个查询获取的索引Key
        /// </summary>
        public string ScrollId { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> List { get; set; }


        public IReadOnlyCollection<IHit<T>> Hits { get; set; }
    }

   
}
