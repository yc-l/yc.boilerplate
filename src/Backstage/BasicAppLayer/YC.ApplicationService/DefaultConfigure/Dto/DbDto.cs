using System;
using System.Collections.Generic;
using System.Text;

namespace YC.ApplicationService
{
    public class DbDto
    {
        /// <summary>
        /// 默认使用的数据库类别
        /// </summary>
        public string DefaultDbType { get; set; }
        /// <summary>
        /// 默认mysql数据库
        /// </summary>
        public string DefaultMySqlConnectionString { get; set; }

        /// <summary>
        /// 默认数据库连接字符串
        /// </summary>
        public string DefaultDBConnectionString { get; set; }

        /// <summary>
        /// 默认第二个数据库连接字符串
        /// </summary>
        public string DefaultSecondConnectionString { get; set; }

        /// <summary>
        /// 读 数据库集合
        /// </summary>
        public List<KeyValue> ReadDbConnectionStringList { get; set; }

        /// <summary>
        /// 读 数据库集合
        /// </summary>
        public List<KeyValue> WriteDbConnectionStringList { get; set; }
        /// <summary>
        /// 是否强制覆盖已经同名的数据库
        /// </summary>
        public bool IsCoverExistDb { get; set; }

        /// <summary>
        /// 是否开启读写分离
        /// </summary>
        public bool IsOpenReadWriteSeparation { get; set; }

        public ConnectionRedis ConnectionRedis { get; set; }


    }
    public class KeyValue
    {

        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }

    public class ConnectionRedis
    {
        public string Connection { get; set; }
        public string InstanceName { get; set; }
        public string SessionTimeOut { get; set; }

    }

 


   



}
