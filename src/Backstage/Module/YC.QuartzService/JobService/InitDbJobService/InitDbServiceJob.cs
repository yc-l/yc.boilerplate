using Dapper;
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
using YC.Common.ShareUtils;
using YC.Core.Autofac;
using YC.DapperFrameWork;
using YC.Model.SysDbEntity;
//------------------------------------------------
//   演示任务 定时写文件 任务
// 
//   author：林宣名
//------------------------------------------------

namespace YC.QuartzService.JobService.DeleteLogJobService
{
   public class InitDbServiceJob:IJob
    {


        public async  Task Execute(IJobExecutionContext context)
        {
           var tenantList = DefaultConfig.TenantSetting.TenantList;
            string sqlFile = System.Environment.CurrentDirectory + @"\DbSqlFile\bigdatadb.sql";
            tenantList = tenantList.Where(x => x.TenantId == 1 || x.TenantId == 2).ToList();
            foreach (var t in tenantList) {

                if (t.TenantId == 2) {
                    sqlFile = System.Environment.CurrentDirectory + @"\DbSqlFile\bigdatadb2.sql";
                }

            var logInfo=await DoWork(t.TenantName, t.DbConnectionString, sqlFile);
            }
            //return Task.CompletedTask;
        }

        public async Task<string> DoWork(string name,string connStr,string sqlFilePath) {

            DapperFrameWork.IUnitOfWork unitOfWork = new DefaultUnitOfWork(connStr);
            var connection = unitOfWork.Connection;
            string logInfo = "";
            string sqlContent = "";
            var isRead = FileUtils.ReadFile(sqlFilePath, out sqlContent);
            if (isRead && !string.IsNullOrWhiteSpace(sqlContent))
            {
                try
                {
                    var count = await connection.ExecuteAsync(sqlContent);
                    logInfo = $"当前时间：{DateTime.Now},当前租户：{name} , 执行数据库重置返回记录数：{count}.\r\n";
                    LogUtils.WriteLog(new LogDto() { TypeName = "重置数据库日志", Message = logInfo });
                }
                catch (Exception ex)
                {

                    LogUtils.WriteLog(new LogDto() { TypeName = "重置数据库错误日志", Message = $"时间：{DateTime.Now}，错误：" + ex.ToString() });
                }
                finally {
                    unitOfWork.Dispose();
                }
            }
            return logInfo;
        }

    }
}
