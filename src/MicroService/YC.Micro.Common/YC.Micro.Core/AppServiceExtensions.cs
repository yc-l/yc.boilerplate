using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YC.Micro.Core
{
    public static class AppServiceExtensions
    {
        /// <summary>
        /// 注册应用程序域中所有有AppService特性的服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddAppServices(this IServiceCollection services)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    var serviceAttribute = type.GetCustomAttribute<AppServiceAttribute>();

                    if (serviceAttribute != null)
                    {

                        var serviceType = serviceAttribute.ServiceType;
                        if (serviceType == null && serviceAttribute.InterfaceServiceType)
                        {
                            serviceType = type.GetInterfaces().FirstOrDefault();
                        }
                        if (serviceType == null)
                        {
                            serviceType = type;
                        }
                        switch (serviceAttribute.Lifetime)
                        {
                            case ServiceLifetime.Singleton:
                                services.AddSingleton(serviceType, type);
                                break;
                            case ServiceLifetime.Scoped:
                                services.AddScoped(serviceType, type);
                                break;
                            case ServiceLifetime.Transient:
                                services.AddTransient(serviceType, type);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
