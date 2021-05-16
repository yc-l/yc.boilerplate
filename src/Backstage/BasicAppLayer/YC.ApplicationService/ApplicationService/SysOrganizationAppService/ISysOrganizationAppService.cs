
using System.Collections.Generic;
using System.Threading.Tasks;
using YC.ApplicationService.Dto;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Domain;
using YC.Core.DynamicApi;
using YC.Model;

namespace YC.ApplicationService
{

    /// <summary>
    ///  组织机构业务接口
    /// </summary>
    public interface ISysOrganizationAppService : IApplicationService, IDependencyInjectionSupport
    {


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>返回数据集合</returns>
        Task<IApiResult> GetPageSysOrganizationListAsync(PageInput<PageInputDto> input);
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> CreateSysOrganizationAsync(SysOrganizationAddOrEditDto input);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> DeleteSysOrganizationByIdAsync(long id);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> UpdateSysOrganizationAsync(SysOrganizationAddOrEditDto input);
        /// <summary>
        /// 通过主键Id获取对象
        /// </summary>
        /// <returns>返回对象结果</returns>
        Task<ApiResult<SysOrganizationAddOrEditDto>> GetAsync(long id);


         /// <summary>
        /// 获取列表集合，提供给前端树形
        /// </summary>
        /// <returns></returns>
        Task<IApiResult> GetSysOrganizationListAsync(PageInputDto input);

        /// <summary>
        /// 过滤自身和自身所有的
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<IApiResult> GetSysOrganizationFilterByPidAsync(long pid);

       
    }

}


