using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using YC.ApplicationService.Dto;
using YC.Core.Autofac;
using YC.Model.SysDbEntity;

namespace YC.ApplicationService
{
    public interface IAppManager: IDependencyInjectionSupport
    {
      
        public UserDto LoginUser { get; }


        public void SetSession(string key, string data);
        

        public void SetSession(string key, object data);


        public string GetSession(string key);


        public T GetSession<T>(string key) where T : class, new();

    }
}