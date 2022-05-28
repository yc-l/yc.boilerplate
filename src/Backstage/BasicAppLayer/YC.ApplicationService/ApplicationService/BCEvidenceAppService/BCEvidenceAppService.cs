
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
using YC.Model.BlockChainEntity;
using FISCOBCOS.CSharpSdk;


namespace YC.ApplicationService
{
    /// <summary>
    ///  区块链存证业务实现接口
    /// </summary>
    public class BCEvidenceAppService : FreeSqlBaseApplicationService, IBCEvidenceAppService
    {
	private readonly IFreeSqlRepository<BCEvidence, Int64> _bCEvidenceFreeSqlRepository; 
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public BCEvidenceAppService(IFreeSqlRepository<BCEvidence,  Int64> bCEvidenceFreeSqlRepository,
        IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager, IMapper mapper) : base(httpContextAccessor, cacheManager)
        {
            _bCEvidenceFreeSqlRepository = bCEvidenceFreeSqlRepository;
              _cacheManager = cacheManager;
           _mapper = mapper;
        }

        
        /// <summary>
        /// 通过Id查找数据对象
        /// </summary>
        /// <returns>返回数据集合</returns>
        
        public async Task<ApiResult<BCEvidenceAddOrEditDto>> GetAsync(long id)
        {
            var res = new ApiResult<BCEvidenceAddOrEditDto>();

            var entity = await _bCEvidenceFreeSqlRepository.Select
            .WhereDynamic(id)
            .ToOneAsync();

            var entityDto = _mapper.Map<BCEvidenceAddOrEditDto>(entity);
            return res.Ok(entityDto);
        }
        
        
        [HttpPost]
        public async Task<IApiResult> GetPageBCEvidenceListAsync(PageInput<PageInputDto> input)
        {
            Expression<Func<BCEvidence, bool>> exp = null;
            if (input.Filter != null)
            {
                exp = a => a.ServiceId.Contains(input.Filter.QueryString);
            }
            var list = await _bCEvidenceFreeSqlRepository.Select.WhereIf(input.Filter.QueryString.NotNull(), exp)
                .Count(out var total).OrderByDescending(true, a => a.OperaEvidenceDate).Page(input.CurrentPage, input.PageSize)
                .ToListAsync();

             ///返回数据必须是明确实体，要不然可能存在json映射死循环
            var data = new PageOutput<BCEvidenceAddOrEditDto>()
            {
                List = _mapper.Map<List<BCEvidenceAddOrEditDto>>(list),
                Total = total
            };
           
            TraceEvidenceContractService traceEvidenceContractService = new TraceEvidenceContractService();
            return ApiResult.Ok(data);
        }

        [HttpPost]
        public async Task<IApiResult> ValidateBCEvidenceListAsync(string serviceId)
        {
            
            TraceEvidenceContractService traceEvidenceContractService = new TraceEvidenceContractService();
           var result=await traceEvidenceContractService.CallGetServiceListAsync(serviceId);

            return ApiResult.Ok(result.ListKeyValueDtos);
        }


        [HttpPost]
        public async Task<IApiResult> CreateBCEvidenceAsync(BCEvidenceCreateDto input)
        {
            TraceEvidenceContractService traceEvidenceContractService = new TraceEvidenceContractService();
            input.DataValue = input.DataValue + $",\"业务上链用户id\"：{this.LoginUser.Id}";
            var registerEvidenceData=new RegisterTraceEvidence() {
              ServiceId = input.ServiceId,
              TypeName = input.TypeName,
              DataValue = input.DataValue
            };

            var callResult = await traceEvidenceContractService.CallGetServiceListAsync(input.ServiceId);
            if (callResult.ListKeyValueDtos != null) {
               var count= callResult.ListKeyValueDtos.Where(x => x.Key == input.TypeName && x.Value.Contains(input.DataValue)).Count();
                if (count > 0) {
                    return ApiResult.NotOk("存证数据已存在！不能重复上链！");
                }
            }
            var result = await traceEvidenceContractService.PushEvidenceGeTransHashAsync(registerEvidenceData);
            if (result == null) {
                return ApiResult.NotOk("上链超时！");
            }

            input.Id = "0";//做一个处理，要不然automapper 无法转换
            var entity = _mapper.Map<BCEvidence>(input);
            entity.TranscationHash = result;
            entity.OperaEvidenceDate = DateTime.Now;
            entity.EvidenceState = 1;
            var obj = await _bCEvidenceFreeSqlRepository.InsertAsync(entity);

            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk($"上链成功,存储数据库失败！");
            }
            return ApiResult.Ok($"上链成功!交易hash：{entity.TranscationHash}");

        }
        
        
        public async Task<IApiResult> DeleteBCEvidenceByIdAsync(long id)
        {
            var result = false;
            if (id > 0)
            {
                result = (await _bCEvidenceFreeSqlRepository.DeleteAsync(m => m.Id == id)) > 0;
            }

            return ApiResult.Result(result);
        }
        
        
         public async Task<IApiResult> UpdateBCEvidenceAsync(BCEvidenceAddOrEditDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Id)) {
                return ApiResult.NotOk("区块链存证Id 不能为空!");
            }
            long id = Convert.ToInt64(input?.Id);

            var obj = await _bCEvidenceFreeSqlRepository.GetAsync(id);
            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk("对象不存在！");
            }

            _mapper.Map(input, obj);
            await _bCEvidenceFreeSqlRepository.UpdateAsync(obj);
          
            return ApiResult.Ok();
        }
	
	
	}
}
