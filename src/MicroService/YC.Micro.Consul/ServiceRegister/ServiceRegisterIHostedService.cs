using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YC.Micro.Configuration;
using YC.Micro.Consul.ServiceRegister.Options;

namespace YC.Micro.Consul.ServiceRegister
{
    /// <summary>
    /// 服务启动时自动注册
    /// </summary>
    public class ServiceRegisterIHostedService : IHostedService
    {
        private readonly ServiceRegisterOptions serviceRegistryOptions;
        private readonly ConsulClient _consulClient;
        private readonly IServiceDiscovery _serviceDiscovery;

        public ServiceRegisterIHostedService(IOptions<ServiceRegisterOptions> options, ConsulClient consulClient, IServiceDiscovery serviceDiscovery)
        {
            this.serviceRegistryOptions = options.Value;
            _consulClient = consulClient;
            _serviceDiscovery = serviceDiscovery;
        }

        /// <summary>
        /// 服务启动进行注册
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Uri uri = new Uri(serviceRegistryOptions.ServiceAddress);
            // 2、创建consul服务注册对象
            var registration = new AgentServiceRegistration()
            {
                ID = serviceRegistryOptions.ServiceId,
                Name = serviceRegistryOptions.ServiceName,
                Address = $"{uri.Scheme}://{uri.Host}",
                Port = uri.Port,
                Tags = serviceRegistryOptions.ServiceTags,

                Check = new AgentServiceCheck
                {
                    // 3.1、consul健康检查超时间
                    Timeout = TimeSpan.FromSeconds(20),
                    // 3.2、服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 3.3、consul健康检查地址
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}/HealthCheck",
                    // 3.4 consul健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(10),
                    TLSSkipVerify = false
                }
            };
            // 4、注册服务
            _consulClient.Agent.ServiceRegister(registration).Wait();

            #region 

            var nodes = _serviceDiscovery.Discovery(serviceRegistryOptions.ServiceName);
            var node = nodes.Find(x => serviceRegistryOptions.ServiceAddress.Contains(x.Url));
            if (node!=null)
            {
                _consulClient.Agent.ServiceDeregister(serviceRegistryOptions.ServiceId).Wait();
                // 4、注册服务
              
                //查找之前同名服务、相同地址但不包括自身的服务进行删除
                  var catalogServiceList =_serviceDiscovery.GetCatalogService(serviceRegistryOptions.ServiceName).ToList();
               var  repeatServiceList= catalogServiceList.Where(x => serviceRegistryOptions.ServiceName == x.ServiceName
                  && serviceRegistryOptions.ServiceAddress.Contains(x.ServiceAddress + ":" + x.ServicePort)
                  && x.ServiceID != serviceRegistryOptions.ServiceId).ToList();
                if (repeatServiceList.Count() > 1) {
                    var task = Task.Run(async () => {
                        for (int i = 0; i < repeatServiceList.Count(); i++)
                        {
                            await _consulClient.Agent.ServiceDeregister(repeatServiceList[i].ServiceID);
                        }

                    });
                    task.Wait();
                  
                }
                _consulClient.Agent.ServiceRegister(registration).Wait();

            }

            #endregion 

            Console.WriteLine($"服务注册成功:{uri.AbsolutePath}");

            return Task.CompletedTask;
        }

        /// <summary>
        /// 服务停止时注销
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                var configurationRoot = DefaultConfig.GetAppsettingsConfigurationEntity<ServiceRegisterSetting>();
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(configurationRoot.ConsulAddress);
            });

            consulClient.Agent.ServiceDeregister(serviceRegistryOptions.ServiceId).Wait();

            Console.WriteLine($"服务注销成功:{serviceRegistryOptions.ServiceAddress}");
            // 关闭连接
            consulClient.Dispose();
            return Task.CompletedTask;
        }
    }
}