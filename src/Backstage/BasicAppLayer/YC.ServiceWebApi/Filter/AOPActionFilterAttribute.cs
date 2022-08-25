using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YC.ServiceWebApi.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using YC.ApplicationService;
using YC.Core.Autofac;
using YC.Core.Cache;
using YC.Core;
using YC.Common.ShareUtils;
using YC.Common;
using Microsoft.AspNetCore.Http;
using YC.ServiceWebApi.Filter.Dto;
using YC.ServiceWebApi.Extensions;
using YC.ApplicationService.ApplicationService.SysUserAppService.Dto;
using AutoMapper;
using YC.ApplicationService.Dto;

namespace YC.ServiceWebApi.Filter
{
    public class AOPActionFilterAttribute : ActionFilterAttribute, IDependencyInjectionSupport
    {
        private ISysUserAppService _sysUserService;
        private ITenant _tenant;
        public ICacheManager _cacheManager;
        public IHttpContextAccessor _httpContextAccessor;
        public RequestInfoDto _requestInfoDto;
        private ISysAuditLogAppService _sysAuditLogAppService;
        public readonly IMapper _mapper;

        public AOPActionFilterAttribute(ICacheManager cacheManager, ITenant tenant, IHttpContextAccessor httpContextAccessor, ISysUserAppService sysUserService,
            IMapper mapper, ISysAuditLogAppService sysAuditLogAppService)
        {
            _tenant = tenant;
            _cacheManager = cacheManager;
            _httpContextAccessor = httpContextAccessor;
            _requestInfoDto = new RequestInfoDto();
            _sysUserService = sysUserService;
            _mapper = mapper;
            _sysAuditLogAppService = sysAuditLogAppService;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            bool exist = FilterAnonymous(context.ActionDescriptor.FilterDescriptors);
            if (exist)
            {
                return;
            }
            if (_requestInfoDto.Key == context.ActionDescriptor.Id)
            {
                _requestInfoDto.StopTime = DateTime.Now;
                _requestInfoDto.ElapsedMilliseconds = Convert.ToInt64((_requestInfoDto.StopTime - _requestInfoDto.StartTime).TotalMilliseconds);
                var obj = context.Result;
                _requestInfoDto.ResponseState = false;
                var resultType = obj?.GetType();
                if (obj != null)//不为空，说明程序正常有返回值，如果为null，说明可能报空了
                {
                    try
                    {
                        if (resultType.FullName.Equals(typeof(JsonResult).FullName))
                        {
                            var result = (JsonResult)obj;
                            //_requestInfoDto.ResponseData = result.Value == null ? "" : System.Text.Json.JsonSerializer.Serialize(result.Value);
                            //aop拦截处理 如果不是我们已经定义标准化返回，那么我们需要在外层包一层，如果不是，就直接让他自己按照正常处理返回
                            if ((result.Value.GetType().Name.Contains(typeof(ApiResult).Name) || result.Value.GetType().Name.Contains(typeof(ApiResult<>).Name)))
                            {
                                var State = System.Text.Json.JsonSerializer.Serialize(result.Value).ToJObject().GetValue("State");
                                _requestInfoDto.ResponseState = Convert.ToBoolean(State);
                            }
                        }
                        if (resultType.FullName.Equals(typeof(ObjectResult).FullName))
                        {
                            var result = (ObjectResult)context.Result;
                            //_requestInfoDto.ResponseData = result.Value == null ? "" : System.Text.Json.JsonSerializer.Serialize(result.Value);
                            //aop拦截处理 如果不是我们已经定义标准化返回，那么我们需要在外层包一层，如果不是，就直接让他自己按照正常处理返回
                            if ((result.Value.GetType().Name.Contains(typeof(ApiResult).Name) || result.Value.GetType().Name.Contains(typeof(ApiResult<>).Name)))
                            {
                                var State = System.Text.Json.JsonSerializer.Serialize(result.Value).ToJObject().GetValue("State");
                                _requestInfoDto.ResponseState = Convert.ToBoolean(State);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "审计日志拦截返回值处理失败！" + ex.ToString());
                    }

                    //throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "获取context.Result 对象为空！");
                }
                //写入审计日志
                //WriteRequestLog(context, _requestInfoDto);
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool exist = FilterAnonymous(context.ActionDescriptor.FilterDescriptors);
            if (exist)
            {
                return;
            }

            #region 拦截请求信息

            _requestInfoDto.Key = context.ActionDescriptor.Id;
            _requestInfoDto.StartTime = DateTime.Now;
            _requestInfoDto.TenantId = _tenant.TenantId;
            var requestString = context.ActionArguments?.ToString();
            string ua = context.HttpContext.Request.Headers["User-Agent"];
            var client = UAParser.Parser.GetDefault().Parse(ua);
            var device = client.Device.Family;
            device = device.ToLower() == "other" ? "" : device;
            _requestInfoDto.Browser = client.UA.Family;
            _requestInfoDto.Os = client.OS.Family;
            _requestInfoDto.Device = device;
            _requestInfoDto.BrowserInfo = ua;
            _requestInfoDto.IP = IPUtils.GetIP(context?.HttpContext?.Request);

            _requestInfoDto.ParamsString = context.ActionArguments?.ToIndentedJson();

            #endregion 拦截请求信息

            string[] filterActions = DefaultConfig.AllowedNoTokenUrls.Select(x => x.Url).ToArray();
            var controllerName = context.RouteData.Values["controller"]?.ToString();//获取当前控制器名称
            if (filterActions.Any(x => x.Contains(context.HttpContext.Request.Path)))
            {//可以放行,不校验
                return;
            }

            #region 1-指定控制器允许通过 只要访问的是集合内的控制器，才允许请求

            string requestPath = context.HttpContext.Request.Path.Value;

            #endregion 1-指定控制器允许通过 只要访问的是集合内的控制器，才允许请求

            #region 2- token 校验

            var token = context.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(token))
            {
                context.Result = new JsonResult(new ApiResultDto<string>() { Code = 400, Message = "token 为空!", Value = "" });
                return;
            }

            token = token.ToString().Replace("Bearer", "").Trim();

            #region 2.1 永久允许访问的token

            string[] isAllowedTokens = DefaultConfig.AllowedTokens;
            if (isAllowedTokens.Any(x => x.Contains(token)))
            {
                return;
            }

            #endregion 2.1 永久允许访问的token

            UserDto userInfo = new UserDto();
            if (DefaultConfig.DefaultAppConfig.VerifyTokenUniqueness)
            {//演示系统，该属性不开启，默认不校验唯一性
                ValidateTokenExtenstions.ValidateToken(token, _httpContextAccessor, _cacheManager);
            }

            var payloadDic = TokenContext.GetPayLoad(token);//ids4 拿过来必须可以解析，并且里面得到payloadDic里面有租户id和用户账号，
            var tokenKey = payloadDic[DefaultConfig.DefaultAppConfig.TokenKeyName]?.ToString();//拿到唯一Id
            //如果这里要改造全局身份校验，那么就是token中获取tenantid和userid，再通过数据库查询，用户信息
            userInfo = _cacheManager.Get<UserDto>(string.Format(DefaultConfig.CACHE_TOKEN_USER, tokenKey));
            if (userInfo == null) {
                throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "登录超时，请重新登录！");
            }
            _requestInfoDto.User = userInfo;

            #endregion 2- token 校验

            #region 3-权限校验

            //权限校验,如果自定义抛出异常，需要展示给前端看，那么必须 添加DefaultConfig.DefaultAppConfig.ExceptionKey ，全局日志校验，就会放通
            var userRolePermissions = GetUserRolePermissionCache(userInfo.TenantId, userInfo.Id);
            if (userRolePermissions == null)//如果角色权限获取异常直接抛出
            {
                throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "用户角色权限数据获取异常！");
            }
            string requestApi = context.HttpContext.Request.Path.Value.Replace("/api/", "");
            var isAllowedPermission = userRolePermissions.PermissionList.Exists(x => x.Api.Contains(requestApi));
            string[] isAllowedPaths = DefaultConfig.AllowedNoPermissionUrls;
            if (!isAllowedPermission && !isAllowedPaths.Any(x => x.Contains(requestApi)))
            {
                throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "没有对应的访问权限！");
            }

            #endregion 3-权限校验
        }

        public void WriteRequestLog(ActionExecutedContext context, RequestInfoDto requestInfo)
        {
            string sb = "--------------------------------------------------------------------\r\n";

            sb += "请求方基础信息：" + requestInfo.ToIndentedJson() + "\r\n";
            sb += "请求类型：" + context.HttpContext.Request.Method.ToLower() + "\r\n";
            sb += "请求Api：" + context.ActionDescriptor.AttributeRouteInfo.Template.ToLower() + "\r\n"; ;
            sb += "请求参数：" + requestInfo.ParamsString + "\r\n";
            sb += "请求结果状态：" + requestInfo.ResponseState.ToString() + "\r\n";
            //sb += "请求返回结果：" + requestInfo.ResponseData + "\r\n";
            sb += "执行时间：" + requestInfo.ElapsedMilliseconds + "毫秒.\r\n";

            LogUtils.WriteLog(new LogDto() { TypeName = "API接口请求日志", Message = sb });

            var sysAuditLog = _mapper.Map<SysAuditLogAddOrEditDto>(requestInfo);
            sysAuditLog.CreationTime = DateTime.Now;
            sysAuditLog.RequestApi = context.ActionDescriptor.AttributeRouteInfo.Template.ToLower();
            sysAuditLog.RequestMethod = context.HttpContext.Request.Method.ToLower();
            if (requestInfo.User != null)
            {
                sysAuditLog.UserId = requestInfo.User.Id;
                sysAuditLog.UserAccount = requestInfo.User.Account;
            }
            //sysAuditLog.ResponseData = requestInfo.ResponseData;
            sysAuditLog.ResponseState = requestInfo.ResponseState;
            try
            {
                _sysAuditLogAppService.CreateSysAuditLog(sysAuditLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取缓存中的对应用户的角色权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserRolePermissionDto GetUserRolePermissionCache(int tenantId, long id)
        {
            var result = new UserRolePermissionDto();

            string userRolePermissionCacheKey = string.Format(DefaultConfig.CACHE_USER_ROLE_PEMISSION, string.Format("tenantId_{0}_userId_{1}", tenantId, id));

            if (_cacheManager.Exists(userRolePermissionCacheKey))
            {
                result = _cacheManager.Get<UserRolePermissionDto>(userRolePermissionCacheKey);
            }
            else//直接再查询一次
            {
                result = _sysUserService.GetUserRolePermission(id).Data;//查询
                //再次进行缓存
                var isSuccessed = _cacheManager.Add(userRolePermissionCacheKey, result, TimeSpan.FromSeconds(DefaultConfig.DefaultAppConfig.CacheExpire));
            }
            return result;
        }

        /// <summary>
        /// 有标识AllowAnonymousFilter 自动放过 action级别拦截
        /// </summary>
        /// <param name="filterDescriptors"></param>
        /// <returns></returns>
        public bool FilterAnonymous(IList<FilterDescriptor> filterDescriptors)
        {
            var exist = filterDescriptors.Any(x => x.Filter.GetType() == typeof(Microsoft.AspNetCore.Mvc.Authorization.AllowAnonymousFilter));
            return exist;
        }
    }
}