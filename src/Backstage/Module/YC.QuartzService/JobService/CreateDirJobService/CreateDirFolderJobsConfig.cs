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
namespace YC.QuartzService.JobService.CreateDirJobService
{

    public class CreateDirFolderJobsConfig : IJobsConfig
    {

        Dictionary<string, string> cronDic = new Dictionary<string, string>();

        public CreateDirFolderJobsConfig()
        {

            string cron = CreateDirFolderCronConfig.CreateDirFolderServiceJobTriggerCron;

            cronDic.Add("CreateDirFolderServiceJobTriggerCron", cron);

        }


        /// <summary>
        /// 创建文件夹定时服务
        /// </summary>
        public QuartzJobsCollection CreateDirFolderJob
        {

            get
            {

                IJobDetail createDirFolderJob = JobBuilder.Create<CreateDirFolderJob>()  //创建一个作业
                    .WithIdentity("CreateDirFolderJob_Key", "CreateDirFolderJob_Group")//自定义
                    .UsingJobData("CreateFloderName", DateTime.Now.ToString("yyyy-MM-dd") + new Random().Next(999))//这个事件因为只调用一次，所以传过去的值就只是为一个一个
                    .Build();

                ITrigger createDirFolderTrigger = TriggerBuilder.Create()
                                            .WithIdentity("CreateDirFolderTrigger_key", "CreateDirFolderTrigger_Group").StartNow()
                                            //现在开始
                                            .WithCronSchedule(cronDic["CreateDirFolderServiceJobTriggerCron"])// 
                                            .Build();

                HashSet<ITrigger> triggerList = new HashSet<ITrigger>();
                triggerList.Add(createDirFolderTrigger);
                return new QuartzJobsCollection() { JobDetail = createDirFolderJob, Trigger = triggerList };
            }
        }


    }
}
