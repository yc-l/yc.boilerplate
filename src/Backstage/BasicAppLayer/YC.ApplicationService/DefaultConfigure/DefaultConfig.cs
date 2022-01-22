
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YC.ApplicationService.DefaultConfigure;
using YC.Common;
using YC.Common.ShareUtils;
using System.Linq;
using YC.ApplicationService.DefaultConfigure.Dto;

namespace YC.ApplicationService
{
    /// <summary>
    /// 默认配置
    /// </summary>
    public class DefaultConfig
    {
        /// <summary>
        /// 如果是应用，默认要在开启时候，做一次设置动作
        /// </summary>
        public static string dbConfigFilePath = System.Environment.CurrentDirectory + "//DefaultConfig.json";
        /// <summary>
        /// 租户session 前缀
        /// </summary>
        public const string SESSION_TENANT = "TENANT_";
        /// <summary>
        ///用户session 前缀
        /// </summary>
        public const string SESSIONT_TENANT_USER = "TENANT_{0}_USER";
        /// <summary>
        /// //登录验证码
        /// </summary>
        public const string SESSION_VERIFICATIONCODE = "LOGIN_VERIFICATIONCODE";
        /// <summary>
        /// 用户缓存前缀
        /// </summary>
        public const string CACHE_TOKEN_USER = "CACHE_TOKEN_USER_{0}";
        /// <summary>
        /// 用户刷新缓存前缀
        /// </summary>
        public const string CACHE_RETOKEN_USER = "CACHE_RETOKEN_USER_{0}";
        /// <summary>
        /// 用户角色权限
        /// </summary>
        public const string CACHE_USER_ROLE_PEMISSION = "CACHE_USER_ROLE_PEMISSION_{0}";

        /// <summary>
        /// 数据库配置
        /// </summary>
        public static DataBaseConfig DbConfig
        {
            get
            {
                return JsonConfig.ToObject<DataBaseConfig>();
            }
        }

        /// <summary>
        /// 租户配置
        /// </summary>
        public static TenantSetting TenantSetting
        {
            get
            {
                return JsonConfig.GetOjectByJsonKey<TenantSetting>("TenantSetting");
            }
        }

        /// <summary>
        /// 默认app设置
        /// </summary>
        public static DefaultAppConfig DefaultAppConfig
        {
            get
            {
                return JsonConfig.GetOjectByJsonKey<DefaultAppConfig>("AppSetting");
            }
        }

        /// <summary>
        /// es配置
        /// </summary>
        public static ElasticSearchSeting ElasticSearchSeting
        {
            get
            {
                return JsonConfig.GetOjectByJsonKey<ElasticSearchSeting>("ElasticSearchSeting");
            }
        }

        /// <summary>
        /// 允许不需要token访问URL
        /// </summary>
        public static List<AllowedNoTokenUrl> AllowedNoTokenUrls
        {
            get
            {

                return DefaultConfig.DefaultAppConfig.AllowedNoTokenUrls;
            }
        }

        /// <summary>
        /// 允许不做权限校验url
        /// </summary>
        public static string[] AllowedNoPermissionUrls
        {
            get
            {

                return DefaultConfig.DefaultAppConfig.AllowedNoPermissionUrls.Select(x => x.Url).ToArray();
            }
        }
        /// <summary>
        /// es 集群节点
        /// </summary>
        public static string[] ElasticSearchNodes
        {
            get
            {
                return DefaultConfig.ElasticSearchSeting.Nodes.Select(x => x.Node).ToArray();
            }
        }
        /// <summary>
        /// json配置
        /// </summary>
        public static string JsonConfig { get; set; }

        /// <summary>
        /// redis 配置
        /// </summary>
        public static ConnectionRedis ConnectionRedis => DbConfig.ConnectionRedis;
        /// <summary>
        /// 获取指定的json对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T GetJsonList<T>(string json) where T : class
        {
            T tempJsonData = json.ToObject<T>();
            return tempJsonData;
        }

        /// <summary>
        /// 获取指定的json字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetConfigJson(string path)
        {
            string jsonfile = path;

            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    var o = JToken.ReadFrom(reader);
                    return o.ToString();
                }
            }

        }

        /// <summary>
        /// 修改json配置
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool SetConfigJson(string path, string content)
        {
            string error = "";
            bool result = FileUtils.CoverWriteFile(path, content, out error);
            return result;
        }

    }
}
