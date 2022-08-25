using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using YC.Core.Autofac;

namespace YC.Core.Attribute
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]

    public class AopTestAttribute :  System.Attribute,  IFilterMetadata, IOrderedFilter, IDependencyInjectionSupport
    {
        public IHttpContextAccessor _httpContextAccessor;
        public AopTestAttribute(IHttpContextAccessor httpContextAccessor=null) {

            _httpContextAccessor = httpContextAccessor;
        }

        public int Order => throw new NotImplementedException();
    }
}
