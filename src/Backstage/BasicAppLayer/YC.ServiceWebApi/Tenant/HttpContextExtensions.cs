using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using YC.Common;

namespace YC.ServiceWebApi.Tenant
{
    //租户拦截核心--注入操作获取方法
    public class HttpContextExtensions
    {
        public static string GetRawBodyStringFormater(HttpRequest httpRequest, Encoding encoding)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            httpRequest.EnableBuffering();
            using (StreamReader reader = new StreamReader(httpRequest.Body, encoding))
            {
                var data = reader.ReadToEnd();//
                httpRequest.Body.Seek(0, SeekOrigin.Begin);
                return data;
            }

        }

        public static string GetRequestParams(HttpRequest request)
        {

            string requestParamsString = "";
            string[] contentTypeArray = new string[] { "application/json;charset=utf-8", "application/json", "text/json" };

            var isExist = contentTypeArray.Any(x => request.ContentType.ToLower().Contains(x));
            if (isExist)
            {
                requestParamsString = HttpContextExtensions.GetRawBodyStringFormater(request, Encoding.UTF8);
                //获取完成后，需要回写给Request.Body,要不然读取完成后，接口那边获取不到
                byte[] array = Encoding.UTF8.GetBytes(requestParamsString);
                MemoryStream stream = new MemoryStream(array);
                request.Body = stream;
            }

            return requestParamsString;
        }
    }
}
