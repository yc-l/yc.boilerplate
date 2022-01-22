using Autofac;
using Autofac.Extras.DynamicProxy;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YC.ApplicationService;
using YC.Cache.Redis;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Cache;
using YC.DapperFrameWork;
using YC.ServiceWebApi.Filter;

namespace YC.ServiceWebApi.AopModule
{
    [DependsOn]
    public class DapperAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope().EnableInterfaceInterceptors()
                     .InterceptedBy(typeof(AopInterceptor));//这一步是仓储注入的重要的点，允许拦截器

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());//注入当前程程序集

            builder.RegisterType<DefaultUnitOfWork>().As<DapperFrameWork.IUnitOfWork>().WithParameter("dbConnectionString", DefaultConfig.TenantSetting.DefaultDbConnectionString).WithParameter("defaultDbType", GetDbType()).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
            //builder.RegisterType<TenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
        }

        private static RepositoryUtils.Dialect GetDbType()
        {
            RepositoryUtils.Dialect dbType;
            switch (DefaultConfig.TenantSetting.DefaultDbType)
            {
                case 0: dbType = RepositoryUtils.Dialect.MySQL; break;
                case 1: dbType = RepositoryUtils.Dialect.SQLServer; break;
                case 2: dbType = RepositoryUtils.Dialect.PostgreSQL; break;
                case 4: dbType = RepositoryUtils.Dialect.SQLite; break;
                default: dbType = RepositoryUtils.Dialect.SQLServer; break;
            }
            return dbType;
        }
    }
}