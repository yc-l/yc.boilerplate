
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
    ///  业务接口
    /// </summary>
    public interface ISysAuditLogAppService : IApplicationService, IDependencyInjectionSupport
    {


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>返回数据集合</returns>
        Task<IApiResult> GetPageSysAuditLogListAsync(PageInput<PageInputDto> input);
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>返回执行结果</returns>
       IApiResult CreateSysAuditLog(SysAuditLogAddOrEditDto input);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> DeleteSysAuditLogByIdAsync(long id);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <returns>返回执行结果</returns>
        Task<IApiResult> UpdateSysAuditLogAsync(SysAuditLogAddOrEditDto input);
        /// <summary>
        /// 通过主键Id获取对象
        /// </summary>
        /// <returns>返回对象结果</returns>
        Task<ApiResult<SysAuditLogAddOrEditDto>> GetAsync(long id);


    }

}


