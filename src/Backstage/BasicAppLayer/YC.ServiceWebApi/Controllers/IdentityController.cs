using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YC.ServiceWebApi.Result;
using ServiceWebApi.Dto;
using YC.Common;
using YC.Common.ShareUtils;
using YC.ApplicationService;
using Newtonsoft.Json.Linq;
using YC.ServiceWebApi.Tenant;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using YC.Core;
using YC.Model.DbEntity;
using YC.Core;
using MongoDB.Driver;
using YC.MongoDB;
using YC.Core.Cache;
using YC.ElasticSearch.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YC.ServiceWebApi.Controllers
{


    /// <summary>
    /// 接口服务身份认证
    /// </summary>
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]


    public class IdentityController : BaseController
    {
        public readonly ISysUserAppService _sysUserService;
        public readonly IUserManager _userManager;
        public ICacheManager _cacheManager;
       
        public IdentityController(ISysUserAppService sysUserService, IUserManager userManager,
            ICacheManager cacheManager)
        {
            _sysUserService = sysUserService;
            _userManager = userManager;
            _cacheManager = cacheManager;
        }
        /// <summary>
        /// 获取token，通过登录
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="pwd">用户密码</param>
        /// <returns>返回登录结果</returns>
        [HttpPost]

        public IActionResult GetTokenByLogin([FromBody] LoginUserDto loginUserDto)
        {

            //登录，先去数据库做验证，成功了，说明可以进行token创建，往payLoad字典中加入,如果没有传TenantId 默认就为默认租户
            IApiResult<UserDto> result = _userManager.UserLogin(loginUserDto.UserId, loginUserDto.Pwd, loginUserDto.GuidKey, loginUserDto.ValidateCode, loginUserDto.TenantId == 0 ? 1 : loginUserDto.TenantId);
            return new JsonResult(result);


        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="token">原始token</param>
        /// <returns></returns>
        [HttpPost]
        public IApiResult RefreshToken(string token)
        {
            var msg = "";
            var tokenStr = _userManager.RefreshToken(token);
            var state = string.IsNullOrWhiteSpace(tokenStr) ? false : true;
            msg = state == true ? "刷新token获取成功！" : "token过期,请重新登录！";
            return ApiResult.Result<string>(state, tokenStr, msg);
        }

        /// <summary>
        /// 返回一个guid
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IApiResult GetGuid()
        {
            return ApiResult.Result<string>(true, Guid.NewGuid().ToString(), "");
        }

        /// <summary>
        /// 返回验证码如图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetVerificationCode(string guidKey)
        {
            if (string.IsNullOrWhiteSpace(guidKey)) {
                throw new Exception(DefaultConfig.DefaultAppConfigDto.ExceptionKey+"验证码指定Key不存在！");
            }
            string verificationCode = "";
            var imageMemoryStream=  VerificationCodeUtils.CreateVerificationCodeImage(out verificationCode);        
           // _userManager.SetSession(DefaultConfig.SESSION_VERIFICATIONCODE, verificationCode);
            _cacheManager.Add(guidKey, verificationCode);
            var bytes = imageMemoryStream.GetBuffer();
            return new FileContentResult(bytes, "image/jpeg");
        }

    }

}
