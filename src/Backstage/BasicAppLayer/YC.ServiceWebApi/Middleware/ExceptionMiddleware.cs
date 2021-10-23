using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService;
using YC.Core;
using YC.ServiceWebApi.Result;

namespace YC.ServiceWebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger _logger;
        private IHostingEnvironment environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostingEnvironment environment)
        {
            this.next = next;
            this._logger = logger;
            this.environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
                var features = context.Features;
            }
            catch (Exception e)
            {

                await HandleException(context, e);

            }
        }

        private async Task HandleException(HttpContext context, Exception e)
        {
            context.Response.StatusCode = 200;//这里不能改为500，或者400,要改为200，保证前端接收正常状态，然后在返回内部状态定义结果code 是400
            context.Response.ContentType = "application/json;charset=utf-8;";
            string error = "";
            string errorMessage = "";
            DefaultConfig defaultConfig = new DefaultConfig();
            bool isExistCustomError = false;
            void ReadException(Exception ex)
            {
                error += string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, ex.InnerException);
                if (ex.InnerException != null)
                {
                    ReadException(ex.InnerException);

                    if (ex.InnerException.Message.Contains(DefaultConfig.DefaultAppConfig.ExceptionKey))
                    {
                        errorMessage += ex.InnerException.Message;
                        isExistCustomError = true;
                    }
                }

            }

            ReadException(e);
            _logger.LogError(e.ToString());
            var result = new ApiResult<string>();
            if (DefaultConfig.DefaultAppConfig.IsDebug)//开发状态下显示所有异常信息
            {
                result = new ApiResult<string>().NotOk(e?.ToString());
                //error = JsonConvert.SerializeObject(result);
            }
            else//生产状态，如果有自定义异常，就抛出，如果不是提示默认
            {
                result = e.Message.Contains(DefaultConfig.DefaultAppConfig.ExceptionKey)? new ApiResult<String>().NotOk(e.Message): new ApiResult<String>().NotOk(DefaultConfig.DefaultAppConfig.DefaultExceptionString);
                //error = JsonConvert.SerializeObject(result);
            }

            var jsonResult = new { code = "400", message = result.Message };//返回的是一个对象由于前端接收json 要小写，所以在这里必须特殊处理
            byte[] array = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(jsonResult));//返回的需要一个序列化，前端接收会进行反序列化，最后得到一个对象
            //MemoryStream stream = new MemoryStream(array);
            //context.Response.Body = stream;
            await context.Response.Body.WriteAsync(array, 0, array.Length);
        }
    }
}
