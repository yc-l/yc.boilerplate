using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YC.Core.Autofac;
//------------------------------------------------
//   演示任务 定时写文件 任务
// 
//   author：林宣名
//------------------------------------------------

namespace YC.QuartzService.JobService.WriteFileJobService
{
   public class FileServiceJob : IJob, IDependencyInjectionSupport
    {
    //    IdleBus<IFreeSql> _ib;
    //    public FileServiceJob(IdleBus<IFreeSql> ib)
    //    {
    //        _ib = ib;
    //    }
        public Task Execute(IJobExecutionContext context)
        {

            FileWrite();
            return Task.CompletedTask;
        }

        public void FileWrite()
        {
            LogHelper.WriteLog(DateTime.Now.ToString());

        }
    }
}
