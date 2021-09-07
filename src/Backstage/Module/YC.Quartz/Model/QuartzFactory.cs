

using YC.QuartzModule.Model;
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
//   author：林滨
//------------------------------------------------
namespace YC.QuartzModule
{

    public class QuartzFactory
    {
        public static object quartzSync = new object();

        private static Queue<IScheduler> queueList = new Queue<IScheduler>();
        private static IScheduler scheduler;//全局变量，要不然工厂返回好像会是新的，新加入的job 无法获取得到

        public IQuartzRepository QZServiceRpository;
        public async Task QuartzFactory()
        {
            scheduler= await StdSchedulerFactory.GetDefaultScheduler();
            queueList = new Queue<IScheduler>();
            ThreadPool.QueueUserWorkItem(u =>
                {
                    while (true)
                    {
                        IScheduler schedulerObj = null;

                        if (queueList == null)
                        {
                            continue;
                        }

                        lock (queueList)
                        {
                            if(queueList.Count()>0)
                                schedulerObj = queueList.Dequeue();
                        }

                        if (queueList.Count() <= 0)
                        {
                            Thread.Sleep(30);
                        }


                    }
                });
            QZServiceRpository = new QuartzRepository(scheduler);

        }

        /// <summary>
        ///  Scheduler 保存到队列中
        /// </summary>
        /// <param name="iScheduler"></param>
        protected void SchedulerSaveToQueue(IScheduler iScheduler)
        {
            lock (queueList)
            {
                queueList.Enqueue(iScheduler);
            }
        }

        /// <summary>
        /// 是否存在调度程序
        /// </summary>
        /// <param name="inputScheduler">需要和队列中bidder调度程序</param>
        /// <param name="tempIScheduler">返回指定调用任务对象</param>
        /// <returns>是否存在在调用程序</returns>
        public bool IsExistsIScheduler(IScheduler inputScheduler, out IScheduler tempIScheduler)
        {
            tempIScheduler = null;
            List<IScheduler> list = queueList.Where(x => x.Equals(inputScheduler)).ToList();
            if (list.Count() > 0)
            {

                return false;
            }
            else
            {
                tempIScheduler = inputScheduler;
                return true;
            }

        }

        /// <summary>
        /// 同步加载初始化服务
        /// </summary>
        public void QuartzService()
        {

            scheduler.Start();// 服务调度器启动 这个必须放在前面，如果放在底部，第一个作业不执行

        }

        /// <summary>
        /// 异步加载初始化服务
        /// </summary>
        /// <returns></returns>
        public Task AsyncQuartzService()
        {
            return Task.Run(() =>
            {
                scheduler.Start();// 服务调度器启动 这个必须放在前面，如果放在底部，第一个作业不执行
            });

        }


    }
}
