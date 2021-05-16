using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService;
using YC.ApplicationService.ApplicationService.Dto;
using YC.Common.ShareUtils;
using YC.Core;
using YC.Core.Cache;
using YC.DapperFrameWork;
using YC.ServiceWebApi.Extensions;
using YC.ServiceWebApi.Tenant;

namespace YC.ServiceWebApi
{

    public class DefaultTenant : ITenant
    {
        public IHttpContextAccessor _httpContextAccessor;
        public ICacheManager _cacheManager;
        public DefaultTenant(IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheManager = cacheManager;

            string tenantObj = "";
            string requestString = "";
            var requestHeader = _httpContextAccessor.HttpContext.Request.Headers.Where(x => x.Key == "accept").Select(x => x.Value).FirstOrDefault();
            string[] filterActions = new string[] { "/api/Identity/GetTokenByLogin", "/api/Identity/RefreshToken" };
            //如果是还没登录
            if (filterActions.Contains(_httpContextAccessor.HttpContext.Request.Path.Value))
            {
                //如果是登录授权控制器，就自动跳过

                if (_httpContextAccessor.HttpContext.Request.ContentType == null)
                {
                    tenantObj = _httpContextAccessor.HttpContext.Request.Query[DefaultConfig.TenantSettingDto.TenantKeyName];
                }
                else
                {
                    requestString = HttpContextExtensions.GetRequestParams(_httpContextAccessor.HttpContext.Request);
                    if (!string.IsNullOrWhiteSpace(requestString))
                    {
                        tenantObj = requestString.ToJObject().GetValueByKey<string>(DefaultConfig.TenantSettingDto.TenantKeyName);

                    }

                    if (_httpContextAccessor.HttpContext.Request.ContentType == "application/x-www-form-urlencoded")
                    {
                        var formData = _httpContextAccessor.HttpContext.Request.Form;
                        tenantObj = formData.Where(x => x.Key.Contains(DefaultConfig.TenantSettingDto.TenantKeyName)).Select(x => x.Value).FirstOrDefault();

                    }
                }
            }
            else
            {//登录后，请求从token中获取对应的租户
                var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new Exception(DefaultConfig.DefaultAppConfigDto.ExceptionKey + "token为空！");
                }
                else
                {
                    tenantObj = TokenContext.GetPayLoad(token)[DefaultConfig.TenantSettingDto.TenantKeyName]?.ToString();
                    if (string.IsNullOrWhiteSpace(tenantObj) || DefaultConfig.TenantSettingDto.TenantList.Where(x => x.TenantId == int.Parse(tenantObj)).FirstOrDefault() == null)//不存在租户id，或者租户id不在配置中
                        throw new Exception(DefaultConfig.DefaultAppConfigDto.ExceptionKey + "token 相关信息无效，请从新获取Token！");
                    ValidateTokenExtenstions.ValidateToken(token, _httpContextAccessor, _cacheManager);
                }


            }


            //var tenantObj = _httpContextAccessor.HttpContext.Request.Form.Where(x => x.Key.Equals(DefaultConfig.TenantSettingDto.TenantKeyName)).Select(x => x.Value).FirstOrDefault();

            //初始化做一次数据配置导入
            if (string.IsNullOrEmpty(tenantObj))
            {
                tenantObj = "0";
            }
            var data = DefaultConfig.TenantSettingDto.TenantList.Where(x => x.TenantId == int.Parse(tenantObj)).FirstOrDefault();

            if (data == null)
            {

                if (DefaultConfig.TenantSettingDto.MultiTnancy)//如果开启多租户情况下，那么就必须查找
                {////说明传入的租户不在配置中，这里可以改造在路由请求中租户采用对应的key 来进行判定，通过加解密进行拆解分析
                    throw new Exception(DefaultConfig.DefaultAppConfigDto.ExceptionKey + "不存在对应的租户！");
                }
                else
                {
                    data = new ApplicationService.DefaultConfigure.TenantInfo();
                    data.DbConnectionString = DefaultConfig.TenantSettingDto.DefaultDbConnectionString;
                    data.TenantId = DefaultConfig.TenantSettingDto.DefaultTenantId;
                }

            }

            this.TenantId = data.TenantId;
            this.TenantDbString = data.DbConnectionString;
            //TenantId = 1;
            //this.TenantDbString = "Server=127.0.0.1;Port=3307;User Id=root;Password=123456;Database=bigDataDB;";



        }

        public int TenantId { get; set; }

        public string TenantDbString { get; set; }


    }
}
