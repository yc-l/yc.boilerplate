using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace YC.Common.NetCoreUtils
{
   public class SessionManager:IDisposable
    {
        IHttpContextAccessor _httpContextAccessor;
        public SessionManager(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
        public void SetSession(string key, string data)
        {
            _httpContextAccessor.HttpContext.Session.Set(key, Common.ShareUtils.ConvertUtils.StringToBytesByUtf8(data));
        }

        public void SetSession(string key, object data)
        {
            _httpContextAccessor.HttpContext.Session.Set(key, Common.ShareUtils.ConvertUtils.StringToBytesByUtf8(JsonSerializer.Serialize(data)));
        }

        public string GetSession(string key)
        {
            byte[] sessionData;
            string temp = "";
            _httpContextAccessor.HttpContext.Session.TryGetValue(key, out sessionData);
            if (sessionData != null)
            {
                temp = YC.Common.ShareUtils.ConvertUtils.BytesToStringByUtf8(sessionData);
            }
            return temp;
        }

        public T GetSession<T>(string key) where T : class, new()
        {

            byte[] sessionObjJson;
            T t = new T();
            _httpContextAccessor.HttpContext.Session.TryGetValue(key, out sessionObjJson);
            if (sessionObjJson != null)
            {
                t = JsonSerializer.Deserialize<T>(YC.Common.ShareUtils.ConvertUtils.BytesToStringByUtf8(sessionObjJson));
            }
            return t;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
