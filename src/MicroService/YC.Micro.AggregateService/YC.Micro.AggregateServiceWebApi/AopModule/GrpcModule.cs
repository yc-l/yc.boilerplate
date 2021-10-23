using Autofac;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Core;
using YC.Micro.AggregateServiceWebApi.Tenant;
using YC.Micro.Configuration;
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
