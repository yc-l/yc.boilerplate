
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using YC.Model;
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


namespace YC.ApplicationService
{
    /// <summary>
    ///  流程管理业务实现接口
    /// </summary>
    public class ProcessFlowAppService : FreeSqlBaseApplicationService, IProcessFlowAppService
    {
	private readonly IFreeSqlRepository<ProcessFlow, String> _processFlowFreeSqlRepository; 
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public ProcessFlowAppService(IFreeSqlRepository<ProcessFlow,  String> processFlowFreeSqlRepository,
        IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager, IMapper mapper) : base(httpContextAccessor, cacheManager)
        {
            _processFlowFreeSqlRepository = processFlowFreeSqlRepository;
              _cacheManager = cacheManager;
           _mapper = mapper;
        }

        
        /// <summary>
        /// 通过Id查找数据对象
        /// </summary>
        /// <returns>返回数据集合</returns>
        
        public async Task<ApiResult<ProcessFlowAddOrEditDto>> GetAsync(String id)
        {
            var res = new ApiResult<ProcessFlowAddOrEditDto>();

            var entity = await _processFlowFreeSqlRepository.Select
            .WhereDynamic(id)
            .ToOneAsync();

           var temp= entity.FlowContent.ToJObject();
            
            var entityDto = _mapper.Map<ProcessFlowAddOrEditDto>(entity);
            return res.Ok(entityDto);
        }
        
        
        [HttpPost]
        public async Task<IApiResult> GetPageProcessFlowListAsync(PageInput<PageInputDto> input)
        {
            Expression<Func<ProcessFlow, bool>> exp = null;
            if (input.Filter != null)
            {
                //exp = a => a.Name.Contains(input.Filter.QueryString);
            }
            var list = await _processFlowFreeSqlRepository.Select.WhereIf(input.Filter.QueryString.NotNull(), exp)
                .Count(out var total).OrderByDescending(true, a => a.Id).Page(input.CurrentPage, input.PageSize)
                .ToListAsync();

             ///返回数据必须是明确实体，要不然可能存在json映射死循环
            var data = new PageOutput<ProcessFlowAddOrEditDto>()
            {
                List = _mapper.Map<List<ProcessFlowAddOrEditDto>>(list),
                Total = total
            };

           
            return ApiResult.Ok(data);
        }

        
        public async Task<IApiResult> GetAllProcessFlowListAsync()
        {
            Expression<Func<ProcessFlow, bool>> exp = null;
           
            var list = await _processFlowFreeSqlRepository.Select
                .Count(out var total).OrderByDescending(true, a => a.Id)
                .ToListAsync();

            ///返回数据必须是明确实体，要不然可能存在json映射死循环
            var data = new PageOutput<ProcessFlowAddOrEditDto>()
            {
                List = _mapper.Map<List<ProcessFlowAddOrEditDto>>(list),
                Total = total
            };


            return ApiResult.Ok(data);
        }


        [HttpPost]
        public async Task<IApiResult> CreateProcessFlowAsync(ProcessFlowAddOrEditDto input)
        {

            input.Id = ObjectId.Get();//做一个处理，要不然automapper 无法转换
            var entity = _mapper.Map<ProcessFlow>(input);
            var obj = await _processFlowFreeSqlRepository.InsertAsync(entity);

            if (obj==null)
            {
                return ApiResult.NotOk();
            }
            return ApiResult.Ok();
        }

     
        public async Task<IApiResult> DeleteProcessFlowByIdAsync(string id)
        {
            var result = false;
            if (!string.IsNullOrWhiteSpace(id))
            {
                result = (await _processFlowFreeSqlRepository.DeleteAsync(m => m.Id == id)) > 0;
            }

            return ApiResult.Result(result);
        }

      
        public async Task<IApiResult> UpdateProcessFlowAsync(ProcessFlowAddOrEditDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Id)) {
                return ApiResult.NotOk("流程管理Id 不能为空!");
            }
            
            var obj = await _processFlowFreeSqlRepository.GetAsync(input.Id);
            if (obj==null)
            {
                return ApiResult.NotOk("对象不存在！");
            }

            _mapper.Map(input, obj);
            await _processFlowFreeSqlRepository.UpdateAsync(obj);
          
            return ApiResult.Ok();
        }
        
	
	}
}
