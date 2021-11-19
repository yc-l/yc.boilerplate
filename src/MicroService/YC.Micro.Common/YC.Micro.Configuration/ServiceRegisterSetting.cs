using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Micro.Configuration
{
   public class ServiceRegisterSetting
    {

        // 服务ID
        public string ServiceId { get; set; }

        // 服务名称
        public string ServiceName { get; set; }

        // 服务地址
        public string ServiceAddress { get; set; }

        // 服务标签(版本)
        public string[] ServiceTags { set; get; }   

        // 服务注册地址
        public string ConsulAddress { get; set; }

    }
}
