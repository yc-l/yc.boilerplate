using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YC.Common;
using YC.Common.ShareUtils;

namespace YC.Core.HttpExtensions
{
    public static class HttpRequestExtensions
    {
        public static string GetAbsoluteUri(HttpContext context)
        {
            string paramsData = "";

            if (context.Request.QueryString.Value != null)
            {
                paramsData += context.Request.QueryString;
            }
            try
            {
                paramsData += GetRawBodyStringFormater(context.Request, Encoding.UTF8);
                if (context.Request.Form != null)//token 请求时候，内容为空，会报异常
                {
                    paramsData += context.Request.Form.ToIndentedJson();
                }
            }
            catch (Exception ex)
            {

                LogUtils.WriteLog(new LogDto() { TypeName = "HttpRequestExtensions 拦截日志", Message = ex.ToString() });
            }

            //Stream stream = context.Request.Body;
            //Encoding encoding = Encoding.UTF8;
            //string responseData = string.Empty;
            //using (StreamReader reader = new StreamReader(stream, encoding))
            //{
            //    paramsData = reader.ReadToEnd();
            //}

            return $"请求地址：{context.Request.Host}{context.Request.Path},请求参数：{paramsData}";
            //return new StringBuilder()
            //    .Append(context.Request.Scheme)
            //    .Append("://")
            //    .Append(context.Request.Host)
            //    .Append(context.Request.PathBase)
            //    .Append(context.Request.Path)
            //    .Append(context.Request.QueryString)
            //    .ToIndentedJson();
        }
        public static string GetHttpHeader(HttpContext context)
        {

            return new StringBuilder()
                 .Append(context.Request.Headers.ToIndentedJson())
                .ToString();
        }

        public static string GetRawBodyStringFormater(HttpRequest httpRequest, Encoding encoding)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            //httpRequest.Body.CopyTo(tempStream);
            using (StreamReader reader = new StreamReader(httpRequest.Body, encoding))
            {
                var data = reader.ReadToEnd();//

                return data;
            }

        }
    }
}
