using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.ApplicationService;
using YC.Common.ShareUtils;
using YC.Core;
using YC.Core.Cache;

namespace YC.ServiceWebApi.Extensions
{
    public class ValidateTokenExtenstions
    {
        public static void  ValidateToken(string token, IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager)
        {
           
            token = token.ToString().Replace("Bearer", "").Trim();
            var tokenValidate = TokenContext.Validate(token, (load) =>
            {
              
                try
                {
                    var tenantObj = load[DefaultConfig.TenantSetting.TenantKeyName]?.ToString();
                    if (DefaultConfig.TenantSetting.MultiTnancy)
                    {//开启租户情况下，进行校验
                        if (string.IsNullOrWhiteSpace(tenantObj) || DefaultConfig.TenantSetting.TenantList.Where(x => x.TenantId == int.Parse(tenantObj)).FirstOrDefault() == null)//不存在租户id，或者租户id不在配置中
                            throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "token 相关信息无效，请从新获取Token！");
                    }

                    var tokenKey = load[DefaultConfig.DefaultAppConfig.TokenKeyName]?.ToString();
                    //刷新token，自动放过去
                    if (httpContextAccessor.HttpContext.Request.Path.Value.Contains("Identity/RefreshToken"))
                    {
                        return true;
                    }
                    var userInfo = cacheManager.Get<UserDto>(string.Format(DefaultConfig.CACHE_TOKEN_USER, tokenKey));
                    if (userInfo == null)
                        throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "Token 无效，请从新获取Token！");
                    if (userInfo.Token != token)
                        throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "Token 信息不一致，请重新获取token！");

                    if (userInfo.IP != IPUtils.GetIP(httpContextAccessor?.HttpContext?.Request)) {
                        throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "Token 留存IP信息不一致！");
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains(DefaultConfig.DefaultAppConfig.ExceptionKey))
                    {
                        throw ex;
                    }
                    else {
                        throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "获取租户信息失败！");
                    }
                    
                }
            });

            if (!tokenValidate)
            {
                throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "Token 验证失败，请从新获取Token！");
            }
        }
    }
}
