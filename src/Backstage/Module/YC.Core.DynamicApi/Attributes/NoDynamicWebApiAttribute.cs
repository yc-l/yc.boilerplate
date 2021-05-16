using System;

namespace YC.Core.DynamicApi.Attributes
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public class NoDynamicWebApiAttribute:Attribute
    {
        
    }
}