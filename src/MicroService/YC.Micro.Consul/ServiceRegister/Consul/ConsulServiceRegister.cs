using Consul;
using Microsoft.Extensions.Options;
using YC.Micro.Consul.ServiceRegister.Options;
using System;

namespace YC.Micro.Consul.ServiceRegister
{
    /// <summary>
    /// consul服务注册实现
    /// </summary>
    public class ConsulServiceRegister : IServiceRegister
    {
        // 服务注册选项
        public readonly ServiceRegisterOptions serviceRegistryOptions;

        private readonly ConsulClient _consulClient;
        private readonly IServiceDiscovery _serviceDiscovery;

        public ConsulServiceRegister(IOptions<ServiceRegisterOptions> options, ConsulClient consulClient, IServiceDiscovery serviceDiscovery)
        {
            this.serviceRegistryOptions = options.Value;
            _consulClient = consulClient;
            _serviceDiscovery = serviceDiscovery;
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        public void Register()
        {
            //获取
            var uri = new Uri(serviceRegistryOptions.ServiceAddress);
            var nodes = _serviceDiscovery.Discovery(serviceRegistryOptions.ServiceName);

            var node = nodes.Find(x => serviceRegistryOptions.ServiceAddress.Contains(x.Url));

            // 3、创建consul服务注册对象
            var registration = new AgentServiceRegistration()
            {
                ID = string.IsNullOrEmpty(serviceRegistryOptions.ServiceId) ? Guid.NewGuid().ToString() : serviceRegistryOptions.ServiceId,
                Name = serviceRegistryOptions.ServiceName,
                Address = $"{uri.Scheme}://{uri.Host}",
                Port = uri.Port,
                Tags = serviceRegistryOptions.ServiceTags,
                Check = new AgentServiceCheck
                {
                    // 3.1、consul健康检查超时间
                    Timeout = TimeSpan.FromSeconds(10),
                    // 3.2、服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 3.3、consul健康检查地址
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}{"/HealthCheck"}",
                    // 3.4 consul健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(10),
                }
            };
            // 4、注册服务
            _consulClient.Agent.ServiceRegister(registration).Wait();

            Console.WriteLine($"服务注册成功:{serviceRegistryOptions.ServiceAddress}");
        }

        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="serviceNode"></param>
        public void Deregister()
        {
            // 注销服务
            _consulClient.Agent.ServiceDeregister(serviceRegistryOptions.ServiceId).Wait();
            Console.WriteLine($"服务注销成功:{serviceRegistryOptions.ServiceAddress}");
        }
    }
}