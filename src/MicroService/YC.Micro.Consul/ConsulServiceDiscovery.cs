using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace YC.Micro.Consul
{
    /// <summary>
    /// consul服务发现
    /// </summary>
    public class ConsulServiceDiscovery: IServiceDiscovery
    {
        private string _consulServiceAddress;
        private readonly ConsulClient _consulClient;
        private readonly ILoadBalance _loadBalance;
       
        public ConsulServiceDiscovery(ConsulClient consulClient, ILoadBalance loadBalance) {
            _consulClient = consulClient;
            _loadBalance = loadBalance;
           
        }

        public string GetLoadBalanceActivityServiceAddress(string serviceName) {

            // 1、获取服务地址
            List<ConsulServiceNode> serviceNodes =Discovery(serviceName);

            // 2、负载均衡选择
            ConsulServiceNode serviceNode = _loadBalance.Select(serviceNodes);
            return serviceNode.Url;
        }
        public List<ConsulServiceNode> Discovery(string serviceName)
        {
            // 1.2、从远程服务器取
            CatalogService[] queryResult = RemoteDiscovery(serviceName);

            var list = new List<ConsulServiceNode>();
            foreach (var service in queryResult)
            {
                list.Add(new ConsulServiceNode { Url = service.ServiceAddress + ":" + service.ServicePort });
            }

            return list;
        }

        public CatalogService[] GetCatalogService(string serviceName)
        {
            // 1.2、从远程服务器取
            CatalogService[] queryResult = RemoteDiscovery(serviceName);
            return queryResult;
        }

        private CatalogService[] RemoteDiscovery(string serviceName)
        {
           
            // consul查询服务,根据具体的服务名称查询
            var queryResult = _consulClient.Catalog.Service(serviceName).Result;
            // 3、判断请求是否失败
            if (!queryResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"consul连接失败:{queryResult.StatusCode}");
            }

            return queryResult.Response;
        }

    }
}
