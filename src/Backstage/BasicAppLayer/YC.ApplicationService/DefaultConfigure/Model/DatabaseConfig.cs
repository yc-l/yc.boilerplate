using System;
using System.Collections.Generic;
using System.Text;

namespace YC.ApplicationService
{
    public class DataBaseConfig
    {

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
