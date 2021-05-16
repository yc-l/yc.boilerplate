using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService.Dto;
using YC.Common.NetCoreUtils;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Cache;
using YC.DapperFrameWork;
using YC.Model.SysDbEntity;

namespace YC.ApplicationService
{
    public class AppManager : IAppManager
    {


        public IHttpContextAccessor _httpContextAccessor { get; }
        public ITenant _tenant;
        public ICacheManager _cacheManager;
      

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

        private UserDto GetLoginUser()
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

        public AppManager(IHttpContextAccessor httpContextAccessor, ITenant tenant, ICacheManager cacheManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenant = tenant;
            _cacheManager = cacheManager;
           

        }

        public void SetSession(string key, string data)
        {
            new SessionManager(this._httpContextAccessor).SetSession(key, data);

        }

        public void SetSession(string key, object data)
        {
            new SessionManager(this._httpContextAccessor).SetSession(key, data);
        }

        public string GetSession(string key)
        {
            var sessionObj = new SessionManager(this._httpContextAccessor).GetSession(key);
            return sessionObj;
        }

        public T GetSession<T>(string key) where T : class, new()
        {
            var temp = (T)(new SessionManager(_httpContextAccessor).GetSession<T>(key));
            return temp;
        }

       
    }
}
