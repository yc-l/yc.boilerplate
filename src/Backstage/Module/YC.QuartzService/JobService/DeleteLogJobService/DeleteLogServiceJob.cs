using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YC.ApplicationService;
using YC.Common;
using YC.Core.Autofac;
using YC.FreeSqlFrameWork;
using YC.Model.SysDbEntity;
//------------------------------------------------
//   演示任务 定时写文件 任务
// 
//   author：林宣名
//------------------------------------------------

namespace YC.QuartzService.JobService.DeleteLogJobService
{
   public class DeleteLogServiceJob:IJob
    {
        //IdleBus<IFreeSql> _ib;//没有接管DI 无法注入操作
        //public DeleteLogServiceJob(IdleBus<IFreeSql> ib) {
        //    _ib = ib;
        //}


        public  Task Execute(IJobExecutionContext context)
        {
            //具体业务代码
            //LogUtils.WriteLog(new LogDto() { TypeName = "记录日志", Message = DateTime.Now.ToString() });
          var _ib=  AutofacUtils.GetService<IdleBus<IFreeSql>>();
            var fsql = _ib.Get("1");
            var list = new List<IFreeSql>();
            list.Add(_ib.Get("1"));
            list.Add(_ib.Get("2"));
            foreach (var f in list) {
                var count = f.Select<SysAuditLog>().Count();
              
                    // var deleteCount= fsql.Delete<SysAuditLog>().Where(x => x.Id > 100).ExecuteAffrows();
                    var addCount = f.Insert<SysAuditLog>(new SysAuditLog() { Key = "test" + DateTime.Now }).ExecuteAffrows();
               
            }
          
            return Task.CompletedTask;
        }

     
    }
}
