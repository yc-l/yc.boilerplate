using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISCOBCOS.CSharpSdk.Dto
{
   public class AccountDto
    {
        [JsonProperty("address", Required = Required.Always)]
        public string Address { get; set; }
        
        [JsonProperty("publicKey", Required = Required.Always)]
        public string PublicKey { get; set; }
        
        [JsonProperty("privateKey", Required = Required.Always)]
        public string PrivateKey { get; set; }
        
        [JsonProperty("userName", Required = Required.Always)]
        public string UserName { get; set; }
       
        [JsonProperty("type", Required = Required.Always)]
        public int Type { get; set; }
    }
}
