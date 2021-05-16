using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.ApplicationService;

namespace YC.ServiceWebApi.Filter.Dto
{
    public class RequestInfoDto
    {

        public string Key { get; set; }
        /// <summary>
        /// IP
        /// </summary>

        public string IP { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>

        public string Browser { get; set; }

        /// <summary>
        /// 操作系统
        /// </summary>

        public string Os { get; set; }

        /// <summary>
        /// 设备
        /// </summary>

        public string Device { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>

        public string BrowserInfo { get; set; }

        /// <summary>
        /// 耗时（毫秒）
        /// </summary>
        public long ElapsedMilliseconds { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string ParamsString { get; set; }
        /// <summary>
        /// 开始执行时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束执行时间
        /// </summary>
        public DateTime StopTime{ get; set; }

        public bool ResponseState { get; set; }

        public string ResponseData { get; set; }
        
    }
}
