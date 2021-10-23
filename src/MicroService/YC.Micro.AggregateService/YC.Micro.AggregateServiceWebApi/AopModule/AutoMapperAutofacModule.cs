using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using YC.Model.SysDbEntity;

namespace YC.Micro.AggregateServiceWebApi.AopModule
{
    /// <summary>
    /// autoMapper 单例注入
    /// </summary>
    public class AutoMapperAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                //当需要特定转换时候，使用这样的方式，自定义，通常是一个属性，转为另一个属性，在映射时候已经配置了转换，但存在类型差异可以这样
                cfg.CreateMap<string, long>().ConvertUsing(s => Convert.ToInt64(s));
                cfg.CreateMap<long, Int64>().ConvertUsing(s => Convert.ToInt64(s));
                cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s));
                cfg.CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
                cfg.CreateMap<string, Type>().ConvertUsing<TypeTypeConverter>();
                #region //配置项注入模式

                cfg.AddMaps(GetType().GetTypeInfo().Assembly);//提供给autoMapper 的注入，只要有继承Profile 类的，查找自动添加进来对应的映射,注册当前程序集
                #endregion

            });

          

            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();
            builder.RegisterInstance(mapper).SingleInstance();//单例注册 
           
        }


        #region 自定义转换拓展
        public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
        {
            public DateTime Convert(string source, DateTime destination, ResolutionContext context)
            {
                return System.Convert.ToDateTime(source);
            }
        }

        public class TypeTypeConverter : ITypeConverter<string, Type>
        {
            public Type Convert(string source, Type destination, ResolutionContext context)
            {
                return Assembly.GetExecutingAssembly().GetType(source);
            }
        }

        #endregion
    }

    public static class AssemblyExtenstion
    {

        public static IEnumerable<TResult> GetInstancesByAssembly<TResult>(this Assembly ass)
        {
            return ass.GetTypes()
                    .Where(x => typeof(TResult).IsAssignableFrom(x) && x.IsClass)
                    .Select(x => Activator.CreateInstance(x))
                    .Cast<TResult>();


        }
    }
}
