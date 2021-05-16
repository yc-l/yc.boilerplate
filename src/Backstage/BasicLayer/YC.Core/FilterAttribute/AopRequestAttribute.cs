using System;
using System.Collections.Generic;
using System.Text;

namespace YC.Core.Attribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]

    public class AopRequestAttribute : System.Attribute
    {
    }
}
