using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using YC.Log.Serilog;
using YC.QuartzService.Interface;
using YC.QuartzService.JobService.CreateDirJobService;
using YC.QuartzService.JobService.DeleteLogJobService;
using YC.QuartzService.JobService.WriteFileJobService;
using YC.QuartzServiceModule;
using YC.ServiceWebApi.WebHostBuilderExtensions;

namespace YC.ServiceWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration(config =>
            {
                // config.AddJsonFile($"appsetting.json", optional: false, reloadOnChange: true);
                //如果要当场获取并执行其他操作可以在这如下操作
                var tempconfig = config.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true).Build();
                //比如使用 有些ioc 不方便操作，需要使用工厂类根据配置类别，动态处理
            }).UseStartup<Startup>().UseSerilogDefault().UseInitCall(() =>
            {
                //这里加入自定义相关服务
                InitHostingExtensions.Call("这是一个测试");
            });
    }
}