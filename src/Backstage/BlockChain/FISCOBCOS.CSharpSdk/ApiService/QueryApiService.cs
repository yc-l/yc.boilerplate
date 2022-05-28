using FISCOBCOS.CSharpSdk.Core;
using Nethereum.JsonRpc.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FISCOBCOS.CSharpSdk.ApiService
{
    /// <summary>
    /// 基础json rpc api 查询服务
    /// </summary>
    public class QueryApiService:BaseService
    {
        protected RpcClient _rpcClient;
        public QueryApiService(string url, int rpcId):base(url,rpcId)
        {
            _rpcClient = new RpcClient(new Uri(url));
            this._rpcId = rpcId;

        }

        #region 基础属性
        protected int _rpcId { get; set; }

        /// <summary>
        /// 异步 rpc 其他接口相对简单，参数要求参照单元测试 
        /// 具体参考：https://fisco-bcos-documentation.readthedocs.io/zh_CN/latest/docs/api.html
        /// 采用在线json 转实体等方式进行自己该做转化
        /// </summary>
        /// <param name="apiName">api请求接口名</param>
        /// <param name="paramsValue">对应的请求参数</param>
        /// <returns>返回值，根据文档中对应api 返回object </returns>
        #endregion
        public async Task<TResult> SendQueryAsync<TResult>(string apiName, params object[] paramsValue)
        {
            var request = new RpcRequest(this._rpcId, apiName, paramsValue);
            var responseResult = await _rpcClient.SendRequestAsync<TResult>(request);
            return responseResult;
        }

    }
}
