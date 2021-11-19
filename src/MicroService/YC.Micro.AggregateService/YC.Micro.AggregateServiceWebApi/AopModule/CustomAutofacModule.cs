using Autofac;
using Consul;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Core;
using YC.Micro.AggregateServiceWebApi.Tenant;
using YC.Micro.Configuration;
using YC.Micro.Consul;
using YC.Micro.UserWebService;
using static YC.Micro.UserWebService.IUserService;

namespace YC.Micro.AggregateServiceWebApi.AopModule
{
    public class CustomAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //多租户注入
            builder.RegisterType<DefaultTenant>().As<ITenant>().AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();

            var configurationRoot = DefaultConfig.GetAppsettingsConfigurationEntity<ConsulSetting>();
            //consul Client配置注入
            builder.RegisterType<ConsulClient>().WithParameter("configOverride", new Action<ConsulClientConfiguration>(options=> {
                options.Address =new Uri(configurationRoot.ConsulServiceAddress);
            })).SingleInstance().PropertiesAutowired();
            //consul 配置注入
            builder.RegisterType<ConsulServiceDiscovery>().As<IServiceDiscovery>()
                .AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
            //consul 负载均衡随机算法
            builder.RegisterType<RandomLoadBalance>().As<ILoadBalance>()
            .AsImplementedInterfaces().SingleInstance().PropertiesAutowired();
          
        }
    }
}
