using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Common.ShareUtils;
using YC.QuartzService.Interface;
using YC.QuartzService.JobService.CreateDirJobService;
using YC.QuartzService.JobService.WriteFileJobService;
using YC.QuartzServiceModule;
using YC.QuartzServiceModule.Model;

namespace QuartzTest
{
    public class Program
    {
        Dictionary<string, JobKey> jobKeyDic = new Dictionary<string, JobKey>();
        public List<ShowModel> CacheList = new List<ShowModel>();
        public static JobKey temp;
        public static QuartzFactory quartzFactory;
        public static async Task Main(string[] args)
        {
            quartzFactory = new QuartzFactory();
            await quartzFactory.Init();
            await quartzFactory.QuartzService();
            string error = "";

            List<IJobLibray> jobLibraysList = new List<IJobLibray>();
            jobLibraysList.Add(new CreateDirFolderJobLibray());
            jobLibraysList.Add(new WriteFileJobLibray());

            var list = await quartzFactory.quartzServiceRpository.DefaultRunningServer(jobLibraysList);
            Show();
            while (true)
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("0.暂停任务");
                Console.WriteLine("1.恢复任务");
                Console.WriteLine("2.删除任务");
                Console.WriteLine("3.停止所有任务");
             
                Console.WriteLine("------------------------------------------------");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    var jobList = quartzFactory.quartzServiceRpository.JobTriggerServerList;//重新获取一个对象，JobTriggerServerList每次操作都会在变化,需要深拷贝，浅拷贝引用无效

                    var JobKeyList = new List<JobKey>();
                    JobKeyList.AddRange(jobList.Select(x => x.JobKey));//这个是深拷贝
                    for (int j = 0; j < JobKeyList.Count; j++)
                    {
                        await quartzFactory.quartzServiceRpository.ResumeQuartzServer(JobKeyList[j]);
                    }

                }
                else if (input == "0")
                {
                    var jobList = quartzFactory.quartzServiceRpository.JobTriggerServerList;//重新获取一个对象，JobTriggerServerList每次操作都会在变化,需要深拷贝，浅拷贝引用无效

                    var JobKeyList = new List<JobKey>();
                    JobKeyList.AddRange(jobList.Select(x => x.JobKey));//这个是深拷贝
                    for (int j = 0; j < JobKeyList.Count; j++)
                    {
                        await quartzFactory.quartzServiceRpository.PauseQuartzServer(JobKeyList[j]);
                    }
                }

                else if (input == "2")
                {
                    var jobList = quartzFactory.quartzServiceRpository.JobTriggerServerList;//重新获取一个对象，JobTriggerServerList每次操作都会在变化,需要深拷贝，浅拷贝引用无效

                    var JobKeyList = new List<JobKey>();
                    JobKeyList.AddRange(jobList.Select(x => x.JobKey));//这个是深拷贝
                    for (int j = 0; j < JobKeyList.Count; j++)
                    {
                        await quartzFactory.quartzServiceRpository.DeleteQuartzServer(JobKeyList[j]);
                    }

                }
                else if (input == "3")
                {
                    await quartzFactory.quartzServiceRpository.StopAllQuartzServer();
                }


                Show();
            }

            Console.ReadLine();


        }

        public static void Show()
        {


            List<JobsTrigger> jobsTriggerList = quartzFactory.quartzServiceRpository.GetALLQuartzServer();

            foreach (var i in jobsTriggerList)
            {

                ShowModel temp = new ShowModel();
                temp.JobKeyName = i.JobKey.Name;
                temp.TriggerName = i.TriggerKey.Name;
                temp.State = i.TriggerState.ToString();
                //temp.JobsTrigger = i;
                Console.WriteLine(temp.ToJson());
            }






        }
    }

    /// <summary>
    /// 演示Demo
    /// </summary>
    public class ShowModel
    {

        public string JobKeyName { get; set; }
        public string TriggerName { get; set; }
        public string State { get; set; }

        public JobsTrigger JobsTrigger { get; set; }

    }
}
