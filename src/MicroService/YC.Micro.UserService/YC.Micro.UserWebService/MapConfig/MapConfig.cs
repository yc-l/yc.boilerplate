using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Model;
using YC.Model.SysDbEntity;

namespace YC.Micro.UserWebService
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<SysUser, UserDto>((MemberList.None)).ReverseMap();
            CreateMap<List<SysUser>, UserDtoList>((MemberList.None)).ForMember(dest => dest.UserDto, opt => opt.MapFrom(src => src.ToList())).ReverseMap();
        }
    }

}
