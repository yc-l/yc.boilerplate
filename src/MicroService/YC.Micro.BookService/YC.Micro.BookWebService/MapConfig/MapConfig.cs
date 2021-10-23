using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ElasticSearch.Models;
using YC.Model;
using YC.Model.SysDbEntity;

namespace YC.Micro.BookWebService
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<Book, BookDto>((MemberList.None)).ReverseMap();
            CreateMap<List<Book>, BookDtoList>((MemberList.None)).ForMember(dest => dest.BookDto, opt => opt.MapFrom(src => src.ToList())).ReverseMap();
        }
    }

}
