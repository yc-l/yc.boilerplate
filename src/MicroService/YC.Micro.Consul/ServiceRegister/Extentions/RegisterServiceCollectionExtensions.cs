using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using YC.Micro.Consul.ServiceRegister.Options;
using YC.Micro.Configuration;

namespace YC.Micro.Consul.ServiceRegister.Extentions
{
    /// <summary>
    ///  服务注册IOC容器扩展
    /// </summary>
    public static class RegisterServiceCollectionExtensions
    {
        // consul服务注册
        public static IServiceCollection AddServiceRegister(this IServiceCollection services)
        {
            IConfiguration configuration = DefaultConfig.AppsettingsConfiguration;

            // 1、配置选项到IOC
            //services.Configure<ServiceRegisterOptions>(configuration.GetSection("ServiceRegisterSetting"));
            services.AddSingleton<ServiceRegisterOptions>();
            // 2、注册开机自动注册服务
            services.AddHostedService<ServiceRegisterIHostedService>();
            return services;
        }
    }
}