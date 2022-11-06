using Autofac;
using Autofac.Extras.DynamicProxy;
using CSRedis;
using Microsoft.Extensions.Caching.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.ApplicationService;
using YC.Cache.Redis;
using YC.Core;
using YC.Core.Attribute;
using YC.Core.Autofac;
using YC.Core.Cache;
using YC.FreeSqlFrameWork;
using YC.MongoDB;

namespace YC.ServiceWebApi.AopModule
{
    [DependsOn]
    public class CustomAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AopTestAttribute>().InstancePerLifetimeScope().PropertiesAutowired();
            builder.RegisterType<AopInterceptor>();

            //Mongodb 注入
            // builder.RegisterType<MongoDbRepository>().As<IMongoDbRepository>().WithParameter("connectionString", DefaultConfig.DefaultAppConfig.MongoDbString).WithParameter("dbName", DefaultConfig.DefaultAppConfig.MongoDbName).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
            switch (DefaultConfig.DefaultAppConfig.UseCacheModuleType)
            {
                //redis 注入
                case 0:
                    var tempConfigOptions = new StackExchange.Redis.ConfigurationOptions();
                    tempConfigOptions.SyncTimeout = 5000;
                    tempConfigOptions.ConnectTimeout = 15000;
                    tempConfigOptions.ResponseTimeout = 15000;

                    // CSRedis 版本
                    string testRedisConfig = DefaultConfig.DefaultAppConfig.RedisConnectionString;
                    var csredis = new CSRedis.CSRedisClient(testRedisConfig);
                    RedisHelper.Initialization(csredis);// 直接用
                    builder.RegisterType<CSRedisClient>().WithParameter("connectionString", testRedisConfig)
                        .InstancePerLifetimeScope();

                    //redis cache注入,CSRedisCore
                    builder.RegisterType<CSRedisCacheManager>().As<ICacheManager>().InstancePerLifetimeScope();

                    ////redis cache注入,StackExchange.Redis版本
                    //builder.RegisterType<RedisCacheManager>().As<ICacheManager>().WithParameter("options", new RedisCacheOptions()
                    //{
                    //    Configuration = DefaultConfig.ConnectionRedis.Connection,
                    //    InstanceName = DefaultConfig.ConnectionRedis.InstanceName,
                    //    ConfigurationOptions = tempConfigOptions,
                    //}).SingleInstance();

                    break;
                //默认内存注入
                case 1:
                    builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().InstancePerLifetimeScope(); break;
                default:
                    builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().InstancePerLifetimeScope(); break;
            }

            //多租户注入
            builder.RegisterType<DefaultTenant>().As<ITenant>().AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
        }
    }
}