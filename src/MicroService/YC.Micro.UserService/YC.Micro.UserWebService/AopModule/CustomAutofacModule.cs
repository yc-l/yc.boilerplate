using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Core;
using YC.Micro.UserWebService.Tenant;

namespace YC.Micro.UserWebService.AopModule
{
    public class CustomAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //多租户注入
            builder.RegisterType<DefaultTenant>().As<ITenant>().AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
        }
    }
}
