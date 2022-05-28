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
using YC.ElasticSearch.Models;
using YC.Model;
using YC.Model.BlockChainEntity;
using YC.Model.SysDbEntity;

namespace YC.ApplicationService
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<SysUser, UserDto>((MemberList.None)).ReverseMap();
            CreateMap<SysUser, PersonInfoDto>((MemberList.None)).ReverseMap();
            CreateMap<SysUser, PageUserOutputDto>((MemberList.None)).ReverseMap();
            CreateMap<SysUser, UserAddOrEditDto>((MemberList.None)).
                ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<SysRole, SysRoleAddOrEditDto>((MemberList.None)).ReverseMap();
            CreateMap<SysPermission, SysPermissionDto>((MemberList.None)).ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.GetEnumName((PermissionType)src.Type))).ReverseMap();
            CreateMap<SysPermission, SysPermissionAddOrEditDto>((MemberList.None)).ReverseMap();
            CreateMap<SysRole, RoleInfoDto>((MemberList.None))
                 .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<SysDataDictionary, SysDataDictionaryAddOrEditDto>((MemberList.None)).ReverseMap();
            CreateMap<SysDataDictionary, SysDataDictionaryDto>((MemberList.None)).ReverseMap();
            CreateMap<SysUser, UserRolePermissionDto>((MemberList.None)).ReverseMap();
            CreateMap<SysAuditLog, SysAuditLogAddOrEditDto>((MemberList.None)).ReverseMap();
            CreateMap<SysAuditLog, SysAuditLogDto>((MemberList.None)).ReverseMap();

            CreateMap<SysOrganization, SysOrganizationAddOrEditDto>((MemberList.None)).ReverseMap();
            CreateMap<SysOrganization, SysOrganizationDto>((MemberList.None)).ReverseMap();

            CreateMap<Book, BookAddOrEditDto>((MemberList.None)).ReverseMap();
            CreateMap<Book, BookDto>((MemberList.None)).ReverseMap();

            CreateMap<BCEvidence, BCEvidenceCreateDto>((MemberList.None)).ReverseMap();
            CreateMap<BCEvidence, BCEvidenceAddOrEditDto>((MemberList.None)).ReverseMap();
            CreateMap<BCEvidence, BCEvidenceDto>((MemberList.None)).ReverseMap();
        }
    }
}