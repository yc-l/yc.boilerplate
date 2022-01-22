using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService;
using YC.Common;
using YC.Common.ShareUtils;
using YC.Core;
using YC.ServiceWebApi.Result;

namespace YC.ServiceWebApi.Filter
{
    public class AOPResourceFilterAttribute : Attribute, IResourceFilter
    {
        private readonly ILogger<AOPResourceFilterAttribute> logger;

        public AOPResourceFilterAttribute(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<AOPResourceFilterAttribute>();
        }

        /// <summary>
        /// 拦截action 级别错误日志
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (context.Exception != null)//如果异常不为空
            {
                var result = new ApiResult<string>();
                LogUtils.WriteLog(new LogDto() { TypeName = "Action执行异常日志", Message = "异常：" + context.Exception?.ToString() });//可以在这里统一全局拦截返回
                if (DefaultConfig.DefaultAppConfig.IsDebug)//开发状态下显示所有异常信息
                {
                    result = new ApiResult<string>().NotOk(context.Exception?.ToString());
                }
                else//生产状态，如果有自定义异常，就抛出，如果不是提示默认
                {
                    result = new ApiResult<String>().NotOk(DefaultConfig.DefaultAppConfig.DefaultExceptionString);
                }
                context.Result = new JsonResult(result);
                }
            }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // logger.LogInformation("ResourceFilter Executing!");
        }
    }
}