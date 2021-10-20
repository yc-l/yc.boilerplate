using AutoMapper;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YC.ApplicationService.Dto;
using YC.Common.NetCoreUtils;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Cache;
using YC.Core.Domain;
using YC.Core.DynamicApi;
using YC.Core.DynamicApi.Attributes;
using YC.DapperFrameWork;
using YC.FreeSqlFrameWork;
using YC.Model.SysDbEntity;

namespace YC.ApplicationService
{
    
    public class FreeSqlBaseApplicationService : BaseApplicationService, IApplicationService
    {

        private readonly IMapper _mapper;
        public FreeSqlBaseApplicationService(IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager)
         : base(httpContextAccessor, cacheManager)
        {

        }

      
     
    }
}
