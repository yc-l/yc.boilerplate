using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using YC.Common;
using YC.Common.ShareUtils;
using YC.Core.Attribute;
using YC.Core.HttpExtensions;

namespace YC.Core.Autofac
{
    public class AopInterceptor : IInterceptor
    {
        IHttpContextAccessor _httpContextAccessor;
        public AopInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;//注入无法获取
        }
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            #region 可拓展处理代码

            //string requestParams = "";
            ////只要不是有Service 关键字的服务就直接操作，不进行日志留存，这里通常过滤 工作单元、缓存等
            //if (!invocation.Method.Name.Contains("Service"))
            //{

            //    //var data = invocation.Arguments.Where(x => x.ToString() == "TanantId").FirstOrDefault();

            //    //var t = invocation.ReturnValue;
            //    //var t1 = invocation.;
            //    //var t2 = invocation.MethodInvocationTarget;

            //    if (invocation.Arguments != null)
            //    {
            //        requestParams = string.Format("你正在调用方法 \"{0}\"  参数是 {1}... ", invocation.Method.Name,
            //                string.Join(", ", invocation.Arguments.Select(a => (a ?? "")?.ToString()).ToArray()));

            //        LogUtils.WriteLog(new LogDto() { TypeName = "请求日志", Message = "请求参数：" + requestParams });
            //    }
            //    bool isException = false;
            //    try
            //    {
            //        invocation.Proceed();//进行业务操作，这里拦截不到service 异常，由于使用 动态api模式，直接会在Action AOPResourceFilterAttribute中的OnResourceExecuted拦截了
            //    }
            //    catch (Exception ex)
            //    {
            //        isException = true;
            //        LogUtils.WriteLog(new LogDto() { TypeName = "执行异常日志", Message = "异常：" + ex.ToString() });//可以在这里统一全局拦截返回
            //        invocation.ReturnValue = new ApiResult<string>().NotOk("程序内部错误.");//为返回结果赋值,全部返回程序内部错误，异常结果日志拦截
            //    }

            //    if (invocation.ReturnValue != null && !isException)//非异常情况下，才记录请求日志结果值
            //    {
            //        LogUtils.WriteLog(new LogDto() { TypeName = "请求日志", Message = "返回值：" + invocation.ReturnValue?.ToString() });//可以在这里统一全局拦截返回值的设计
            //    }

            //    return;
            //}
            ////过滤登录时候的拦截，此时登录用户为空
            //if (!invocation.Method.Name.Equals("Login"))
            //{
            //    LogUtils.WriteLog(new LogDto() { TypeName = "请求日志", Message = "\r\n" + $"登录用户信息：{GetSession("loginUser")}" });
            //}

            //requestParams = string.Format("你正在调用方法 \"{0}\"  参数是 {1}... ", invocation.Method.Name,
            //         string.Join(", ", invocation.Arguments.Select(a => (a ?? "")?.ToString()).ToArray()));
            //LogUtils.WriteLog(new LogDto() { TypeName = "请求日志", Message = "\r\n" + requestParams });

            //invocation.Proceed();

            //string b = string.Format("方法执行完毕，返回结果：{0}.", invocation.ReturnValue?.ToString());

            //LogUtils.WriteLog(new LogDto() { TypeName = "请求日志", Message = b }); 
            #endregion
        }

        public string GetSession(string key)
        {
            byte[] sessionData;
            string temp = "";
            _httpContextAccessor.HttpContext.Session.TryGetValue(key, out sessionData);
            if (sessionData != null)
            {
                temp = YC.Common.ShareUtils.ConvertUtils.BytesToStringByUtf8(sessionData);
            }
            return temp;
        }


    }
}
