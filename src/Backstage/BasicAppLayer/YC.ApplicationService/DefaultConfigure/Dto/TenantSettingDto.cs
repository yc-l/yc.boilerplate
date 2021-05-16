using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.ApplicationService.DefaultConfigure
{
    public class TenantSettingDto
    {
        /// <summary>
        /// 
        /// </summary>
        public bool MultiTnancy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DefaultTenantId { get; set; }
        public string DefaultDbConnectionString { get; set; }
        public int DataType { get; set; }
        private string _TenantKeyName;
        public string TenantKeyName { get { return _TenantKeyName; } set { _TenantKeyName = value; } }
        /// <summary>
        /// 
        /// </summary>
        public List<TenantInfo> TenantList { get; set; }
    }
    public class TenantInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        /// 通用后台管理系统
        /// </summary>
        public string TenantName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActived { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DbConnectionString { get; set; }
        public int DataType { get; set; }
    }

}
