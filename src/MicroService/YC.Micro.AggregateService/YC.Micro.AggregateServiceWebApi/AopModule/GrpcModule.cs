using Autofac;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Core;
using YC.Micro.AggregateServiceWebApi.Tenant;
using YC.Micro.Configuration;
using YC.Micro.Consul;
using YC.Micro.Core;
using YC.Micro.UserWebService;
using static YC.Micro.AggregateServiceWebApi.IBookService;
using static YC.Micro.UserWebService.IUserService;

namespace YC.Micro.AggregateServiceWebApi.AopModule
{
    public static class GrpcModuleExtensions
    {
        public static IServiceCollection AddGrpcModule(this IServiceCollection services)
        {
            var configurationRoot = DefaultConfig.AppsettingsConfiguration;
            GrpcChannelSetting grpcChannelSetting = DefaultConfig.GetAppsettingsConfigurationEntity<GrpcChannelSetting>(configurationRoot);

            //Transient 对象总是不同的; 每个控制器和每个服务都提供了一个新的实例。
            //Scoped 对象在请求中是相同的，但在不同的请求中是不同的。
            //Singleton 对象对于每个对象和每个请求都是一样的（不管ConfigureServices中是否提供一个实例）
            // container will create the instance(s) of these types and will dispose them
            services.AddTransient<ClientInterceptor>();
            //container did not create instance so it will NOT dispose it
            // services.AddTransient(new ClientInterceptor());

            //User grpc服务
            var userServiceUrl = grpcChannelSetting.GrpcServices.Where(x => x.Name == "UserService").Select(x => x.Url).FirstOrDefault();

            services.AddGrpcClient<IUserService.IUserServiceClient>(options =>
            {
                options.Address = new Uri(userServiceUrl);
            });
            // Book grpc服务
            var bookServiceUrl = grpcChannelSetting.GrpcServices.Where(x => x.Name == "BookService").Select(x => x.Url).FirstOrDefault();

            services.AddGrpcClient<IBookService.IBookServiceClient>(options =>
            {
                options.Address = new Uri(bookServiceUrl);
            });

            return services;
        }
    }
}