using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YC.Model.SysDbEntity
{
    [Table("Sys_UserSysOrganization")]
    public partial class SysUserSysOrganization
    {
        #region Declarations
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("主键Id")]
        public long Id { get; set; }

        [Display(Name = "用户Id")]
        public int SysUser_ID { get; set; }

        [Display(Name = "组织Id")]
        public long SysOrganization_ID { get; set; }

        #endregion
    }
}
