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
        public string contractAddress = "0x196bcc72893d357492ff59c88631a60cf31f94d8";
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
                BuildParams.CreateParam("string", "typeName"),
                BuildParams.CreateParam("string", "dataValue") };
            var paramsValue = new object[] { rtv.ServiceId, rtv.TypeName, rtv.DataValue };
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
                BuildParams.CreateParam("string", "typeName"),
                BuildParams.CreateParam("string", "dataValue") };
            var paramsValue = new object[] { rtv.ServiceId, rtv.TypeName, rtv.DataValue };
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
                callTraceEvidence.ListKeyValueDtos = new List<KeyValueDto>();
               
                for (int i = 0; i < callTraceEvidence.Count; i++) {
                    string[] typeList = outputList[1].Result.ToJson().ToObject<string[]>();
                    string[] dataList = (string[])outputList[2].Result.ToJson().ToObject<string[]>();
                    callTraceEvidence.ListKeyValueDtos.Add(new KeyValueDto() {Key= typeList[i],Value= dataList[i] });
                }
               
            }
            return callTraceEvidence;



        }
    }
}
