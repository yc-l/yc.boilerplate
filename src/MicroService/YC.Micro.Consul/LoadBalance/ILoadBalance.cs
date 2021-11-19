
using System.Collections.Generic;

namespace YC.Micro.Consul
{
    /// <summary>
    /// 服务负载均衡
    /// </summary>
    public interface ILoadBalance
    {
        /// <summary>
        /// 服务选择
        /// </summary>
        /// <param name="serviceUrls"></param>
        /// <returns></returns>
        ConsulServiceNode Select(IList<ConsulServiceNode> serviceUrls);

        /// <summary>
        /// 服务选择string
        /// </summary>
        /// <param name="serviceUrls"></param>
        /// <returns></returns>
        string SelectOne(IList<string> serviceUrls);
    }
}
