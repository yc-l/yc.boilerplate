using FISCOBCOS.CSharpSdk.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using FISCOBCOS.CSharpSdk;
using FISCOBCOS.CSharpSdk.Core;
using FISCOBCOS.CSharpSdk.Dto;
using System.Threading.Tasks;
using FISCOBCOS.CSharpSdk.Utis;

namespace FISCOBCOS.CSharpSdk
{
    public class TraceEvidenceContractService
    {
        public string privateKey = "";
        string binCode = "";
        string abi = "";
        public string contractAddress = "0x294840a676a3d51dd8b30dd6ce2829b02930e6f1";
        public TraceEvidenceContractService()
        {

            this.privateKey = "0x25aa95ed437f8efaf37cf849a5a6ba212308d5d735105e03e38410542bf1d5ff";
            bool getAbiState = FileUtils.ReadFile(AppContext.BaseDirectory + "\\Contract\\TraceEvidence\\" + "EvidenceConV1.abi", out abi);
            bool getBinCodeState = FileUtils.ReadFile(AppContext.BaseDirectory + "\\Contract\\TraceEvidence\\" + "EvidenceConV1.bin", out binCode);

        }

        /// <summary>
        /// 异步调用合约方法 进行区块链存证
        /// </summary>
        /// <returns></returns>
  
        public async Task<RegisterTraceEvidenceRes>  PushEvidenceAsync(RegisterTraceEvidence rtv)
        {

            var contractService = new ContractService(BaseConfig.DefaultUrl, BaseConfig.DefaultRpcId, BaseConfig.DefaultChainId, BaseConfig.DefaultGroupId, privateKey);
          
            var inputsParameters = new[] { BuildParams.CreateParam("string", "serviceId"),
                BuildParams.CreateParam("string[]", "eviData"),
                BuildParams.CreateParam("string", "dataValue") };
            var paramsValue = new object[] { rtv.ServiceId, new string[] { rtv.TypeName, rtv.BusinessFlowId, rtv.BehaviorTypeId }, rtv.DataValue };
            string functionName = "registerServiceData";//调用合约方法

            ReceiptResultDto receiptResultDto = await contractService.SendTranscationWithReceiptAsync(abi, contractAddress, functionName, inputsParameters, paramsValue);
            var solidityAbi = new SolidityABI(abi);
          
            RegisterTraceEvidenceRes res = new RegisterTraceEvidenceRes();

            res.BlockChainRes = receiptResultDto;
            if (receiptResultDto.Output!=null) {
                var inputList = solidityAbi.InputDecode(functionName, receiptResultDto.Input);
                var outputList = solidityAbi.OutputDecode(functionName, receiptResultDto.Output);

                res.State = Convert.ToInt32(outputList[0].Result.ToString());
                res.Message = outputList[1].Result.ToString();
            }
            return res;



        }

        /// <summary>
        /// 异步调用合约方法 进行区块链存证,仅获取交易hash
        /// </summary>
        /// <returns></returns>

        public async Task<string> PushEvidenceGeTransHashAsync(RegisterTraceEvidence rtv)
        {

            var contractService = new ContractService(BaseConfig.DefaultUrl, BaseConfig.DefaultRpcId, BaseConfig.DefaultChainId, BaseConfig.DefaultGroupId, privateKey);

            var inputsParameters = new[] { BuildParams.CreateParam("string", "serviceId"),
                BuildParams.CreateParam("string[]", "eviData"),
                BuildParams.CreateParam("string", "dataValue") };
            var paramsValue = new object[] { rtv.ServiceId, new string[] { rtv.TypeName, rtv.BusinessFlowId, rtv.BehaviorTypeId }, rtv.DataValue };
            string functionName = "registerServiceData";//调用合约方法

           string txHash = await contractService.SendTranscationWithTransHashAsync(abi, contractAddress, functionName, inputsParameters, paramsValue);
            return txHash;



        }

        /// <summary>
        /// 异步查询存证
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>

        public async Task<CallTraceEvidence> CallGetServiceListAsync(string serviceId)
        {
            var contractService = new ContractService(BaseConfig.DefaultUrl, BaseConfig.DefaultRpcId, BaseConfig.DefaultChainId, BaseConfig.DefaultGroupId, privateKey);
            string functionName = "getServiceList";

            var inputsParameters = new[] { BuildParams.CreateParam("string", "serviceId") };
            var paramsValue = new object[] { serviceId };
            var result = await contractService.CallRequestAsync(contractAddress, abi, functionName, inputsParameters, paramsValue);
            var solidityAbi = new SolidityABI(abi);
            CallTraceEvidence callTraceEvidence = new CallTraceEvidence();
            callTraceEvidence.ReceiptDto = result;
            if (result.Output != null) {
                var outputList = solidityAbi.OutputDecode(functionName, result.Output);
                callTraceEvidence.ReceiptDto = result;
                callTraceEvidence.Count= Convert.ToInt32(outputList[0].Result.ToString());
                callTraceEvidence.EviDataDtos = new List<EviDataDto>();
               
                for (int i = 0; i < callTraceEvidence.Count; i++) {
                    string[] businessFlowIdList = outputList[1].Result.ToJson().ToObject<string[]>();
                    string[] behaviorTypeList = outputList[2].Result.ToJson().ToObject<string[]>();
                    string[] dataList = (string[])outputList[3].Result.ToJson().ToObject<string[]>();
                    callTraceEvidence.EviDataDtos.Add(new EviDataDto() {
                        BehaviorTypeId=behaviorTypeList[i],
                        BusinessFlowId=businessFlowIdList[i],
                    DataValue=dataList[i]});
                }
               
            }
            return callTraceEvidence;



        }

        /// <summary>
        /// 查询存证是否成功！
        /// </summary>
        /// <param name="txHash"></param>
        /// <returns></returns>
        public async Task<bool> GetTransStateByTxHash(string txHash) {

            bool state = false;
            var contractService = new ContractService(BaseConfig.DefaultUrl, BaseConfig.DefaultRpcId, BaseConfig.DefaultChainId, BaseConfig.DefaultGroupId, privateKey);
            var result = contractService.GetTranscationReceipt(txHash);
            if (result?.Status == "0x0")
            {
                state = true;
            }
            return state;
        } 
    }
}
