using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.QuartzServiceModule.Model
{
  public  class QuartzJobsCollection
    {
      /// <summary>
      /// 任务
      /// </summary>
      public IJobDetail JobDetail { get; set; }
      
      /// <summary>
      /// 触发器
      /// </summary>
      public HashSet<ITrigger> Trigger { get; set; }

      
    }
}
