using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YC.Common.NetCoreUtils;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Cache;
using YC.Core.DynamicApi;
using YC.Core.DynamicApi.Attributes;
using YC.DapperFrameWork;
using YC.Model.SysDbEntity;

namespace YC.ApplicationService
{
  
    public class DapperBaseApplicationService : BaseApplicationService, IApplicationService
    {
        public IUnitOfWork _unitOfWork;


        public DapperBaseApplicationService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager)
        :base(httpContextAccessor,cacheManager)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
