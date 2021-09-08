
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.QuartzService.Interface;
//------------------------------------------------
//   演示任务 定时创建 文件夹 获取任务执行时间表达式
// 
//   author：林宣名
//------------------------------------------------

namespace YC.QuartzService.JobService.CreateDirJobService
{
   public class CreateDirFolderCronConfig:ICronConfig
    {
       /// <summary>
       /// 
       /// </summary>
       public static string CreateDirFolderServiceJobTriggerCron
        {

           get {
                //return "10 * * * * ? ";
               return "0/5 * * * * ? *";
           }
      }


    }
}
