using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Micro.UserApplicationService.ApplicationService.SysUserAppService.Dto;

namespace YC.Micro.UserApplicationService.SysUserAppService.Dto
{
    public class UserAddOrEditDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "请输入账号")]
        public string Account { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Required(ErrorMessage = "请输入昵称")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }


        public int Sex { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public string[] UserRoleIds { get; set; }



    }
}
