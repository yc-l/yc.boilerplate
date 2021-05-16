using Autofac;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using YC.Common.NetCoreUtils;

namespace YC.Core.Autofac
{
    /// <summary>
    /// Autofac依赖注入服务
    /// </summary>
    public class AutofacUtils
    {
        /// <summary>
        /// Autofac依赖注入静态服务
        /// </summary>
        public static ILifetimeScope Container { get; set; }

        public static IServiceProvider _serviceProvider;
        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T Resolve<T>() where T : class
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }

        /// <summary>
        /// 获取服务(Single)
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetService<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        /// <summary>
        /// 获取服务(请求生命周期内)
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetScopeService<T>() where T : class
        {
            return (T)GetService<IHttpContextAccessor>().HttpContext.RequestServices.GetService(typeof(T));
        }
    }
}
