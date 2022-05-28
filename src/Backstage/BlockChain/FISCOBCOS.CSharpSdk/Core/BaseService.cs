using FISCOBCOS.CSharpSdk.Dto;
using FISCOBCOS.CSharpSdk.Utils;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.JsonDeserialisation;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.Client.RpcMessages;
using Nethereum.RLP;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FISCOBCOS.CSharpSdk.Core
{
    public class BaseService
    {
        public BaseService(string url, int rpcId, int chainId = 1, int groupId = 1, string privateKey = null)
        {
            this._url = url;
            this._rpcId = rpcId;
            this._chainId = chainId;
            this._groupId = groupId;
            this._gasPrice = BaseConfig.DefaultGasPrice;
            this._gasLimit = BaseConfig.DefaultGasLimit;
            this._requestId = BaseConfig.DefaultRequestId;
            this._requestObjectId = BaseConfig.DefaultRequestObjectId;
            this._transcationValue = BaseConfig.DefaultTranscationsValue;
            this._privateKey = privateKey;
        }

        #region 基础属性
        protected string _url { get; set; }
        protected int _rpcId { get; set; }
        protected int _groupId { get; set; }
        protected int _chainId { get; set; }
        protected int _gasLimit { get; set; }
        protected int _gasPrice { get; set; }
        protected int _requestId { get; set; }
        protected int _requestObjectId { get; set; }
        protected int _transcationValue { get; set; }
        protected string _privateKey { get; set; }
        #endregion

        #region 内部方法

        /// <summary>
        /// 构建交易参数
        /// </summary>
        /// <param name="txData">交易数据</param>
        /// <param name="blockNumber">区块高度</param>
        /// <param name="to">发送地址</param>
        /// <returns>交易参数实体</returns>
        protected TransactionDto BuildTransactionParams(string txData, long blockNumber, string to)
        {
            TransactionDto rawTransaction = new TransactionDto();
            rawTransaction.BlockLimit = blockNumber + 500;//交易防重上限，默认加500
            rawTransaction.Data = txData;//交易数据
            rawTransaction.ExtraData = "";//附加数据，默认为空字符串
            rawTransaction.FiscoChainId = this._chainId;//链ID
            rawTransaction.GasLimit = this._gasLimit;//交易消耗gas上限，默认为30000000
            rawTransaction.GasPrice = this._gasPrice;//默认为30000000
            rawTransaction.Randomid = new Random().Next(10000000, 1000000000); ;
            rawTransaction.To = to;//合约部署默认为空
            rawTransaction.Value = this._transcationValue;//默认为0
            rawTransaction.GroupId = this._groupId;//群组ID 

            return rawTransaction;

        }

        /// <summary>
        /// 创建交易RLP
        /// </summary>
        /// <param name="rawTransaction">交易实体</param>
        /// <returns>RLPSigner</returns>
        protected RLPSigner BuildRLPTranscation(TransactionDto rawTransaction)
        {
            var tx = new RLPSigner(new[] {rawTransaction.Randomid.ToBytesForRLPEncoding(),rawTransaction.GasPrice.ToBytesForRLPEncoding(), rawTransaction.GasLimit.ToBytesForRLPEncoding(),rawTransaction.BlockLimit.ToBytesForRLPEncoding(), rawTransaction.To.HexToByteArray(),  rawTransaction.Value.ToBytesForRLPEncoding(),rawTransaction.Data.HexToByteArray(),rawTransaction.FiscoChainId.ToBytesForRLPEncoding(),
              rawTransaction.GroupId.ToBytesForRLPEncoding(),rawTransaction.ExtraData.HexToByteArray()});
            return tx;
        }


        /// <summary>
        ///同步 获取当前区块高度
        /// </summary>
        /// <param name="rpcId">rpcId</param>
        /// <param name="groupId">群组Id</param>
        /// <returns>当前区块高度</returns>
        public long GetBlockNumber()
        {
            var request = new RpcRequestMessage(this._rpcId, JsonRPCAPIConfig.GetBlockNumber, new object[] { this._groupId });
            var responseResult = HttpUtils.RpcPost<object>(this._url, request);
            long blockNumber = Convert.ToInt64(responseResult.ToString(), 16);
            return blockNumber;
        }

        #endregion

        #region 同步方法
        /// <summary>
        /// 请求发送RPC交易
        /// </summary>
        /// <typeparam name="TResult">返回结果</typeparam>
        /// <param name="txData">交易数据（rlp）</param>
        /// <param name="txSignature">交易签名</param>
        /// <returns>返回交易结果</returns>
        protected TResult SendRequest<TResult>(byte[][] txData, EthECDSASignature txSignature) where TResult : class, new()
        {
            var rlpSignedEncoded = RLPEncoder.EncodeSigned(new SignedData(txData, txSignature), 10).ToHex();
            var request = new RpcRequestMessage(this._requestId, JsonRPCAPIConfig.SendRawTransaction, new object[] { this._requestObjectId, rlpSignedEncoded });
            var result = HttpUtils.RpcPost<TResult>(this._url, request);
            return result;
        }

        /// <summary>
        /// 同步 通用合约部署，只返回交易Hash
        /// </summary>
        /// <param name="binCode">合约内容</param>
        /// <returns>交易Hash</returns>
        public string DeployContract(string binCode, string abi = null, params object[] values)
        {
            var blockNumber = GetBlockNumber();
            var resultData = "";
            ConstructorCallEncoder _constructorCallEncoder = new ConstructorCallEncoder();
            if (values == null || values.Length == 0)
                resultData = _constructorCallEncoder.EncodeRequest(binCode, "");

            var des = new ABIDeserialiser();
            var contract = des.DeserialiseContract(abi);
            if (contract.Constructor == null)
                throw new Exception(
                    "Parameters supplied for a constructor but ABI does not contain a constructor definition");
            resultData = _constructorCallEncoder.EncodeRequest(binCode,
         contract.Constructor.InputParameters, values);

            var transParams = BuildTransactionParams(resultData, blockNumber, "");

            var tx = BuildRLPTranscation(transParams);
            tx.Sign(new EthECKey(this._privateKey.HexToByteArray(), true));
            var result = SendRequest<object>(tx.Data, tx.Signature);
            return Convert.ToString(result);

        }



        /// <summary>
        /// 同步 通用合约部署，返回交易回执
        /// </summary>
        /// <param name="binCode">合约内容</param>
        /// <returns>交易回执</returns>
        public ReceiptResultDto DeployContractWithReceipt(string binCode, string abi = null, params object[] values)
        {
            var txHash = DeployContract(binCode,abi,values);
            var receiptResult = GetTranscationReceipt(txHash);
            return receiptResult;
        }

        /// <summary>
        ///同步 发送交易,返回交易回执
        /// </summary>
        /// <param name="abi">合约abi</param>
        /// <param name="contractAddress">合约地址</param>
        /// <param name="functionName">合约请求调用方法名称</param>
        /// <param name="inputsParameters">方法对应的 参数</param>
        /// <param name="value">请求参数值</param>
        /// <returns>交易回执</returns>
        public ReceiptResultDto SendTranscationWithReceipt(string abi, string contractAddress, string functionName, Parameter[] inputsParameters, params object[] value)
        {
            ReceiptResultDto receiptResult = new ReceiptResultDto();

            var des = new ABIDeserialiser();
            var contract = des.DeserialiseContract(abi);

            var function = contract.Functions.FirstOrDefault(x => x.Name == functionName);
            var sha3Signature = function.Sha3Signature;// "0x53ba0944";
            var functionCallEncoder = new FunctionCallEncoder();
            var result = functionCallEncoder.EncodeRequest(sha3Signature, inputsParameters,
                value);
            var blockNumber = GetBlockNumber();
            var transDto = BuildTransactionParams(result, blockNumber, contractAddress);
            var tx = BuildRLPTranscation(transDto);
            tx.Sign(new EthECKey(this._privateKey.HexToByteArray(), true));
            var txHash = SendRequest<object>(tx.Data, tx.Signature);
            if (txHash != null)
            {
                receiptResult = GetTranscationReceipt(txHash.ToString());
            }
            return receiptResult;
        }

        /// <summary>
        /// 同步 获取交易回执
        /// </summary>
        /// <param name="tanscationHash">交易Hash</param>
        /// <returns></returns>
        public ReceiptResultDto GetTranscationReceipt(string tanscationHash)
        {
            var rpcRequest = new RpcRequestMessage(this._requestId, JsonRPCAPIConfig.GetTransactionReceipt, new object[] { this._requestObjectId, tanscationHash });
            var result = HttpUtils.RpcPost<ReceiptResultDto>(this._url, rpcRequest);
            return result;
        }

        /// <summary>
        /// 同步 Call 调用 适用于链上调用但不需要共识（通常用constant,view等修饰的合约方法）
        /// </summary>
        /// <param name="contractAddress">合约地址</param>
        /// <param name="abi">合约abi</param>
        /// <param name="callFunctionName">调用方法名称</param>
        /// <returns>返回交易回执</returns>
        public ReceiptResultDto CallRequest(string contractAddress, string abi, string callFunctionName, Parameter[] inputsParameters = null, params object[] value)
        {
            CallInput callDto = new CallInput();
            callDto.From = new Account(this._privateKey).Address.ToLower();//address ;
            callDto.To = contractAddress;
            var contractAbi = new ABIDeserialiser().DeserialiseContract(abi);

            callDto.Value = new HexBigInteger(0);
            var function = contractAbi.Functions.FirstOrDefault(x => x.Name == callFunctionName);

            var sha3Signature = function.Sha3Signature;// "0x53ba0944";

            if (inputsParameters == null)
            {
                callDto.Data = "0x" + sha3Signature;
            }
            else
            {
                var functionCallEncoder = new FunctionCallEncoder();
                var funcData = functionCallEncoder.EncodeRequest(sha3Signature, inputsParameters,
                    value);
                callDto.Data =  funcData;
            }

            var request = new RpcRequestMessage(this._requestId, JsonRPCAPIConfig.Call, new object[] { this._requestObjectId, callDto });
            var result = HttpUtils.RpcPost<ReceiptResultDto>(this._url, request); ;

            return result;

        }


        /// <summary>
        /// rpc 其他接口查询
        /// </summary>
        /// <param name="apiName">api请求接口名</param>
        /// <param name="paramsValue">对应的请求参数</param>
        /// <returns>返回值，根据文档中对应api 返回object </returns>
        public TResult SendQuery<TResult>(string apiName, params object[] paramsValue) where TResult : class, new()
        {
            var request = new RpcRequestMessage(this._rpcId, apiName, paramsValue);
            var result = HttpUtils.RpcPost<TResult>(this._url, request);
            return result;
        }
        #endregion
    }
}
