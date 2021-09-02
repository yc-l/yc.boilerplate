
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YC.Core
{
    /// <summary>
    /// 标识是否映射生成表
    /// </summary>
    public class IsMappedToCreateAttribute : System.Attribute
    {

        public IsCreate IsCreate
        {
            get;
            set;
        }

        public IsMappedToCreateAttribute(IsCreate isCreate)//在实例化的时候，就指定Block是Yes还是No
        {
            IsCreate = isCreate;
        }
    }

    public enum IsCreate
    {
        NO,
        Yes
    }

}
