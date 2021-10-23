using Autofac;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Core;
using YC.Micro.AggregateServiceWebApi.Tenant;
using YC.Micro.Configuration;
using YC.Micro.UserWebService;
using static YC.Micro.UserWebService.IUserService;

namespace YC.Micro.AggregateServiceWebApi.AopModule
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
