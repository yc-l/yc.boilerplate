using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YC.Model.DbEntity;

namespace YC.Model.SysDbEntity
{
    [Table("Sys_User")]
    public partial class SysUser : FullEntity<long>
    {
        #region Declarations
        [Display(Name = "员工编号")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]

        public string NO { get; set; }

        [Display(Name = "员工名称")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]

        public string Name { get; set; }

        [Display(Name = "登录账号")]
        [StringLength(64, ErrorMessage = "{0}不能超过64个字符！")]

        public string Account { get; set; }

        [Display(Name = "密码")]
        [StringLength(64, ErrorMessage = "{0}不能超过64个字符！")]
        public string Password { get; set; }

        [Display(Name = "性别")]

        public int Sex { get; set; }

        [Display(Name = "联系电话")]
        [StringLength(16, ErrorMessage = "{0}不能超过16个字符！")]
        [RegularExpression(@"1[3|4|5|6|7|8|9][0-9]{9}|\d{3}-\d{7,8}|\d{4}-\d{7,8}", ErrorMessage = "{0}格式错误！")]
        public string Mobile { get; set; }

        [Display(Name = "信箱")]
        [StringLength(64, ErrorMessage = "{0}不能超过64个字符！")]
        [RegularExpression(@"[A-Za-z0-9.%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "{0}格式错误！")]
        public string Email { get; set; }

        [Display(Name = "备注")]
        [StringLength(128, ErrorMessage = "{0}不能超过128个字符！")]
        public string Remark { get; set; }


        [Display(Name = "登录次数")]
        public int? LoginCount { get; set; }

        [Display(Name = "助记符")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]
        public string Memoni { get; set; }

           
        #endregion
    }

    public partial class SysUser
    {

        public SysUser() {
         
            this.SysRole = new List<SysRole>();

        }
        [Display(Name = "所属角色")]
        [NotMapped]
        public string[] RoleIDs { get; set; }

        [Display(Name = "可分配角色")]
        [NotMapped]
        public string[] AssignableRoleIDs { get; set; }

        [Display(Name = "所属部门")]
        [NotMapped]
        public string[] OrganizationIDs { get; set; }

        [Display(Name = "用户类型")]
        [NotMapped]
        public UserType UserType { get; set; }

        [Display(Name = "跨部门查看权限")]
        [NotMapped]
        public bool? InterDepartmental { get; set; }
        [NotMapped]
        [Display(Name = "所属部门")]
        public string OrgNames { get; set; }

      

        [NotMapped]
        public List<SysRole> SysRole { get; set; }
       
    }



}
