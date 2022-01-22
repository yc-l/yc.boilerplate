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
using YC.ApplicationService.DefaultConfigure;
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
            List<AllowedNoTokenUrl> filterActions = DefaultConfig.AllowedNoTokenUrls;
            var rpv = _httpContextAccessor.HttpContext.Request.Path.Value;
            var noCheckTenantData= filterActions.Where(x => x.NoCheckTenant == true && x.Url.Contains(rpv)).FirstOrDefault();
            var checkTenantData = filterActions.Select(x=>x.Url);
            if (noCheckTenantData != null) {//这里功能不涉及到租户，所以为了保证流程通畅，提供默认租户，方便注入地方流程顺利通过
                this.TenantId = DefaultConfig.TenantSetting.DefaultTenantId;
                this.TenantDbString = DefaultConfig.TenantSetting.DefaultDbConnectionString;
                return; 
            }
            if (checkTenantData.Contains(rpv))//不需要token验证
            {
                
                if (_httpContextAccessor.HttpContext.Request.ContentType == null)
                {
                    tenantObj = _httpContextAccessor.HttpContext.Request.Query[DefaultConfig.TenantSetting.TenantKeyName];
                }
                else
                {
                    requestString = HttpContextExtensions.GetRequestParams(_httpContextAccessor.HttpContext.Request);
                    if (!string.IsNullOrWhiteSpace(requestString))
                    {
                        tenantObj = requestString.ToJObject().GetValueByKey<string>(DefaultConfig.TenantSetting.TenantKeyName);

                    }

                    if (_httpContextAccessor.HttpContext.Request.ContentType == "application/x-www-form-urlencoded")
                    {
                        var formData = _httpContextAccessor.HttpContext.Request.Form;
                        tenantObj = formData.Where(x => x.Key.Contains(DefaultConfig.TenantSetting.TenantKeyName)).Select(x => x.Value).FirstOrDefault();

                    }
                }
            }
            else///但都要做租户校验
            {//登录后，请求从token中获取对应的租户
                var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "token为空！");
                }
                else
                {
                    tenantObj = TokenContext.GetPayLoad(token)[DefaultConfig.TenantSetting.TenantKeyName]?.ToString();
                    if (string.IsNullOrWhiteSpace(tenantObj) && DefaultConfig.TenantSetting.TenantList.Where(x => x.TenantId == int.Parse(tenantObj)).FirstOrDefault() == null)//不存在租户id，或者租户id不在配置中
                        throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "token 相关信息无效，请从新获取Token！");
                    if (DefaultConfig.DefaultAppConfig.VerifyTokenUniqueness) {//演示系统，该属性不开启，默认不校验唯一性
                        ValidateTokenExtenstions.ValidateToken(token, _httpContextAccessor, _cacheManager);
                    }
                    
                }


            }

            var tenantInfo = new TenantInfo();
            if (!DefaultConfig.TenantSetting.MultiTnancy)
            { //不开启多租户，采用默认配置
                tenantInfo.DbConnectionString = DefaultConfig.TenantSetting.DefaultDbConnectionString;
                tenantInfo.TenantId = DefaultConfig.TenantSetting.DefaultTenantId;
                tenantInfo.DbType = DefaultConfig.TenantSetting.DefaultDbType;
            }
            else { //多租户情况下
                   //初始化做一次数据配置导入
                if (string.IsNullOrEmpty(tenantObj)|| tenantObj== DefaultConfig.TenantSetting.DefaultTenantId.ToString())
                {
                    tenantObj = DefaultConfig.TenantSetting.DefaultTenantId.ToString();
                }

                tenantInfo = DefaultConfig.TenantSetting.TenantList.Where(x => x.TenantId == int.Parse(tenantObj)).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(tenantInfo?.DbConnectionString)) {
                    throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "不存在对应的租户！");
                }
            }

            this.TenantId = tenantInfo.TenantId;
            this.TenantDbType = tenantInfo.DbType;
            this.TenantDbString = tenantInfo.DbConnectionString;

        }

        public int TenantId { get; set; }
        public int TenantDbType { get; set; }
        public string TenantDbString { get; set; }


    }
}
