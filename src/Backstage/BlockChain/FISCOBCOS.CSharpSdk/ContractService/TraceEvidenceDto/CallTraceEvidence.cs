using FISCOBCOS.CSharpSdk.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISCOBCOS.CSharpSdk
{
    public class CallTraceEvidence
    {
        public ReceiptResultDto ReceiptDto { get; set; }
        public int Count { get; set; }
        public List<EviDataDto> EviDataDtos { get; set; }

    }

    public class  EviDataDto {
        public string BusinessFlowId { get; set; }
        public string BehaviorTypeId { get; set; }
        public string DataValue { get; set; }
        public string TransHash { get; set; }
    }
}
