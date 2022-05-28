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
        public List<KeyValueDto> ListKeyValueDtos { get; set; }

    }

    public class KeyValueDto { 
    
    public string Key { get; set; }
    public string Value { get; set; }
    }
}
