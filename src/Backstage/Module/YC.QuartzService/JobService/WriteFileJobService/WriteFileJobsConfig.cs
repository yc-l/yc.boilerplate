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
namespace YC.QuartzService.JobService.WriteFileJobService
{

    public class WriteFileJobsConfig : IJobsConfig
    {
        //时间表达式，执行任务时间
        public static string cron = "0/5 * * * * ? *";


        /// <summary>
        /// 文件操作写入文件定时服务
        /// </summary>
        public  QuartzJobsCollection WriteFileJob
        {

            get
            {

                IJobDetail writeFileJob = JobBuilder.Create<FileServiceJob>()  //创建一个作业
                    .WithIdentity("WriteFileServiceJob_Key", "WriteFileServiceJob_Group")//自定义
                    .Build();


                ITrigger writeFileTrigger = TriggerBuilder.Create()
                                            .WithIdentity("WriteFileTrigger_key", "WriteFileTrigger_Group").StartNow()
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
