using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService.ApplicationService.Dto;
using YC.ApplicationService.ApplicationService.SysUserAppService.Dto;
using YC.ApplicationService.Dto;
using YC.ApplicationService.SysUserAppService.Dto;
using YC.Model;
using YC.Model.SysDbEntity;
using YC.ServiceWebApi.Filter.Dto;

namespace YC.ApplicationService
{
    public class AppMapConfig : Profile
    {
        public AppMapConfig()
        {
           
            CreateMap<RequestInfoDto, SysAuditLogAddOrEditDto>((MemberList.None)).ReverseMap();

        }
    }

}
