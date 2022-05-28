using System;
using System.Collections.Generic;
using System.Text;

namespace FISCOBCOS.CSharpSdk
{
    /// <summary>
    ///  JsonRPCAPI 方法配置
    /// </summary>
    public static class JsonRPCAPIConfig
    {
        /// <summary>
        /// 返回节点的版本信息
        /// </summary>
        public static string GetClientVersion = "getClientVersion";

        /// <summary>
        /// 返回节点指定群组内的最新区块高度
        /// </summary>
        public static string GetBlockNumber = "getBlockNumber";

        /// <summary>
        /// 返回节点所在指定群组内的最新PBFT视图
        /// </summary>
        public static string GetPbftView = "getPbftView";
        /// <summary>
        /// 返回指定群组内的共识节点列表
        /// </summary>
        public static string GetSealerList = "getSealerList";

        /// <summary>
        /// 返回指定群组内的观察节点列表
        /// </summary>
        public static string GetObserverList = "getObserverList";

        /// <summary>
        /// 返回指定群组内的共识状态信息
        /// </summary>
        public static string GetConsensusStatus = "getConsensusStatus";
        /// <summary>
        /// 返回指定群组内的同步状态信息
        /// </summary>
        public static string GetSyncStatus = "getSyncStatus";

        /// <summary>
        /// 返回已连接的p2p节点信息
        /// </summary>
        public static string GetPeers = "getPeers";
        /// <summary>
        /// 返回指定群组内的共识节点和观察节点列表
        /// </summary>
        public static string GetGroupPeers = "getGroupPeers";

        /// <summary>
        /// 返回节点本身和已连接的p2p节点列表
        /// </summary>
        public static string GetNodeIDList = "getNodeIDList";

        /// <summary>
        /// 返回节点所属群组的群组ID列表
        /// </summary>
        public static string GetGroupList = "getGroupList";
        /// <summary>
        /// 返回根据区块哈希查询的区块信息
        /// </summary>
        public static string GetBlockByHash = "getBlockByHash";
        /// <summary>
        /// 返回根据区块高度查询的区块信息
        /// </summary>
        public static string GetBlockByNumber = "getBlockByNumber";
        /// <summary>
        /// 返回根据区块高度查询的区块哈希
        /// </summary>
        public static string GetBlockHashByNumber = "getBlockHashByNumber";
        /// <summary>
        /// 返回根据交易哈希查询的交易信息
        /// </summary>
        public static string GetTransactionByHash = "getTransactionByHash";
        /// <summary>
        /// 返回根据区块哈希和交易序号查询的交易信息
        /// </summary>
        public static string GetTransactionByBlockHashAndIndex = "getTransactionByBlockHashAndIndex";

        /// <summary>
        /// 返回根据区块高度和交易序号查询的交易信息
        /// </summary>
        public static string GetTransactionByBlockNumberAndIndex = "getTransactionByBlockNumberAndIndex";
        /// <summary>
        /// 返回根据交易哈希查询的交易回执信息
        /// </summary>
        public static string GetTransactionReceipt = "getTransactionReceipt";
        /// <summary>
        /// 返回待打包的交易信息
        /// </summary>
        public static string GetPendingTransactions = "getPendingTransactions";
        /// <summary>
        /// 返回待打包的交易数量
        /// </summary>
        public static string GetPendingTxSize = "getPendingTxSize";
        /// <summary>
        /// 返回根据合约地址查询的合约数据
        /// </summary>
        public static string GetCode = "getCode";
        /// <summary>
        /// 返回当前交易总数和区块高度
        /// </summary>
        public static string GetTotalTransactionCount = "getTotalTransactionCount";
        /// <summary>
        /// 返回根据key值查询的value值
        /// </summary>
        public static string GetSystemConfigByKey = "getSystemConfigByKey";
        /// <summary>
        /// 执行一个可以立即获得结果的请求，无需区块链共识
        /// </summary>
        public static string Call = "call";
        /// <summary>
        /// 执行一个签名的交易，需要区块链共识
        /// </summary>
        public static string SendRawTransaction = "sendRawTransaction";


    }
}
