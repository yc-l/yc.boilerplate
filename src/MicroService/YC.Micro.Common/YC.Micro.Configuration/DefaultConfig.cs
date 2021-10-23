using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Common.ShareUtils;

namespace YC.Micro.Configuration
{
    public class DefaultConfig
    {
     
        //如果是应用，默认要在开启时候，做一次设置动作
        public static string dbConfigFilePath = System.Environment.CurrentDirectory + "//appsettings.json";
       
        public static TenantSetting TenantSetting
        {
            get
            {
                return JsonConfig.GetOjectByJsonKey<TenantSetting>("TenantSetting");
            }
        }


        public static IConfigurationRoot GetConfiguration(string fileName)
        {

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(fileName).Build();
            return configuration;
        }
        public static IConfigurationRoot AppsettingsConfiguration
        {

            get
            {

                return DefaultConfig.GetConfiguration("appsettings.json");
            }
        }
        public static T GetAppsettingsConfigurationEntity<T>()
        {
            var entity = DefaultConfig.AppsettingsConfiguration.GetSection(typeof(T).Name).Get<T>();
            return entity;
        }

        public static T GetAppsettingsConfigurationEntity<T>(IConfigurationRoot configurationRoot)
        {
            var entity = configurationRoot.GetSection(typeof(T).Name).Get<T>();
            return entity;
        }

        public static T GetEntity<T>(string fileName)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(fileName).Build();
            var entity = configuration.GetSection(typeof(T).Name).Get<T>();
            return entity;
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

    }
}
