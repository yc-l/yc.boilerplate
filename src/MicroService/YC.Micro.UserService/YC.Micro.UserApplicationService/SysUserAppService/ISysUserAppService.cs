using System.Collections.Generic;
using System.Threading.Tasks;
using YC.Micro.UserApplicationService.ApplicationService.SysUserAppService.Dto;
using YC.Micro.UserApplicationService.Dto;
using YC.Micro.UserApplicationService.SysUserAppService;
using YC.Micro.UserApplicationService.SysUserAppService.Dto;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Domain;
using YC.Core.DynamicApi;
using YC.Model.SysDbEntity;

namespace YC.Micro.UserApplicationService
{
    public interface ISysUserAppService : IApplicationService, IDependencyInjectionSupport
    {
        List<SysUser> GetAllSysUserList();
       

        IApiResult<SysUser> Login(string userId, string pwd, int TenantId = 0);

        List<SysUser> GetUserList(object condition);

         Task<IApiResult> GetPageUserListAsync(PageInput<PageInputDto> input);
        Task<IApiResult> CreateUserAsync(UserAddOrEditDto input);

        Task<IApiResult> DeleteUserByIdAsync(long id);
        Task<IApiResult> UpdateUserAsync(UserAddOrEditDto input);
        Task<ApiResult<UserAddOrEditDto>> GetAsync(long id);
        IApiResult<UserRolePermissionDto> GetUserRolePermission(long id);
    }
}