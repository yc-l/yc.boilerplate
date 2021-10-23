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

namespace YC.Micro.AggregateServiceWebApi.AopModule
{
    /// <summary>
    ///ElasticSearch 注入模块
    /// </summary>
    public class ElasticSearchAutofacModule : Autofac.Module
    {
        /// <summary>
        /// ElasticSearch 注入模块操作
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ElasticSearchRepository<>)).As(typeof(IElasticSearchRepository<>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());//注入当前程程序集
           
            var dbType =
        builder.RegisterType<ElasticSearchDbContext>().As<IElasticSearchDbContext>().WithParameter("nodesArray", DefaultConfig.GetAppsettingsConfigurationEntity<ElasticSearchSetting>().Nodes.Select(x=>x.Node).ToArray()).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
          



        }
    }
}

