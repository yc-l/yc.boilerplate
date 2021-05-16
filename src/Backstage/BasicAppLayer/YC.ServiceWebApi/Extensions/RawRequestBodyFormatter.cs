using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.ServiceWebApi.Extensions
{
    /// <summary>
    /// 该类主要是用于验证如果输入对象参数类型与绑定对象参数类型不一致时，api接口会包错误。
    /// 进行重新绑定数据，然后得到对象对应的数据类型
    /// </summary>
    public class RawRequestBodyFormatter : IInputFormatter
    {
        public RawRequestBodyFormatter()
        {

        }
        /// <summary>
        /// 做指定类型的处理
        /// </summary>
        /// <param name="context">上下文内容</param>
        /// <returns></returns>
        public bool CanRead(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException("argument is Null");
            var contentType = context.HttpContext.Request.ContentType;
            //if (string.IsNullOrEmpty(contentType) || contentType == "text/plain" || contentType == "application/octet-stream"||contentType=="application/json")
            if (string.IsNullOrEmpty(contentType) || contentType == "application/json")
                return true;
            return false;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;
            if (contentType.ToLower() == "application/json")
            {
                using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    var content = await reader.ReadToEndAsync();
                    var type = context.ModelType.Assembly.GetType(context.ModelType.FullName);
                    var aaa = Newtonsoft.Json.JsonConvert.DeserializeObject(content, type);
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }
            return await InputFormatterResult.FailureAsync();
        }
    }

}
