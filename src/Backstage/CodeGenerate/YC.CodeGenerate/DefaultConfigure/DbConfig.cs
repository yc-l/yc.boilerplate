
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YC.Common;
using YC.Common.ShareUtils;

namespace YC.CodeGenerate
{
    public class DbConfig
    {
        public static string dbConfigFilePath = System.Environment.CurrentDirectory + "\\DefaultConfigure\\DefaultConfig.json";

        /// <summary>
        /// 默认连接的数据库类型
        /// </summary>
        public Dapper.RepositoryUtils.Dialect defaultDbType;
        public DatabaseConfig DatabaseConfig;
        public DbConfig()
        {

            DatabaseConfig = GetConfigJson(dbConfigFilePath).ToObject<DatabaseConfig>();
            defaultDbType = DatabaseConfig.GetDbType();
        }
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
        public string GetConfigJson(string path)
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

        public bool SetConfigJson(string path, string content)
        {
            string error = "";
            bool result = FileUtils.CoverWriteFile(path, content, out error);
            return result;
        }


        /// <summary>
        /// 创建库使用
        /// </summary>
        public string MysqlDbConnectionString => DatabaseConfig.DefaultMySqlConnectionString;

        /// <summary>
        /// 默认库
        /// </summary>
        public string DefaultDbConnectionString => DatabaseConfig.DefaultDBConnectionString;


        /// <summary>
        /// 第二个库，多库
        /// </summary>
        public string SecondDbConnectionString => DatabaseConfig.DefaultSecondConnectionString;

        /// <summary>
        /// 随机获取一个读的库
        /// </summary>
        public string ReadDConcectionString => DatabaseConfig.ReadDbConnectionStringList[new Random().Next(0, DatabaseConfig.ReadDbConnectionStringList.Count - 1)].ConnectionString;
        /// <summary>
        /// 随机获取一个写的库
        /// </summary>
        public string WirteDConcectionString => DatabaseConfig.WriteDbConnectionStringList[new Random().Next(0, DatabaseConfig.WriteDbConnectionStringList.Count - 1)].ConnectionString;

        /// <summary>
        /// 是否开启读写分离
        /// </summary>
        public bool IsOpenReadWriteSeparation => DatabaseConfig.IsOpenReadWriteSeparation;
    }
}
