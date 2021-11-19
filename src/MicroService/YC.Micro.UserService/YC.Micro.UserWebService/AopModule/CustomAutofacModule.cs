using Autofac;
using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Core;
using YC.Core.Autofac;
using YC.Micro.Configuration;
using YC.Micro.Consul;
using YC.Micro.UserWebService.Tenant;

namespace YC.Micro.UserWebService.AopModule
{
    [DependsOn]
    public class CustomAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //多租户注入
            builder.RegisterType<DefaultTenant>().As<ITenant>().AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();

            var configurationRoot = DefaultConfig.GetAppsettingsConfigurationEntity<ServiceRegisterSetting>();
            //consul Client配置注入
            builder.RegisterType<ConsulClient>().WithParameter("configOverride", new Action<ConsulClientConfiguration>(options => {
                options.Address = new Uri(configurationRoot.ConsulAddress);
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
