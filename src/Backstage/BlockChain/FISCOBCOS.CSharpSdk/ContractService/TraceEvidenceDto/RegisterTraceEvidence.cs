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

        /// <summary>
        /// 业务流程Id
        /// </summary>
        [JsonProperty("businessFlowId", Required = Required.Always)]
        public string BusinessFlowId { get; set; }
        /// <summary>
        /// 行为类别Id
        /// </summary>
        [JsonProperty("behaviorTypeId", Required = Required.Always)]
        public string BehaviorTypeId { get; set; }

        /// <summary>
        /// 业务流程名称
        /// </summary>
        [JsonProperty("businessFlowName", Required = Required.Always)]
        public string BusinessFlowName { get; set; }
        /// <summary>
        /// 行为类别名称
        /// </summary>
        [JsonProperty("behaviorTypeName", Required = Required.Always)]
        public string BehaviorTypeName { get; set; }

        [JsonProperty("typeName", Required = Required.Always)]
            public string TypeName { get; set; }

            [JsonProperty("dataValue", Required = Required.Always)]
            public string DataValue { get; set; }
    }
}
