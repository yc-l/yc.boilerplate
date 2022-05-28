using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISCOBCOS.CSharpSdk.Dto
{

    /// <summary>
    /// 交易回执Dto
    /// </summary>
    public class ReceiptResultDto
    {
        /// <summary>
        /// 区块链Hash
        /// </summary>
        [JsonProperty("blockHash")]
        public string BlockHash { get; set; }
        /// <summary>
        /// 区块高度
        /// </summary>
        [JsonProperty("blockNumber")]
        public HexBigInteger BlockNumber { get; set; }
        /// <summary>
        /// 合约地址
        /// </summary>
        [JsonProperty("contractAddress")]
        public string ContractAddress { get; set; }
        /// <summary>
        /// 调用者
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("gasUsed")]
        public HexBigInteger GasUsed { get; set; }
        /// <summary>
        /// 输入内容
        /// </summary>
        [JsonProperty("input")]
        public string Input { get; set; }
        /// <summary>
        ///   日志  logs: Array - Array of log objects, which this transaction generated.
        /// </summary>
        [JsonProperty(PropertyName = "logs")]
        public BcosFilterLog[] Logs { get; set; }
        /// <summary>
        /// 布隆过滤器
        /// </summary>
        [JsonProperty("logsBloom")]
        public string LogsBloom { get; set; }
        /// <summary>
        /// 输出内容
        /// </summary>
        [JsonProperty("output")]
        public string Output { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
        /// <summary>
        /// 接受方
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }
        /// <summary>
        /// 交易hash
        /// </summary>
        [JsonProperty("transactionHash")]
        public string TransactionHash { get; set; }
        /// <summary>
        /// 交易Index
        /// </summary>
        [JsonProperty("transactionIndex")]
        public string transactionIndex { get; set; }
    }

    [Serializable]
    public class BcosFilterLog
    {
        [JsonProperty(PropertyName = "removed")]
        public bool Removed { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "logIndex")]
        public HexBigInteger LogIndex { get; set; }
        [JsonProperty(PropertyName = "transactionHash")]
        public string TransactionHash { get; set; }
        [JsonProperty(PropertyName = "transactionIndex")]
        public HexBigInteger TransactionIndex { get; set; }
        [JsonProperty(PropertyName = "blockHash")]
        public string BlockHash { get; set; }
        [JsonProperty(PropertyName = "blockNumber")]
        public HexBigInteger BlockNumber { get; set; }
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "data")]
        public string Data { get; set; }
        [JsonProperty(PropertyName = "topics")]
        public object[] Topics { get; set; }
        [JsonProperty(PropertyName = "blockNumberRaw")]
        public string BlockNumberRaw { get; set; }
        [JsonProperty(PropertyName = "transactionIndexRaw")]
        public string TransactionIndexRaw { get; set; }
        [JsonProperty(PropertyName = "logIndexRaw")]
        public string LogIndexRaw { get; set; }
    }
}
