

using YC.QuartzServiceModule.Model;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


//------------------------------------------------
//   服务工厂
// 
//   author：林宣名
//------------------------------------------------
namespace YC.QuartzServiceModule
{

    public class QuartzFactory
    {
        public static object quartzSync = new object();

        private static Queue<IScheduler> queueList = new Queue<IScheduler>();
        private static IScheduler scheduler;
        public IQuartzRepository quartzServiceRpository;
        public async Task Init()
        {
            scheduler =await StdSchedulerFactory.GetDefaultScheduler();//全局变量，要不然工厂返回好像会是新的，新加入的job 无法获取得到
            quartzServiceRpository = new QuartzRepository(scheduler);
        }

        /// <summary>
        /// 同步加载初始化服务
        /// </summary>
        public async Task QuartzService()
        {

           await scheduler.Start();// 服务调度器启动 这个必须放在前面，如果放在底部，第一个作业不执行

        }

     


    }
}
