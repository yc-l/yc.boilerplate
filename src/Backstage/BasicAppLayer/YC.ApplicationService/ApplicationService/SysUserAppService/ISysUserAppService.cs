using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using YC.ApplicationService.ApplicationService.SysUserAppService.Dto;
using YC.ApplicationService.ApplicationService.SysUserAppService.Dto.Input;
using YC.ApplicationService.Dto;
using YC.ApplicationService.SysUserAppService;
using YC.ApplicationService.SysUserAppService.Dto;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Domain;
using YC.Core.DynamicApi;
using YC.Model.SysDbEntity;

namespace YC.ApplicationService
{
    public interface ISysUserAppService : IApplicationService, IDependencyInjectionSupport
    {
        List<SysUser> GetAllSysUserList();

        Task<PersonInfoDto> GetUserInfo();
        Task<IApiResult> UploadUserAvatar(IFormFileCollection formFiles);
        Task<IApiResult> UpdateUserAvatar(long id, string filePath);

        IApiResult<SysUser> Login(string userId, string pwd, int TenantId = 0);

        Task<IApiResult> ChangePassword(ChangePasswordInputDto input);

        Task<IApiResult> GetPageUserListAsync(PageInput<PageInputDto> input);

        Task<IApiResult> CreateUserAsync(UserAddOrEditDto input);

        Task<IApiResult> DeleteUserByIdAsync(long id);

        Task<IApiResult> UpdateUserAsync(UserAddOrEditDto input);

        Task<ApiResult<UserAddOrEditDto>> GetAsync(long id);

        IApiResult<UserRolePermissionDto> GetUserRolePermission(long id);
    }
}