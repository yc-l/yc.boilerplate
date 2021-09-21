
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YC.ApplicationService.DefaultConfigure;
using YC.Common;
using YC.Common.ShareUtils;

namespace YC.ApplicationService
{
    public class DefaultConfig
    {
        //如果是应用，默认要在开启时候，做一次设置动作
        public static string dbConfigFilePath = System.Environment.CurrentDirectory + "//DefaultConfig.json";
        public const string SESSION_TENANT = "TENANT_";//租户session 前缀
        public const string SESSIONT_TENANT_USER = "TENANT_{0}_USER";//用户session 前缀
        public const string SESSION_VERIFICATIONCODE = "LOGIN_VERIFICATIONCODE";//登录验证码
        public const string CACHE_TOKEN_USER = "CACHE_TOKEN_USER_{0}";//用户缓存前缀
        public const string CACHE_RETOKEN_USER = "CACHE_RETOKEN_USER_{0}";//用户刷新缓存前缀
        public const string CACHE_USER_ROLE_PEMISSION = "CACHE_USER_ROLE_PEMISSION_{0}";//用户角色权限
      
        
        public static DbDto DbConfigDto
        {
            get
            {
                return JsonConfig.ToObject<DbDto>();
            }
        }
        public static TenantSettingDto TenantSettingDto
        {
            get
            {
                return JsonConfig.GetOjectByJsonKey<TenantSettingDto>("TenantSetting");
            }
        }
        public static DefaultAppConfigDto DefaultAppConfigDto
        {
            get
            {
                return JsonConfig.GetOjectByJsonKey<DefaultAppConfigDto>("AppSetting");
            }
        }

        public static string JsonConfig { get; set; }
       
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

        public static bool SetConfigJson(string path, string content)
        {
            string error = "";
            bool result = FileUtils.CoverWriteFile(path, content, out error);
            return result;
        }


        /// <summary>
        /// 创建库使用
        /// </summary>
        public static string MysqlDbConnectionString => DbConfigDto.DefaultMySqlConnectionString;

        /// <summary>
        /// 默认库
        /// </summary>
        public static string DefaultDbConnectionString => DbConfigDto.DefaultDBConnectionString;


        /// <summary>
        /// 第二个库，多库
        /// </summary>
        public string SecondDbConnectionString => DbConfigDto.DefaultSecondConnectionString;

        /// <summary>
        /// 随机获取一个读的库
        /// </summary>
        public string ReadDConcectionString => DbConfigDto.ReadDbConnectionStringList[new Random().Next(0, DbConfigDto.ReadDbConnectionStringList.Count - 1)].ConnectionString;
        /// <summary>
        /// 随机获取一个写的库
        /// </summary>
        public static string WirteDConcectionString => DbConfigDto.WriteDbConnectionStringList[new Random().Next(0, DbConfigDto.WriteDbConnectionStringList.Count - 1)].ConnectionString;

        /// <summary>
        /// 是否开启读写分离
        /// </summary>
        public static bool IsOpenReadWriteSeparation => DbConfigDto.IsOpenReadWriteSeparation;

        public static ConnectionRedis ConnectionRedis => DbConfigDto.ConnectionRedis;
    }
}
