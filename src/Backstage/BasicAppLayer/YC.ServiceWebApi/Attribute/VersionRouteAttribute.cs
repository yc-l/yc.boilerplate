using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YC.ServiceWebApi
{
    /// <summary>
    /// 自定义路由 /api/{version}/[area]/[controler]/[action]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class VersionRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {
        public string GroupName { get; set; }

        public VersionRouteAttribute(ApiVersion version = ApiVersion.V2, string action = "[action]")
            : base($"/api/{version}/[area]/[controller]/{action}")
        {
            GroupName = version.ToString();
        }


    }

    /// 接口版本
    /// </summary>
    public enum ApiVersion
    {
        /// <summary>
        /// V1 版本
        /// </summary>
        V1 = 1,
        /// <summary>
        /// V2 版本
        /// </summary>
        V2 = 2,
    }
}
