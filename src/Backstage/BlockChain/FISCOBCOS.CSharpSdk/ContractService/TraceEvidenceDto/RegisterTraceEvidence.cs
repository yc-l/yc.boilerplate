using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISCOBCOS.CSharpSdk
{
    public class RegisterTraceEvidence
    {
      
            [JsonProperty("serviceId", Required = Required.Always)]
            public string ServiceId { get; set; }

            [JsonProperty("typeName", Required = Required.Always)]
            public string TypeName { get; set; }

            [JsonProperty("dataValue", Required = Required.Always)]
            public string DataValue { get; set; }
    }
}
