using Autofac;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Core.Autofac;
using YC.FreeSqlFrameWork;

namespace YC.Micro.BookWebService
{
    /// <summary>
    /// FreeSql 注入模块
    /// </summary>
    [DependsOn]
    public class FreesqlAutofacModule : Autofac.Module
    {
        /// <summary>
        /// FreeSql 注入模块操作
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(FreeSqlRepository<,>)).As(typeof(IFreeSqlRepository<,>)).InstancePerLifetimeScope();//freeSql 注入

            builder.RegisterGeneric(typeof(FreeSqlRepository<>)).As(typeof(IFreeSqlRepository<>)).InstancePerLifetimeScope();//freeSql 注入

            //这个自带DI注入，也是可以的，反正后面都会被autofac接管
            //services.AddScoped(typeof(IFreeSqlRepository<,>), typeof(FreeSqlRepository<,>));
            //services.AddScoped(typeof(IFreeSqlRepository<>), typeof(FreeSqlRepository<>));

           
        }
    }
}

