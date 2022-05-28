using FISCOBCOS.CSharpSdk.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISCOBCOS.CSharpSdk
{
    public class RegisterTraceEvidenceRes
    {
      

        /// <summary>
        /// 状态码
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get;  set; }

        public ReceiptResultDto BlockChainRes { get; set; }
    }
}
