using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.CodeGenerate.Dto;

namespace YC.CodeGenerate
{
    public class GenerateCodeConfig
    {
        public List<GenerateCodeEnumType> WantToGenerateCodeTypeList { get; set; }

        public List<string> GenerateEntityList { get; set; }

        public TemplateDto Template { get; set; }

        public bool IsTree { get; set; }

        /// <summary>
        /// 去哪个程序集查找指定的Entity
        /// </summary>
        public string AssemblyName { get; set; }
    }
}
