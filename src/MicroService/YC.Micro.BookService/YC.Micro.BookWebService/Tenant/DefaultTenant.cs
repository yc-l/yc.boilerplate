using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Core;
using YC.Micro.Configuration;

namespace YC.Micro.BookWebService.Tenant
{

    public class DefaultTenant : ITenant
    {

        public DefaultTenant()
        {
            var tenant = DefaultConfig.TenantSetting.TenantList.Where(x => x.TenantId == 1).FirstOrDefault();
            this.TenantId = tenant.TenantId;
            this.TenantDbType = tenant.DbType;
            this.TenantDbString = tenant.DbConnectionString;
        }

        public int TenantId { get; set; }
        public int TenantDbType { get; set; }
        public string TenantDbString { get; set; }


    }
}
