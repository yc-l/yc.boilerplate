using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YC.Model.DbEntity;

namespace YC.Model.SysDbEntity
{
    [Table("Sys_UserSysRole")]
    public class SysUserSysRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("主键Id")]
        public long Id { get; set; }

        #region Declarations
        [Display(Name = "角色Id")]

        public long? SysRole_ID { get; set; }
        [Display(Name = "用户ID")]

        public long? SysUser_ID { get; set; }
        [Display(Name = "角色类别")]

        public int SysRole_Type { get; set; }
        #endregion

    }
}
