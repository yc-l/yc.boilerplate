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

namespace YC.ServiceWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseSerilogDefault();


    }
}
