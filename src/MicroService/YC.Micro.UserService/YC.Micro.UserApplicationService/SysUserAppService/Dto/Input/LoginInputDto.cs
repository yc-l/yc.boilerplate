using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Model.SysDbEntity;

namespace YC.Micro.UserApplicationService.Dto
{
    public class LoginInputDto
    {
        public bool State { get; set; }
        public string Message { get; set; }

        public SysUser Data { get; set; }
        public string Token { get; set; }

      
    }
}
