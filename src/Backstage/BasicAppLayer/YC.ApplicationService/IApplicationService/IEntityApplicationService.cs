using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService.Dto;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Domain;
using YC.Core.DynamicApi;
using YC.Core.DynamicApi.Attributes;

namespace YC.ApplicationService
{
   
    public interface IEntityApplicationService<T,TKey> : IApplicationService where T:class,new()
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>返回数据集合</returns>
         Task<IApiResult> GetPageEntityListAsync(PageInput<PageInputDto>  input= default);
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns>返回执行结果</returns>
         Task<IApiResult> CreateAsync(T input);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <returns>返回执行结果</returns>
         Task<IApiResult> DeleteByIdAsync(TKey id);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <returns>返回执行结果</returns>
         Task<IApiResult> UpdateEntityAsync(T input);
        /// <summary>
        /// 通过主键Id获取对象
        /// </summary>
        /// <returns>返回对象结果</returns>
         Task<IApiResult<T>> GetAsync(TKey id);


    }
}
