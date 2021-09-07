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

    public class DeleteLogJobsConfig: IJobsConfig
    {
       
        Dictionary<string, string> cronDic = new Dictionary<string, string>();

        public DeleteLogJobsConfig() {
          
            string temp1 = DeleteLogCronConfig.DeleteLogServiceJobTriggerCron;

            cronDic.Add("DeleteLogServiceJobTriggerCron", temp1);

        }


        /// <summary>
        ///删除日志
        /// </summary>
        public QuartzJobsCollection WriteFileJob
        {

            get
            {

                IJobDetail writeFileJob = JobBuilder.Create<DeleteLogServiceJob>()  //创建一个作业
                    .WithIdentity("DeleteLogJob_Key", "DeleteLogJob_Group")//自定义
                    .Build();


                ITrigger writeFileTrigger = TriggerBuilder.Create()
                                            .WithIdentity("DeleteLogTrigger_key", "DeleteLogTrigger_Group").StartNow()
                                            //现在开始
                                            .WithCronSchedule(cronDic["DeleteLogServiceJobTriggerCron"])// 
                                            .Build();
           

                HashSet<ITrigger> triggerList = new HashSet<ITrigger>();
                triggerList.Add(writeFileTrigger);
                return new QuartzJobsCollection() { JobDetail = writeFileJob, Trigger = triggerList };
            }
        }


    }
}
