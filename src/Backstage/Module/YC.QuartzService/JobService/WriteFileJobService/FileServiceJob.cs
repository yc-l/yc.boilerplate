using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//------------------------------------------------
//   演示任务 定时写文件 任务
// 
//   author：林宣名
//------------------------------------------------

namespace YC.QuartzService.JobService.WriteFileJobService
{
   public class FileServiceJob:IJob
    {

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
