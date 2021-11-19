using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Micro.Configuration
{


    public class ConsulSetting
    {
        
        /// <summary>
        /// 
        /// </summary>
        public string ConsulServiceAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public MicroServices MicroServices { get; set; }
    }
    public class MicroServices
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Service> Services { get; set; }
    }
    public class Service
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> GrpcUrls { get; set; }
    }

  

}
