
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using YC.Core;
using AutoMapper;
using System.Linq.Expressions;
using YC.Core.Attribute;
using YC.Core.Domain;
using YC.Core.Autofac;
using YC.Common.ShareUtils;
using YC.Core.Cache;
using YC.ApplicationService.DefaultConfigure;
using YC.FreeSqlFrameWork;
using YC.Core.DynamicApi;
using YC.Core.DynamicApi.Attributes;
using YC.Model.SysDbEntity;
using YC.ApplicationService.Dto;
using YC.Core.Domain.Output;
using YC.ElasticSearch;
using YC.ElasticSearch.Models;

namespace YC.ApplicationService
{
    /// <summary>
    ///  业务实现接口
    /// </summary>
    public class SysAuditLogAppService : FreeSqlBaseApplicationService, ISysAuditLogAppService
    {
        private readonly IFreeSqlRepository<SysAuditLog, Int64> _sysAuditLogFreeSqlRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public SysAuditLogAppService(IFreeSqlRepository<SysAuditLog, Int64> sysAuditLogFreeSqlRepository,
        IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager, IMapper mapper) : base(httpContextAccessor, cacheManager)
        {
            _sysAuditLogFreeSqlRepository = sysAuditLogFreeSqlRepository;
            _cacheManager = cacheManager;
            _mapper = mapper;
           
        }


        /// <summary>
        /// 通过Id查找数据对象
        /// </summary>
        /// <returns>返回数据集合</returns>

        public async Task<ApiResult<SysAuditLogAddOrEditDto>> GetAsync(long id)
        {
            var res = new ApiResult<SysAuditLogAddOrEditDto>();

            var entity = await _sysAuditLogFreeSqlRepository.Select
            .WhereDynamic(id)
            .ToOneAsync();
            var entityDto = _mapper.Map<SysAuditLogAddOrEditDto>(entity);
            return res.Ok(entityDto);
        }


        /// <summary>
        /// 获取日志分页数据
        /// </summary>
        /// <param name="input">分页参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IApiResult> GetPageSysAuditLogListAsync(PageInput<PageInputDto> input)
        {
            Expression<Func<SysAuditLog, bool>> exp = null;
            if (input.Filter != null)
            {
                exp = a => a.UserAccount.Contains(input.Filter.QueryString)
                || a.RequestApi.Contains(input.Filter.QueryString)
                || a.IP.Contains(input.Filter.QueryString)
                || a.Browser.Contains(input.Filter.QueryString)
                || a.Device.Contains(input.Filter.QueryString)
                || a.Os.Contains(input.Filter.QueryString);
            }
            var list = await _sysAuditLogFreeSqlRepository.Select.WhereIf(input.Filter.QueryString.NotNull(), exp)
                .Count(out var total).OrderByDescending(true, a => a.CreationTime).Page(input.CurrentPage, input.PageSize)
                .ToListAsync();

            ///返回数据必须是明确实体，要不然可能存在json映射死循环
            var data = new PageOutput<SysAuditLogAddOrEditDto>()
            {
                List = _mapper.Map<List<SysAuditLogAddOrEditDto>>(list),
                Total = total
            };


            return ApiResult.Ok(data);
        }

        [HttpPost]
        public IApiResult CreateSysAuditLog(SysAuditLogAddOrEditDto input)
        {

            input.Id = "0";//做一个处理，要不然automapper 无法转换
            var entity = _mapper.Map<SysAuditLog>(input);
            var obj = _sysAuditLogFreeSqlRepository.Insert(entity);

            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk();
            }
            return ApiResult.Ok();
        }


        public async Task<IApiResult> DeleteSysAuditLogByIdAsync(long id)
        {
            var result = false;
            if (id > 0)
            {
                result = (await _sysAuditLogFreeSqlRepository.DeleteAsync(m => m.Id == id)) > 0;
            }

            return ApiResult.Result(result);
        }


        public async Task<IApiResult> UpdateSysAuditLogAsync(SysAuditLogAddOrEditDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Id))
            {
                return ApiResult.NotOk("Id 不能为空!");
            }
            long id = Convert.ToInt64(input?.Id);

            var obj = await _sysAuditLogFreeSqlRepository.GetAsync(id);
            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk("对象不存在！");
            }

            _mapper.Map(input, obj);
            await _sysAuditLogFreeSqlRepository.UpdateAsync(obj);

            return ApiResult.Ok();
        }

    }
}
