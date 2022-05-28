
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
    ///  区块链存证业务接口
    /// </summary>
    public interface IBCEvidenceAppService : IApplicationService, IDependencyInjectionSupport
    {


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>返回数据集合</returns>
        Task<IApiResult> GetPageBCEvidenceListAsync(PageInput<PageInputDto> input);
        /// <summary>
        /// 区块链上链
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> CreateBCEvidenceAsync(BCEvidenceCreateDto input);
        /// <summary>
        /// 区块链核验
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        Task<IApiResult> ValidateBCEvidenceListAsync(string serviceId);
       
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> DeleteBCEvidenceByIdAsync(long id);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> UpdateBCEvidenceAsync(BCEvidenceAddOrEditDto input);
        /// <summary>
        /// 通过主键Id获取对象
        /// </summary>
        /// <returns>返回对象结果</returns>
        Task<ApiResult<BCEvidenceAddOrEditDto>> GetAsync(long id);


    }

}


