
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
    ///  数据字典业务接口
    /// </summary>
    public interface ISysDataDictionaryAppService : IApplicationService, IDependencyInjectionSupport
    {


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>返回数据集合</returns>
        Task<IApiResult> GetPageSysDataDictionaryListAsync(PageInput<PageInputDto> input);
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> CreateSysDataDictionaryAsync(SysDataDictionaryAddOrEditDto input);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> DeleteSysDataDictionaryByIdAsync(long id);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> UpdateSysDataDictionaryAsync(SysDataDictionaryAddOrEditDto input);
        /// <summary>
        /// 通过主键Id获取对象
        /// </summary>
        /// <returns>返回对象结果</returns>
        Task<ApiResult<SysDataDictionaryAddOrEditDto>> GetAsync(long id);


         /// <summary>
        /// 获取列表集合，提供给前端树形
        /// </summary>
        /// <returns></returns>
        Task<IApiResult> GetSysDataDictionaryListAsync(PageInputDto input);

        /// <summary>
        /// 过滤自身和自身所有的
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<IApiResult> GetSysDataDictionaryFilterByPidAsync(long pid);

       
    }

}


