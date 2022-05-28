using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace FISCOBCOS.CSharpSdk.Dto
{
    /// <summary>
    /// 交易Dto
    /// </summary>
    [Serializable]
    public class TransactionDto
    {

        /// <summary>
        /// 随机数，用于交易防重
        /// </summary>
        [JsonProperty("randomid", Required = Required.Always)]
        public BigInteger Randomid { get; set; }

        /// <summary>
        /// 默认为30000000
        /// </summary>
        [JsonProperty("gasPrice", Required = Required.Always)]
        public BigInteger GasPrice { get; set; }

        /// <summary>
        /// 交易消耗gas上限，默认为30000000
        /// </summary>
        [JsonProperty("gasLimit", Required = Required.Always)]
        public BigInteger GasLimit { get; set; }

        /// <summary>
        /// 交易防重上限，默认为当前最新区块高度+500
        /// </summary>
        [JsonProperty("blockLimit", Required = Required.Always)]
        public long BlockLimit { get; set; }

        /// <summary>
        /// 一般为合约地址
        /// </summary>
        [JsonProperty("to", Required = Required.Always)]
        public String To { get; set; }

        /// <summary>
        /// 默认为0
        /// </summary>
        [JsonProperty("value", Required = Required.Always)]
        public BigInteger Value { get; set; }

        /// <summary>
        /// 交易数据
        /// </summary>
        [JsonProperty("data", Required = Required.Always)]
        public String Data { get; set; }

        /// <summary>
        /// 链Id
        /// </summary>
        [JsonProperty("fiscoChainId", Required = Required.Always)]
        public BigInteger FiscoChainId { get; set; }

        /// <summary>
        /// 群组Id
        /// </summary>
        [JsonProperty("groupId", Required = Required.Always)]
        public BigInteger GroupId { get; set; }

        /// <summary>
        /// 附加数据，默认为空字符串
        /// </summary>
        [JsonProperty("extraData", Required = Required.Always)]
        public String ExtraData { get; set; }
    }
}
