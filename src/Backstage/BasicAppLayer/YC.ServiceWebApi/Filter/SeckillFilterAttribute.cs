using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YC.Common.ShareUtils;
using YC.Core;
using YC.Core.Cache;

namespace YC.ServiceWebApi.Filter
{   
    
    /// <summary>
    /// 秒杀场景拦截 缓存和cookie
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class SeckillFilterAttribute:ActionFilterAttribute, IFilterMetadata,IOrderedFilter
    {
        private static int tempState=0;
        public ICacheManager _cacheManager;
        public SeckillFilterAttribute(ICacheManager cacheManager) {
            _cacheManager = cacheManager;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
           

        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            
            string ip=IPUtils.GetIP(context?.HttpContext?.Request);
            var token = context.HttpContext.Request.Headers["Authorization"];//在全局action中校验了
            if (string.IsNullOrWhiteSpace(token)) {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}:token为空，拦截！");
                context.Result = new JsonResult(ApiResult.NotOk("token为空，拦截！"));
                return;
            }
            string key = EncryptUtils.MD5(ip) + EncryptUtils.Encrypt(token);
            string value = DateTime.Now.ToString();
           
           var cacheValue = RedisHelper.Get(key);
            context.HttpContext.Request.Cookies.TryGetValue(key, out string cookieValue);
          
            //cookie 不为null
            if (!string.IsNullOrEmpty(cookieValue)) {

                bool isExceed = IsExceed(Convert.ToDateTime(value), Convert.ToDateTime(cookieValue));
                if (isExceed)
                {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}:请求过快，cookies拦截！");
                    context.Result = new JsonResult(ApiResult.NotOk("请求过快，cookies拦截！"));
                    return;
                }
                else {
                    if (cacheValue != null)
                    {
                        RedisHelper.Set(key, DateTime.Now.ToString());//修改缓存
                    }
                    else {
                        RedisHelper.Set(key, DateTime.Now.ToString());//存储缓存
                    }
                    context.HttpContext.Response.Cookies.Delete(key);
                    context?.HttpContext?.Response.Cookies.Append(key, DateTime.Now.ToString(), new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(1)
                    });
                    return;
                }
             
            }
            //校验cache
            if (cacheValue != null) {
                DateTime d1= Convert.ToDateTime(value);
                DateTime d2 = Convert.ToDateTime(cacheValue.ToString());
                bool isExceed = IsExceed(d1, d2);
                if (isExceed)
                {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}:请求过快，cache拦截！");
                    context.Result = new JsonResult(ApiResult.NotOk("请求过快，cache拦截！"));
                }
                else
                {
                    RedisHelper.Set(key, DateTime.Now.ToString());//修改缓存
                    context?.HttpContext?.Response.Cookies.Append(key, value, new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(1)
                    });
                }
                return;
            }
            RedisHelper.Set(key, DateTime.Now.ToString());//存储缓存
            context?.HttpContext?.Response.Cookies.Append(key, DateTime.Now.ToString(), new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1)
            });
            
        }

        public bool IsExceed(DateTime now,DateTime old) {
            bool result = false;
          TimeSpan timeSpan=  DateUtils.DateDiff(now, old);
            var tsm = timeSpan.TotalMilliseconds;
            if (tsm < 1000)//小于1秒，说明是恶意提交请求，需要拦截
            {
                result = true;
            }
            else {
                result = false;
            }

            return result;
        }

        #region 闲置代码

        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>      
        protected static void SetCookies(string key, string value, int minutes = 30)
        {
            //HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            //{
            //    Expires = DateTime.Now.AddMinutes(minutes)
            //});
        }
        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        protected void DeleteCookies(string key)
        {
            //HttpContext.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        protected void GetCookies(string key)
        {
            //HttpContext.Request.Cookies.TryGetValue(key, out string value);
            //if (string.IsNullOrEmpty(value))
            //    value = string.Empty;
            //return value;
        } 
        #endregion
    }
}
