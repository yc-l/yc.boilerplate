
using System.Collections.Generic;
using System.Threading.Tasks;
using YC.ApplicationService.Dto;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Domain;
using YC.Core.Domain.Entity;
using YC.Core.DynamicApi;
using YC.Model;
using YC.Model.SysDbEntity;

namespace YC.ApplicationService
{

    /// <summary>
    ///  权限业务接口
    /// </summary>
    public interface ISysPermissionAppService : IApplicationService, IDependencyInjectionSupport
    {


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>返回数据集合</returns>
        Task<IApiResult> GetPageSysPermissionListAsync(PageInput<PageInputDto> input);
      
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> CreateSysPermissionAsync(SysPermissionAddOrEditDto input);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> DeleteSysPermissionByIdAsync(long id);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> UpdateSysPermissionAsync(SysPermissionAddOrEditDto input);
        /// <summary>
        /// 通过主键Id获取对象
        /// </summary>
        /// <returns>返回对象结果</returns>
        Task<ApiResult<SysPermissionAddOrEditDto>> GetAsync(long id);


        #region 树形代码接口
        /// <summary>
        /// 获取权限集合，提供给前端树形
        /// </summary>
        /// <returns></returns>
        Task<IApiResult> GetSysPermissionListAsync(PageInputDto input);
        /// <summary>
        /// 过滤自身和自身所有的
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<IApiResult> GetSysPermissionFilterByPidAsync(long pid);
       
        #endregion


    }

}


