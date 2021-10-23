using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService.Dto;
using YC.Core;
using YC.Core.Cache;
using YC.Core.Domain;
using YC.Core.DynamicApi.Attributes;

namespace YC.ApplicationService
{
    public class BaseApplicationService
    {
        public IHttpContextAccessor _httpContextAccessor;
        public ICacheManager _cacheManager;

        public BaseApplicationService(IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager)
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
                        var tenantObj = load[DefaultConfig.TenantSetting.TenantKeyName]?.ToString();
                        var tokenKey = load[DefaultConfig.DefaultAppConfig.TokenKeyName]?.ToString();
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

        public virtual T SetIdKey<T>(T input) where T : class, new()
        {

            var inputProperties = input.GetType().GetProperties();
            //T tempModel = (T)Activator.CreateInstance(input.GetType());
            foreach (var p in inputProperties)
            {
                string keyData = "";
                //获取主键
                if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false))
                {
                    //自增类型
                    if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute), false))
                    {
                        p.SetValue(input, null);
                    }
                    else
                    {
                        if (p.PropertyType.IsGenericType &&
               p.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))//判断convertsionType是否为nullable泛型类
                        {
                            System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(p.PropertyType);
                            p.SetValue(input, Convert.ChangeType(keyData, nullableConverter.UnderlyingType));
                        }
                        else if (p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition().Equals(typeof(Guid)))
                        {

                            p.SetValue(input, Convert.ChangeType(Guid.NewGuid(), p.PropertyType));
                        }
                        else
                        {
                            keyData = ObjectId.Get();
                            p.SetValue(input, Convert.ChangeType(keyData, p.PropertyType));
                        }
                    }



                }

            }
            return input;
        }

        public string GetKeyName<T>(T input)
        {
            string keyName = "";
            var inputProperties = input.GetType().GetProperties();
            foreach (var p in inputProperties)
            {
                if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false))
                {
                    keyName = p.Name;
                }
            }

            return keyName;

        }
    }
}
