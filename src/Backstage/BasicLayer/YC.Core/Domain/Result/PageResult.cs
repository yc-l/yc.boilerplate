using System.Collections.Generic;

namespace YC.Core
{
    /// <summary>
    /// 分页信息输出
    /// </summary>
    public class PageResult<T>
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public long Total { get; set; } = 0;

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> List { get; set; }
    }
}
