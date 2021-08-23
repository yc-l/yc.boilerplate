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
    [Table("Sys_RolePermission")]
    public class SysRolePermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("主键Id")]
        public long Id { get; set; }
        [DisplayName("角色Id")]
        public long RoleId { get; set; }
        [DisplayName("权限Id")]
        public long PermissionId { get; set; }

        [DisplayName(" 创建ID")]
        public long CreatorUserId { get; set; }



        [DisplayName(" 创建时间")]
        public DateTime? CreationTime { get; set; }

      

    }
}
