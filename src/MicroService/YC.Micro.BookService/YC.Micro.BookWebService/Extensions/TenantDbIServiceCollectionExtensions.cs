using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using YC.Core;
using YC.Core.Autofac;
using YC.FreeSqlFrameWork;
using YC.Micro.Configuration;

namespace YC.Micro.BookWebService.ServiceCollectionExtensions
{
    public static class TenantDbIServiceCollectionExtensions
    {
        public static IdleBus<IFreeSql> AddTenantDb(this IServiceCollection services)
        {

            IdleBus<IFreeSql> ib = new IdleBus<IFreeSql>(TimeSpan.FromMinutes(10));
            SetTenantDb(services, ib,
                       DefaultConfig.TenantSetting.DefaultTenantId.ToString(),
                       DefaultConfig.TenantSetting.DefaultDbType,
                       DefaultConfig.TenantSetting.DefaultDbConnectionString);
            if (DefaultConfig.TenantSetting.MultiTnancy)
            {
                foreach (var i in DefaultConfig.TenantSetting.TenantList)
                {
                    SetTenantDb(services, ib, i.TenantId.ToString(), i.DbType, i.DbConnectionString);
                }
            }
            return ib;
          
        }

        private static void SetTenantDb(IServiceCollection services,IdleBus<IFreeSql> ib, string tenantKey, int dataType, string connectionString)
        {
            ib.TryRegister(tenantKey, () =>
            {
                #region FreeSql
                var freeSqlBuilder = new FreeSqlBuilder()
                        .UseConnectionString((DataType)dataType, connectionString)
                        .UseLazyLoading(false)
                        .UseNoneCommandParameter(true);

                #region 监听所有命令

                freeSqlBuilder.UseMonitorCommand(cmd => { }, (cmd, traceLog) =>
                {
                    Console.WriteLine($"{cmd.CommandText}\r\n");
                });

                #endregion

                var fsql = freeSqlBuilder.Build();
                //fsql.GlobalFilter.Apply<IEntitySoftDelete>("SoftDelete", a => a.IsDeleted == false);


               

                #region 监听Curd操作
                fsql.Aop.CurdBefore += (s, e) =>
                     {
                         Console.WriteLine($"{e.Sql}\r\n");
                     };

                #endregion

                #region 审计数据
                //这里是freesql 自己aop 注入事件回调，每次操作回来监听
                //fsql.Aop.AuditValue += (s, e) =>
                //{
                //   //请求管道session 中获取对应的内容
                //    var tenant = AutofacUtils.Resolve<ITenant>();
                //    var appManager = AutofacUtils.Resolve<IAppManager>();

                //    if (tenant == null || tenant.TenantId <= 0)
                //    {
                //        return;
                //    }

                //    if (appManager == null || appManager.LoginUser == null)
                //    {
                //        return;
                //    }

                //    if (e.AuditValueType == FreeSql.Aop.AuditValueType.Insert)
                //    {
                //        switch (e.Property.Name)
                //        {
                //            case "CreatorUserId":
                //                e.Value = appManager.LoginUser.Id;
                //                break;
                //            case "CreationTime":
                //                e.Value = DateTime.Now;
                //                break;
                //            case "TenantId":
                //                e.Value = tenant.TenantId;
                //                break;
                //        }
                //    }
                //    else if (e.AuditValueType == FreeSql.Aop.AuditValueType.Update)
                //    {
                //        switch (e.Property.Name)
                //        {
                //            case "LastModificationTime":
                //                e.Value = DateTime.Now;
                //                break;
                //            //case "LastModifierUserId":
                //            //    e.Value = appManager._loginUser.Id;
                //            //    break;
                //            case "TenantId":
                //                e.Value = tenant.TenantId;
                //                break;
                //        }
                //    }
                //};
                #endregion
                #endregion

                return fsql;
            });
        }
    }
}
