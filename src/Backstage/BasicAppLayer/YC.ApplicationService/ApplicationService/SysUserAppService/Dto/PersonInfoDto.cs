using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.ApplicationService.ApplicationService.SysUserAppService.Dto
{
    public class PersonInfoDto
    {
        #region Declarations

        public string Id { get; set; }

        [Display(Name = "员工编号")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]
        public string? NO { get; set; }

        [Display(Name = "员工名称")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]
        public string? Name { get; set; }

        [Display(Name = "登录账号")]
        [StringLength(64, ErrorMessage = "{0}不能超过64个字符！")]
        public string? Account { get; set; }

        [Display(Name = "密码")]
        [StringLength(64, ErrorMessage = "{0}不能超过64个字符！")]
        public string? Password { get; set; }

        [Display(Name = "性别")]
        public int? Sex { get; set; }

        [Display(Name = "联系电话")]
        [StringLength(16, ErrorMessage = "{0}不能超过16个字符！")]
        [RegularExpression(@"1[3|4|5|6|7|8|9][0-9]{9}|\d{3}-\d{7,8}|\d{4}-\d{7,8}", ErrorMessage = "{0}格式错误！")]
        public string? Mobile { get; set; }

        [Display(Name = "信箱")]
        [StringLength(64, ErrorMessage = "{0}不能超过64个字符！")]
        [RegularExpression(@"[A-Za-z0-9.%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "{0}格式错误！")]
        public string? Email { get; set; }

        [Display(Name = "备注")]
        [StringLength(128, ErrorMessage = "{0}不能超过128个字符！")]
        public string? Remark { get; set; }

        [Display(Name = "头像")]
        [StringLength(500, ErrorMessage = "{0}不能超过500个字符！")]
        public string? Avatar { get; set; }

        #endregion Declarations
    }
}