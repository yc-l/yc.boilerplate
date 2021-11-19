using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using YC.Core.Autofac;
using YC.ElasticSearch;
using YC.FreeSqlFrameWork;
using YC.Micro.Configuration;

namespace YC.Micro.BookWebService.AopModule
{
    /// <summary>
    ///ElasticSearch 注入模块
    /// </summary>
    [DependsOn]
    public class ElasticSearchAutofacModule : Autofac.Module
    {
        /// <summary>
        /// ElasticSearch 注入模块操作
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ElasticSearchRepository<>)).As(typeof(IElasticSearchRepository<>)).InstancePerLifetimeScope();//这一步是仓储注入的重要的点，允许拦截器

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());//注入当前程程序集


          var nodes=  DefaultConfig.GetAppsettingsConfigurationEntity<ElasticSearchSetting>().Nodes.Select(x => x.Node).ToArray();
            var dbType =
        builder.RegisterType<ElasticSearchDbContext>().As<IElasticSearchDbContext>().WithParameter("nodesArray", nodes).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
          



        }
    }
}

