using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService.Dto;

namespace YC.ApplicationService.ApplicationService.Dto
{
    public class PageUserOutputDto
    {

        public long Id { get; set; }

        public string NO { get; set; }


        public string Name { get; set; }

        public string Account { get; set; }

        public int Sex { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Remark { get; set; }

     
    }
}
