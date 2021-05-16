
using System.Collections.Generic;
using System.Threading.Tasks;
using YC.ApplicationService.ApplicationService.SysRoleAppService.Dto;
using YC.ApplicationService.Dto;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Domain;
using YC.Core.DynamicApi;
using YC.Model;

namespace YC.ApplicationService
{

    /// <summary>
    ///  角色业务接口
    /// </summary>
    public interface ISysRoleAppService : IApplicationService, IDependencyInjectionSupport
    {


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>返回数据集合</returns>
        Task<IApiResult> GetPageSysRoleListAsync(PageInput<PageInputDto> input);

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <returns></returns>
        Task<IApiResult> GetSysRoleListAsync();

       
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> CreateSysRoleAsync(SysRoleAddOrEditDto input);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> DeleteSysRoleByIdAsync(long id);

        /// <summary>
        /// 更新角色权限关系
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IApiResult> UpdateSysRolePermissionsAsync(UpdateSysRolePermissionsDto input);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> UpdateSysRoleAsync(SysRoleAddOrEditDto input);
        /// <summary>
        /// 通过主键Id获取对象
        /// </summary>
        /// <returns>返回对象结果</returns>
        Task<ApiResult<SysRoleAddOrEditDto>> GetAsync(long id);


    }

}


