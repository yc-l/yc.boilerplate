using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService.ApplicationService.Dto;
using YC.ApplicationService.Dto;
using YC.Core;
using YC.Core.Autofac;
using YC.Model.SysDbEntity;

namespace YC.ApplicationService
{
    public interface IUserManager : IAppManager
    {

        /// <summary>
        /// 独立隔离一层
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        public IApiResult<UserDto> UserLogin(string userId, string pwd,string guidKey, string validateCode, int TenantId = 1);

        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public string CreateToken(SysUser loginUserDto);
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string RefreshToken(string token);
    }
}
