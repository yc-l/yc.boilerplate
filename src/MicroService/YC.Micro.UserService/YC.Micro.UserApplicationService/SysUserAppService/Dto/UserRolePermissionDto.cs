using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Micro.UserApplicationService.Dto;

namespace YC.Micro.UserApplicationService.ApplicationService.SysUserAppService.Dto
{
   public class UserRolePermissionDto
    {
        public int TenantId { get; set; }
        public string Id { get; set; }

        public string Account { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }


        public int Sex { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public List<RoleInfoDto> RoleInfoList { get; set; }

        public List<SysPermissionDto> PermissionList { get; set; }
    }
}
