using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.ApplicationService;
using YC.Common.ShareUtils;
using YC.QuartzService.Interface;
using YC.QuartzService.JobService.CreateDirJobService;
using YC.QuartzService.JobService.DeleteLogJobService;
using YC.QuartzService.JobService.WriteFileJobService;
using YC.QuartzServiceModule;
using YC.QuartzServiceModule.Model;
using YC.QuartzTest;

namespace QuartzTest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            DefaultConfig.JsonConfig = DefaultConfig.GetConfigJson(DefaultConfig.dbConfigFilePath);
            var qtest = new QuartFactoryTest();
            await qtest.Init();
            Console.ReadLine();


        }

       
    }

   
}
