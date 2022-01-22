using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService.ApplicationService.SysUserAppService.Dto;
using YC.ApplicationService.Dto;

namespace YC.ApplicationService
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Expired { get; set; }
        public string RefreshTokenExpired { get; set; }
        public string Authentication { get; set; }
        public string IP { get; set; }
        public string Avatar { get; set; }
        public List<RoleInfoDto> RoleInfoList { get; set; }

        public List<SysPermissionDto> PermissionList { get; set; }
        public int TenantId { get; set; }

        public string Token { get; set; }
    }
}