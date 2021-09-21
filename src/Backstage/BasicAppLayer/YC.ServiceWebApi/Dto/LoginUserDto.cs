using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWebApi.Dto
{
    [Serializable]
    public class LoginUserDto
    {
        [JsonProperty("userId", Required = Required.Always)]
        public string UserId { get; set; }
        [JsonProperty("pwd", Required = Required.Always)]
        public string Pwd { get; set; }
        //[JsonProperty("tenantId", Required = Required.Always)]
        public int TenantId { get; set; }
        [JsonProperty("validateCode", Required = Required.Always)]
        public string ValidateCode { get; set; }
        public string GuidKey { get; set; }
    }
}
