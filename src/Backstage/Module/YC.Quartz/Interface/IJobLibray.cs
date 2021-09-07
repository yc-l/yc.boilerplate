using YC.QuartzModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//------------------------------------------------
//   服务资源池接口
// 
//   author：林滨
//------------------------------------------------

namespace YC.Quartz.Interface
{
   public interface IJobLibray
    {
      List<QuartzJobsCollection> GetJobList();
    }
}
