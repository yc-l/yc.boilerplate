using Autofac;
using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YC.Core.Autofac
{
    public static class DependModuleExtensions
    {

        public static void AddDependOnModule(this IServiceCollection services, ContainerBuilder builder) {

            var dependsOnType = typeof(DependsOn);
            //Assembly.GetExecutingAssembly();
          var  dependsOnTypeList  = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(u => u.GetCustomAttribute(typeof(DependsOn))!=null).ToList();
            dependsOnTypeList = dependsOnTypeList.Where(x => x.BaseType.FullName == "Autofac.Module").ToList();
            foreach (var d in dependsOnTypeList) {

               var module= (IModule)Convert.ChangeType(Activator.CreateInstance(d), d);
                builder.RegisterModule(module);
            }
        }
    }
}
