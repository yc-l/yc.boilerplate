using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.ApplicationService.DefaultConfigure
{
    public class DefaultAppConfigDto
    {
        public bool IsDebug { get; set; }
        public string ExceptionKey { get; set; }
        public string DefaultExceptionString { get; set; }
        public int CacheExpire { get; set; }

        public int TokenExpire { get; set; }

        public int RefreshTokenExpire { get; set; }
        public string TokenKeyName { get; set; }

        public string TokenIssuer { get; set; }
        public string TokenAudience { get; set; }

       


    }
}
