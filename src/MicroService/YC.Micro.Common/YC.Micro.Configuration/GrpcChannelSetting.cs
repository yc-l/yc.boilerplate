using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Micro.Configuration
{
    public class GrpcChannelSetting
    {
      public List<GrpcService> GrpcServices { get; set; }
    }

    public class GrpcService
    {

        public string Url { get; set; }
        public string Name { get; set; }
    }
}
