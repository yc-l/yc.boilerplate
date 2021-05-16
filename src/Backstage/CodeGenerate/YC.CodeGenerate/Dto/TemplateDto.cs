using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.CodeGenerate.Dto
{
    public class TemplateDto
    {
        public string AddOrEditDtoFilePath { get; set; }
        public string EntityDtoFilePath { get; set; }
        public string ServiceFilePath { get; set; }
        public string IServiceFilePath{ get; set; }
        public string VueFilePath { get; set; }
        public string SaveDir { get; set; }

        public string TreeServiceFilePath { get; set; }
        public string TreeIServiceFilePath { get; set; }
        public string TreeVueFilePath { get; set; }

    }
}
