using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YC.ServiceWebApi.Dto
{
    public class UploadFileDto
    {
        //public string VersionCode { get; set; }
        //public string Description { get; set; }
        public IFormFile File { get; set; } // file文件
    }
}