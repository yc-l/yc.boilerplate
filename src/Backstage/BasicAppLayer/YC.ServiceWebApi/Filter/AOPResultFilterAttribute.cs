using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService;
using YC.Common;
using YC.Common.ShareUtils;
using YC.Core;
using YC.Core.HttpExtensions;
using YC.ServiceWebApi.Result;

namespace YC.ServiceWebApi.Filter
{
    public class AOPResultFilterAttribute : ResultFilterAttribute
    {
        private readonly ILogger<AOPResultFilterAttribute> logger;

        public AOPResultFilterAttribute(ILoggerFactory loggerFactory=null)
        {
            logger = loggerFactory.CreateLogger<AOPResultFilterAttribute>();
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
           
            
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var obj = context.Result;
            if (obj?.GetType() == typeof(ObjectResult))
            {
                try
                {
                    var result = (ObjectResult)obj;

                    //aop拦截处理 如果不是我们已经定义标准化返回，那么我们需要在外层包一层，如果不是，就直接让他自己按照正常处理返回
                    if (!(result.Value.GetType().Name.Contains(typeof(ApiResult).Name)|| result.Value.GetType().Name.Contains(typeof(ApiResult<>).Name)))
                    {
                     
                        context.Result= new JsonResult(ApiResult.Result<object>(true, result.Value, "请求成功."));
                    }
                   
                }
                catch (Exception ex)
                {
                    context.Result = obj;
                    logger.LogError("转换返回JsonResult失败！"+ex.ToString());
                    throw ex;
                }
            }
           
        }
    }

   
}


