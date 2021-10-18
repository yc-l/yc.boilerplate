using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Core.Autofac;

namespace YC.Core
{
    
   public interface ITenant: IDependencyInjectionSupport
    {
        public int TenantId { get; set; }
        public int TenantDbType { get; set; }
        public string TenantDbString{ get; set; }
    }
}
