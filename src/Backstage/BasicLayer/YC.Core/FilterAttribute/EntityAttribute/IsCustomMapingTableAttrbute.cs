
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
    public class IsCustomMapingTableAttrbute : System.Attribute
    {


        public IsCustomMapingTableAttrbute(string mappingTableName, string prefix="_", bool isMapping = true)
        {
            
        
        }
    }

    

}