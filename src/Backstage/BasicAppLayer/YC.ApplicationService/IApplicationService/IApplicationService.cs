
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Core.Autofac;
using YC.Core.DynamicApi;
using YC.Core.DynamicApi.Attributes;

namespace YC.ApplicationService
{
   
    [DynamicWebApi]
    public interface IApplicationService: IDynamicWebApi, IDependencyInjectionSupport
    {


    }
    
}
