using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Common;

namespace YC.ServiceWebApi.WebHostBuilderExtensions
{
    public static class InitHostingExtensions
    {
        /// <summary>
        /// 自定义初始化相关的附加任务
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="test"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseInitCall(this IWebHostBuilder hostBuilder, Action action) {

            action?.Invoke();
            //action?.BeginInvoke();处理异步
            return hostBuilder;
        }

        public static void Call(string info) {

            //custom work
            //LogUtils.WriteLog(new LogDto() { TypeName = "TestLog", Message = info });
        }
    }

   

   
}
