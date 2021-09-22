using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Quartz.Util;
using System.Configuration;
using YC.QuartzService.Interface;
using YC.QuartzServiceModule.Model;



//------------------------------------------------
//   服务配置
// 
//   author：林宣名
//------------------------------------------------
namespace YC.QuartzService.JobService.DeleteLogJobService
{

    public class InitDbJobsConfig: IJobsConfig
    {   //时间表达式，执行任务时间 两分钟执行一次
        public static string cron = "0 0/2 * * * ? ";

        /// <summary>
        ///删除日志
        /// </summary>
        public  QuartzJobsCollection InitDbJob
        {

            get
            {

                IJobDetail writeFileJob = JobBuilder.Create<InitDbServiceJob>()  //创建一个作业
                    .WithIdentity("InitDbJob_Key", "InitDbJob_Group")//自定义
                    .Build();


                ITrigger writeFileTrigger = TriggerBuilder.Create()
                                            .WithIdentity("InitDbJobTrigger_key", "InitDbJobTrigger_Group").StartNow()
                                            //现在开始
                                            .WithCronSchedule(cron)// 
                                            .Build();
           

                HashSet<ITrigger> triggerList = new HashSet<ITrigger>();
                triggerList.Add(writeFileTrigger);
                return new QuartzJobsCollection() { JobDetail = writeFileJob, Trigger = triggerList };
            }
        }


    }
}
