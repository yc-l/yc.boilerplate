using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService.ApplicationService.Dto;
using YC.ApplicationService.ApplicationService.SysUserAppService.Dto;
using YC.ApplicationService.Dto;
using YC.Common.ShareUtils;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Cache;
using YC.DapperFrameWork;
using YC.Model.SysDbEntity;

namespace YC.ApplicationService
{
    public class UserManager : AppManager, IUserManager
    {
        public ISysUserAppService _sysUserService;
        public ICacheManager _cacheManager;
        public ITenant _tenant;
        public IMapper _mapper;

        public UserManager(ISysUserAppService sysUserService, ICacheManager cacheManager,
            IHttpContextAccessor httpContextAccessor, ITenant tenant,
            IMapper mapper) : base(httpContextAccessor, tenant, cacheManager)
        {
            _sysUserService = sysUserService;
            _cacheManager = cacheManager;
            _tenant = tenant;
            _mapper = mapper;
        }

        /// <summary>
        /// 独立隔离一层
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pwd"></param>
        /// <param name=DefaultConfig.TenantSetting.TenantKeyName></param>
        /// <returns></returns>
        public IApiResult<UserDto> UserLogin(string userId, string pwd, string guidKey, string validateCode, int tenantId = 1)
        {
            var res = new ApiResult<UserDto>();
            UserDto userDto = new UserDto();
            if (validateCode != DefaultConfig.DefaultAppConfig.DefaultVerifyCode)
            {//特定放过不要验证码
                if (_cacheManager.Get(guidKey)?.ToString().ToLower() != validateCode.ToLower())
                {
                    return res.NotOk("验证码过期！");
                }
            }

            var loginDto = _sysUserService.Login(userId, pwd, tenantId);
            if (loginDto.State)
            {
                userDto = _mapper.Map<UserDto>(loginDto.Data);
                userDto.Token = CreateToken(loginDto.Data);
                //claimsPrincipal 存储报错到缓存报错
                userDto.Authentication = true.ToString();
                userDto.Expired = DefaultConfig.DefaultAppConfig.CacheExpire.ToString();
                userDto.TenantId = tenantId;
                var tempData = _sysUserService.GetUserRolePermission(loginDto.Data.Id).Data;
                userDto.RoleInfoList = tempData.RoleInfoList;
                userDto.PermissionList = tempData.PermissionList;
                tempData.TenantId = tenantId;
                CreateUserRolePermissionCache(tempData);
                return res.Ok(userDto, loginDto.Message);
            }
            else
            {
                return res.NotOk(loginDto.Message);
            }
        }

        public bool CreateUserRolePermissionCache(UserRolePermissionDto input)
        {
            string userRolePermissionCacheKey = string.Format(DefaultConfig.CACHE_USER_ROLE_PEMISSION, string.Format("tenantId_{0}_userId_{1}", input.TenantId, input.Id));

            if (_cacheManager.Exists(userRolePermissionCacheKey))
            {
                _cacheManager.Remove(userRolePermissionCacheKey);
            }

            var isSuccessed = _cacheManager.Add(userRolePermissionCacheKey, input, TimeSpan.FromSeconds(DefaultConfig.DefaultAppConfig.CacheExpire));

            return isSuccessed;
        }

        public string CreateToken(SysUser loginUserDto)
        {
            string token = "";

            IEnumerable<Claim> claims = new Claim[]
             {
                    new Claim("Id", loginUserDto.Id.ToString()),
                    new Claim("Account", loginUserDto.Account),
                    new Claim(ClaimTypes.Name, loginUserDto.Name??""),
                    //new Claim(ClaimTypes.Email, loginUserDto.Email??""),
                    //new Claim(ClaimTypes.MobilePhone, loginUserDto.Mobile??""),
                    new Claim(ClaimTypes.Expired,DefaultConfig.DefaultAppConfig.TokenExpire.ToString()),
                     new Claim("RefreshTokenExpired",DefaultConfig.DefaultAppConfig.RefreshTokenExpire.ToString()),
                    new Claim(ClaimTypes.Authentication, true.ToString()),
                    new Claim(DefaultConfig.DefaultAppConfig.TokenKeyName,  string.Format("tenantId_{0}_userId_{1}",_tenant.TenantId.ToString(),loginUserDto.Id)),
                    new Claim(DefaultConfig.TenantSetting.TenantKeyName,_tenant.TenantId.ToString()),
                    new Claim("Issuer",DefaultConfig.DefaultAppConfig.TokenIssuer),
                    new Claim("Audience",DefaultConfig.DefaultAppConfig.TokenAudience),
             };

            var payLoad = new Dictionary<string, object>();
            foreach (var i in claims)
            {
                payLoad.Add(i.Type, i.Value);
            }

            token = TokenContext.CreateTokenByHandler(payLoad, DefaultConfig.DefaultAppConfig.TokenExpire);//创建token
            var identity = new ClaimsIdentity("合法访问Token");

            identity.AddClaims(claims);
            identity.AddClaim(new Claim("Token", token));
            var claimsPrincipal = new ClaimsPrincipal(identity);//证件主体，可以持有多个证件claimsIdentity
            _httpContextAccessor.HttpContext.User = claimsPrincipal;//将信息复制给当前的httpContext的用户，结果是无效的，赋值进去后面也找不到。
                                                                    //session保存当前用户,httpcontext User 这个获取不靠谱，因为没有完全实现
            this.SetSession(string.Format(DefaultConfig.SESSIONT_TENANT_USER, _tenant.TenantId), loginUserDto);
            var userDto = _mapper.Map<UserDto>(loginUserDto);
            //claimsPrincipal 存储报错到缓存报错
            userDto.Authentication = true.ToString();
            userDto.Expired = DefaultConfig.DefaultAppConfig.CacheExpire.ToString();
            userDto.TenantId = _tenant.TenantId;
            userDto.Token = token;
            userDto.IP = IPUtils.GetIP(_httpContextAccessor?.HttpContext?.Request);
            _cacheManager.Add(string.Format(DefaultConfig.CACHE_TOKEN_USER, string.Format("tenantId_{0}_userId_{1}", _tenant.TenantId.ToString(), loginUserDto.Id)), userDto, TimeSpan.FromSeconds(DefaultConfig.DefaultAppConfig.TokenExpire));
            //刷新缓存token
            _cacheManager.Add(string.Format(DefaultConfig.CACHE_RETOKEN_USER, string.Format("tenantId_{0}_userId_{1}", _tenant.TenantId.ToString(), loginUserDto.Id)), userDto, TimeSpan.FromSeconds(DefaultConfig.DefaultAppConfig.RefreshTokenExpire));
            return token;
        }

        public string RefreshToken(string token)
        {
            var payloadDic = TokenContext.GetPayLoad(token);
            payloadDic.Remove("nbf");
            payloadDic.Remove("exp");
            var newToken = "";
            var tokenKey = payloadDic[DefaultConfig.DefaultAppConfig.TokenKeyName];
            var userInfo = _cacheManager.Get<UserDto>(string.Format(DefaultConfig.CACHE_TOKEN_USER, tokenKey));
            var refreshUserInfo = _cacheManager.Get<UserDto>(string.Format(DefaultConfig.CACHE_RETOKEN_USER, tokenKey));
            string tokenCacheKey = string.Format(DefaultConfig.CACHE_TOKEN_USER, tokenKey);
            string refreshTokenCacheKey = string.Format(DefaultConfig.CACHE_RETOKEN_USER, tokenKey);
            if (userInfo == null || refreshUserInfo == null)
            {
                return newToken;
            }
            newToken = TokenContext.CreateTokenByHandler(payloadDic, DefaultConfig.DefaultAppConfig.TokenExpire);//创建token
            if (_cacheManager.Exists(tokenCacheKey))
            {
                _cacheManager.Remove(tokenCacheKey);
            }

            if (_cacheManager.Exists(refreshTokenCacheKey))
            {
                _cacheManager.Remove(refreshTokenCacheKey);
            }

            _cacheManager.Add(tokenCacheKey, userInfo, TimeSpan.FromSeconds(DefaultConfig.DefaultAppConfig.RefreshTokenExpire));
            _cacheManager.Add(refreshTokenCacheKey, userInfo, TimeSpan.FromSeconds(DefaultConfig.DefaultAppConfig.TokenExpire));
            return newToken;
        }
    }
}