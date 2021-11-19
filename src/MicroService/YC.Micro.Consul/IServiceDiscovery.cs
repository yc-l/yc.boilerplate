using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Micro.Consul
{
    /// <summary>
    /// 服务发现
    /// </summary>
    public interface IServiceDiscovery
    {
        /// <summary>
        /// 服务发现
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        List<ConsulServiceNode> Discovery(string serviceName);


        CatalogService[] GetCatalogService(string serviceName);
        
            /// <summary>
            /// 获取正常运行、负载均衡算法过滤后指定服务地址
            /// </summary>
            /// <param name="serviceName">服务名称</param>
            /// <returns></returns>
            string GetLoadBalanceActivityServiceAddress(string serviceName);
    }
}
