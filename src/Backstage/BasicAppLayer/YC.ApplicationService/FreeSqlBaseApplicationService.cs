using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YC.Common.NetCoreUtils;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Cache;
using YC.Core.DynamicApi.Attributes;
using YC.DapperFrameWork;
using YC.Model.SysDbEntity;

namespace YC.ApplicationService
{

    public class FreeSqlBaseApplicationService : IApplicationService
    {
        public IHttpContextAccessor _httpContextAccessor;
        public ICacheManager _cacheManager;

        public FreeSqlBaseApplicationService(IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheManager = cacheManager;
        }


        public UserDto LoginUser
        {

            get
            {
                //session 在对方那边如果没有cookie 情况下，是无法留存的
                //var obj = GetSession<UserDto>(string.Format(DefaultConfig.SESSIONT_TENANT_USER, _tenant.TenantId));
                var obj = GetLoginUser();//使用token的维度，从请求头那边获取token，进行token对应cache 数据比对
                if (obj.Id == 0)
                    return null;
                else
                    return obj;
            }
        }
        [NoDynamicMethod]
        public UserDto GetLoginUser()
        {
            UserDto obj = new UserDto();
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                token = token.ToString().Replace("Bearer", "").Trim();
                var tokenValidate = TokenContext.Validate(token, (load) =>
                {
                    try
                    {
                        var tenantObj = load[DefaultConfig.TenantSettingDto.TenantKeyName]?.ToString();
                        var tokenKey = load[DefaultConfig.DefaultAppConfigDto.TokenKeyName]?.ToString();
                        obj = _cacheManager.Get<UserDto>(string.Format(DefaultConfig.CACHE_TOKEN_USER, tokenKey));
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                });
            }

            return obj;
        }

    }
}
