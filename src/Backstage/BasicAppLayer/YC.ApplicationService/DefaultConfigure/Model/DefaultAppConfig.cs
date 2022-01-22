using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.ApplicationService.DefaultConfigure
{
    public class DefaultAppConfig
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

        /// <summary>
        /// nginx 代理映射ip 范围
        /// </summary>
        public string NginxAgentIP { get; set; }

        /// <summary>
        /// 是否校验Token唯一性【单点登录限制】
        /// </summary>
        public bool VerifyTokenUniqueness { get; set; }

        public string MongoDbString { get; set; }
        public string MongoDbName { get; set; }

        public List<AllowedNoTokenUrl> AllowedNoTokenUrls { get; set; }

        public List<FilterUrl> AllowedNoPermissionUrls { get; set; }

        public bool QuartzSeverIsWork { get; set; }
        public string DefaultVerifyCode { get; set; }
        public int UseCacheModuleType { get; set; }
    }
    public class AllowedNoTokenUrl
    {

        public string Url { get; set; }
        public bool NoCheckTenant { get; set; }
    }

    public class FilterUrl
    {

        public string Url { get; set; }
    
    }
}
