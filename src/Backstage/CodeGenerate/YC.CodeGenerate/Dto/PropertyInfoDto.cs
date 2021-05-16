using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.CodeGenerate.Dto
{
    public class PropertyInfoDto
    {
        /// <summary>
        /// 属性的显示名称
        /// </summary>
        public string DisPlayName { get; set; }
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 属性数据类型
        /// </summary>
        public string TypeName { get; set; }
       
        /// <summary>
        /// 是主键时候，的类型
        /// </summary>
        public string IsKeyWithIdType { get; set; }

        /// <summary>
        /// 如果是string 类型，那么返回它的长度
        /// </summary>
        public int StringTypeLength { get; set; }

        /// <summary>
        /// 首字母缩写属性名称
        /// </summary>
        public string InitialsToLowerPropertyName { get; set; }
    }
}
