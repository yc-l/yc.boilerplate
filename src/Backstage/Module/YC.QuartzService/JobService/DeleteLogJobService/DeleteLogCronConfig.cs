
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.QuartzService.Interface;

namespace YC.QuartzService.JobService.DeleteLogJobService
{
   public class DeleteLogCronConfig : ICronConfig
    {
       /// <summary>
       /// 
       /// </summary>
       public static string DeleteLogServiceJobTriggerCron
        {

           get {

               return "0/2 * * * * ? ";//2秒执行一次
           }
      }


    }
}
