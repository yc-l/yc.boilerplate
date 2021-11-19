using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.FreeSqlFrameWork;
using YC.Micro.UserWebService;
using YC.Model.SysDbEntity;

namespace YC.Micro.UserWebService
{
    [Authorize]
    public class UserService : IUserService.IUserServiceBase
    {
        public IFreeSqlRepository<SysUser> _sysUserFreeSqlRepository;
        private readonly IMapper _mapper;

        public UserService(IFreeSqlRepository<SysUser> sysUserFreeSqlRepository, IMapper mapper)
        {
            _sysUserFreeSqlRepository = sysUserFreeSqlRepository;
            _mapper = mapper;
        }

        public override async Task<UserDtoList> GetUserList(UserFormRequest request, ServerCallContext context)
        {
            // UserDtoList userDtoList = new UserDtoList();
            var dataList = await _sysUserFreeSqlRepository.Where(x => x.Id == request.Id).ToListAsync();

            var result = _mapper.Map<UserDtoList>(dataList);

            //var result = _mapper.Map<List<UserDto>>(dataList);
            //userDtoList.UserDto.Add(result);

            return result;
        }
    }
}