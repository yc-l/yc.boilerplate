
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
    ///  租户管理业务接口
    /// </summary>
    public interface ISysTenantAppService : IApplicationService, IDependencyInjectionSupport
    {


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>返回数据集合</returns>
        Task<IApiResult> GetPageSysTenantListAsync(PageInput<PageInputDto> input);
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> CreateSysTenantAsync(SysTenantAddOrEditDto input);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> DeleteSysTenantByIdAsync(long id);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> UpdateSysTenantAsync(SysTenantAddOrEditDto input);
        /// <summary>
        /// 通过主键Id获取对象
        /// </summary>
        /// <returns>返回对象结果</returns>
        Task<ApiResult<SysTenantAddOrEditDto>> GetAsync(long id);

        void TestTranscation();
    }

}


