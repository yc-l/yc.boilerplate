using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Model.SysDbEntity
{
    [Table("Sys_AuditLog")]
    [Display(Name = "审计日志")]
    public class SysAuditLog { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("主键Id")]
        public long Id { get; set; }

        [DisplayName("拦截Key")]
        public string Key { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [DisplayName("IP")]
        public string IP { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        [DisplayName("浏览器")]
        public string Browser { get; set; }

        /// <summary>
        /// 操作系统
        /// </summary>
        [DisplayName("操作系统")]
        public string Os { get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        [DisplayName("设备")]
        public string Device { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        [DisplayName("浏览器信息")]
        [StringLength(300, ErrorMessage = "{0}不能超过300个字符！")]
        public string BrowserInfo { get; set; }

        /// <summary>
        /// 耗时（毫秒）
        /// </summary>
        [DisplayName("耗时（毫秒）")]
        public long ElapsedMilliseconds { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        [DisplayName("租户Id")]
        public int TenantId { get; set; }

        [DisplayName("用户id")]
        public long UserId { get; set; }
        [DisplayName("用户账号")]
        public string UserAccount { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        [DisplayName("请求参数")]
        public string ParamsString { get; set; }
        /// <summary>
        /// 开始执行时间
        /// </summary>
        [DisplayName("开始执行时间")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束执行时间
        /// </summary>
        [DisplayName("结束执行时间")]
        public DateTime StopTime { get; set; }

        [DisplayName("请求方式")]
        public string RequestMethod { get; set; }

        [DisplayName("请求Api")]
        public string RequestApi { get; set; }

        [DisplayName(" 创建时间")]
        public DateTime? CreationTime { get; set; }

        [DisplayName(" 请求是否成功")]
        public bool ResponseState { get; set; }
        [DisplayName(" 返回数据")]
        public string ResponseData { get; set; }

    }
}
