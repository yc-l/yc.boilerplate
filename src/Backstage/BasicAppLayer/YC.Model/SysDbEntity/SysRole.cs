using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YC.Model.DbEntity;

namespace YC.Model.SysDbEntity
{
    [Table("Sys_Role")]
    [Display(Name = "角色表")]
    public class SysRole : ModifiedEntity<long>
    {
        #region Declarations
        [Display(Name = "名称")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]
      
        public string Name { get; set; }

        [Display(Name = "助记符")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]
        public string Memoni { get; set; }

        [Display(Name = "排序")]
        public int? Sort { get; set; }

        [NotMapped]
        public SysRolePermission SysRolePermission { get; set; }

        #endregion

        #region 自定义属性，即由数据实体扩展的实体  
        [NotMapped]
        public virtual ICollection<SysUser> SysUsers { set; get; }
        [NotMapped]
        public string UserIDs { get; set; }
        [NotMapped]
        public string MenuOptionIDs { get; set; }

        [NotMapped]
        public List<long> PermissionIds { get; set; }
        #endregion
    }
}
