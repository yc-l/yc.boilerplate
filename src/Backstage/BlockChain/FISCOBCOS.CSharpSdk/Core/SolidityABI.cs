using FISCOBCOS.CSharpSdk.Dto;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.JsonDeserialisation;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FISCOBCOS.CSharpSdk.Core
{
    /// <summary>
    /// 合约解析
    /// </summary>
    public class SolidityABI
    {

        public ContractABI contractAbi;

        public SolidityABI(string abi)
        {
            contractAbi = new ABIDeserialiser().DeserialiseContract(abi);
        }

        /// <summary>
        /// 解析Input
        /// </summary>
        /// <param name="functionName">对应的input 方法名</param>
        /// <param name="encodeInput">原始input</param>
        /// <returns>解析后的合约input参数集合</returns>
        public List<ParameterOutput> InputDecode(string functionName, string encodeInput)
        {
            var function = contractAbi.Functions.FirstOrDefault(e => e.Name == functionName);
            var functionBuilder = new FunctionBuilder("", function);
            List<ParameterOutput> functionDecode = functionBuilder.DecodeInput(encodeInput);
            return functionDecode;

        }

        /// <summary>
        /// 解析output
        /// </summary>
        /// <param name="functionName">对应的output 方法名</param>
        /// <param name="encodeInput">原始output</param>
        /// <returns>解析后的合约output参数集合</returns>
        public List<ParameterOutput> OutputDecode(string functionName, string encodeOutput)
        {
            var function = contractAbi.Functions.FirstOrDefault(e => e.Name == functionName);
            var functionBuilder = new FunctionBuilder("", function);
            List<ParameterOutput> functionDecode = functionBuilder.DecodeOutput(encodeOutput);
            return functionDecode;

        }

        /// <summary>
        /// 解析output
        /// </summary>
        /// <typeparam name="FunctionOutputDTO">解析output Dto</typeparam>
        /// <param name="encodeOutput">原始output</param>
        /// <returns>解析后得到对象</returns>
        public FunctionOutputDTO OutputDecode<FunctionOutputDTO>(FunctionOutputDTO outputDto, string encodeOutput)
        {
            var functionCallDecoder = new FunctionCallDecoder();
            var outputData = functionCallDecoder.DecodeFunctionOutput<FunctionOutputDTO>(outputDto, encodeOutput);
            return outputData;
        }

        /// <summary>
        /// 解析Event
        /// </summary>
        /// <param name="eventName">even名称</param>
        /// <param name="logs">对应logs</param>
        /// <returns>返回解析后的Event数据</returns>
        public List<EventLog<List<ParameterOutput>>> EventDecode(string eventName, BcosFilterLog[] logs)
        {
            var setEventAbi = contractAbi.Events.FirstOrDefault(e => e.Name == eventName);
            var list = EventExtensions.DecodeAllEventsDefaultTopics(setEventAbi, JArray.FromObject(logs));
            return list;
        }

    }
}
