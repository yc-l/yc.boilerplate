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
    public class AppManager : BaseApplicationService, IAppManager
    {

        public ITenant _tenant;
     
        public AppManager(IHttpContextAccessor httpContextAccessor, ITenant tenant, ICacheManager cacheManager)
        :base(httpContextAccessor, cacheManager)
        {
            _tenant = tenant;
           
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
