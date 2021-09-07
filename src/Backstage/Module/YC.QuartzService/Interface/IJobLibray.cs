using YC.QuartzServiceModule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//------------------------------------------------
//   服务资源池接口
// 
//   author：林宣名
//------------------------------------------------

namespace YC.QuartzService.Interface
{
   public interface IJobLibray
    {
      List<QuartzJobsCollection> GetJobList();
    }
}
