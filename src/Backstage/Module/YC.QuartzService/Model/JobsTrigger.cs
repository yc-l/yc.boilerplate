using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.QuartzServiceModule.Model
{
 public  class JobsTrigger
    {
        /// <summary>
        /// 任务标识
        /// </summary>
        public JobKey JobKey { get; set; }

        /// <summary>
        /// 触发器
        /// </summary>
        public TriggerKey TriggerKey { get; set; }

        /// <summary>
        /// 触发器状态
        /// </summary>
        public TriggerState TriggerState { get;set; }

    }
}
