using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.Caching.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.ApplicationService;
using YC.Cache.Redis;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Cache;
using YC.MongoDB;

namespace YC.ServiceWebApi.AopModule
{

    public class CustomAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AopInterceptor>();
            //builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().InstancePerLifetimeScope();
            //Mongodb 注入
            // builder.RegisterType<MongoDbRepository>().As<IMongoDbRepository>().WithParameter("connectionString", DefaultConfig.DefaultAppConfigDto.MongoDbString).WithParameter("dbName", DefaultConfig.DefaultAppConfigDto.MongoDbName).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired(); 

            #region redis cache
            var tempConfigOptions = new StackExchange.Redis.ConfigurationOptions();
            tempConfigOptions.SyncTimeout = 5000;
            tempConfigOptions.ConnectTimeout = 15000;
            tempConfigOptions.ResponseTimeout = 15000;
            //redis cache注入
            builder.RegisterType<RedisCacheManager>().As<ICacheManager>().WithParameter("options", new RedisCacheOptions()
            {
                Configuration = DefaultConfig.ConnectionRedis.Connection,
                InstanceName = DefaultConfig.ConnectionRedis.InstanceName,
                ConfigurationOptions = tempConfigOptions,
            }).InstancePerLifetimeScope();
            #endregion
            //多租户注入
            builder.RegisterType<DefaultTenant>().As<ITenant>().AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
          
           
        }
    }
}
