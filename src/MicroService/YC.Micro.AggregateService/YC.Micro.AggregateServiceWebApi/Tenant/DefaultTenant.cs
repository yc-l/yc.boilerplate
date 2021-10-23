using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Core;
using YC.Micro.Configuration;

namespace YC.Micro.AggregateServiceWebApi.Tenant
{

    public class DefaultTenant : ITenant
    {

        public DefaultTenant()
        {
            this.TenantId = DefaultConfig.TenantSetting.DefaultTenantId;
            this.TenantDbType = DefaultConfig.TenantSetting.DefaultDbType;
            this.TenantDbString = DefaultConfig.TenantSetting.DefaultDbConnectionString;
        }

        public int TenantId { get; set; }
        public int TenantDbType { get; set; }
        public string TenantDbString { get; set; }


    }
}
