using Nethereum.ABI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FISCOBCOS.CSharpSdk.Core
{
    public class BuildParams
    {
        public static Parameter CreateParam(string type, string name, Type decodedType = null)
        {
            return new Parameter(type, name) { DecodedType = decodedType };
        }
    }
}
