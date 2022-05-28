using System;
using System.Collections.Generic;
using System.Text;

namespace FISCOBCOS.CSharpSdk
{
    public class BaseConfig
    {
        /// <summary>
        /// 链上jsonapi 通信地址 通常是ip：8545端口
        /// </summary>       
        public static string DefaultUrl = "http://127.0.0.1:8545";
       
        /// <summary>
        /// 默认链Id
        /// </summary>
        public static int DefaultChainId = 1;

        /// <summary>
        /// 默认群组Id
        /// </summary>
        public static int DefaultGroupId = 1;

        /// <summary>
        /// rpc 请求默认标识Id
        /// </summary>
        public static int DefaultRpcId = 1;
        /// <summary>
        /// 默认GasLimit
        /// </summary>
        public static int DefaultGasLimit = 30000000;

        /// <summary>
        /// 默认GasPrice
        /// </summary>
        public static int DefaultGasPrice = 30000000;

        /// <summary>
        /// 默认构建请求Id
        /// </summary>
        public static int DefaultRequestId = 1;

        /// <summary>
        /// 默认构建请求 参数Id
        /// </summary>
        public static int DefaultRequestObjectId = 1;

        /// <summary>
        /// 默认交易参数Value
        /// </summary>
        public static int DefaultTranscationsValue = 0;
    }
}
