
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.QuartzService.Interface;

namespace YC.QuartzService.JobService.WriteFileJobService
{
   public class WriteFileCronConfig: ICronConfig
    {
       /// <summary>
       /// 
       /// </summary>
       public static string WriteFileServiceJobTriggerCron
        {

           get {

               return "0/1 * * * * ? *";
           }
      }


    }
}
