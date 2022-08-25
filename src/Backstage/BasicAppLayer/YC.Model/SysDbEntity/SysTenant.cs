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
    [Table("Sys_Tenant")]
    [Display(Name = "租户管理")]
    public class SysTenant : CreateEntity<long>
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[DisplayName("主键Id")]
        //public long Id { get; set; }

        //[DisplayName("租户Id")]
        //public int TenantId { get; set; }

        [DisplayName("租户名称")]
        public string TenantName { get; set; }

        [DisplayName("数据库类别")]
        public int DbType { get; set; }

        [StringLength(300, ErrorMessage = "{0}不能超过300个字符！")]
        [DisplayName("数据库连接字符串")]
        public string DbConnectionString { get; set; }

        [DisplayName("是否默认租户")]
        public bool IsDefaultTenant { get; set; }
    }
}