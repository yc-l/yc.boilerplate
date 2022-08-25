
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
    ///  流程管理业务接口
    /// </summary>
    public interface IProcessFlowAppService : IApplicationService, IDependencyInjectionSupport
    {


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>返回数据集合</returns>
        Task<IApiResult> GetPageProcessFlowListAsync(PageInput<PageInputDto> input);

        /// <summary>
        /// 获取流程列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IApiResult> GetAllProcessFlowListAsync();
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> CreateProcessFlowAsync(ProcessFlowAddOrEditDto input);
        

        
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> DeleteProcessFlowByIdAsync(string id);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> UpdateProcessFlowAsync(ProcessFlowAddOrEditDto input);
        /// <summary>
        /// 通过主键Id获取对象
        /// </summary>
        /// <returns>返回对象结果</returns>
        Task<ApiResult<ProcessFlowAddOrEditDto>> GetAsync(string id);
          

    }

}


