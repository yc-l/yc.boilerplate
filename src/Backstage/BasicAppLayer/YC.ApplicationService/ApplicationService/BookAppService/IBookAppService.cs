﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using YC.ApplicationService.ApplicationService.BookAppService.Dto;
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
    public interface IBookAppService : IApplicationService, IDependencyInjectionSupport
    {


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns>返回数据集合</returns>
        Task<IApiResult> GetPageBookListAsync(BookPageInput<PageInputDto> input);



    }

}

