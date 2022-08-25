
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
using Newtonsoft.Json.Linq;

namespace YC.ApplicationService
{
    /// <summary>
    ///  区块链存证业务实现接口
    /// </summary>
    public class BCEvidenceAppService : FreeSqlBaseApplicationService, IBCEvidenceAppService
    {
	   private readonly IFreeSqlRepository<BCEvidence, Int64> _bCEvidenceFreeSqlRepository;
        private readonly IFreeSqlRepository<ProcessFlow, String> _processFlowFreeSqlRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public BCEvidenceAppService(IFreeSqlRepository<BCEvidence,  Int64> bCEvidenceFreeSqlRepository,
            IFreeSqlRepository<ProcessFlow, String> processFlowFreeSqlRepository,
        IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager, IMapper mapper) : base(httpContextAccessor, cacheManager)
        {
            _bCEvidenceFreeSqlRepository = bCEvidenceFreeSqlRepository;
              _cacheManager = cacheManager;
           _mapper = mapper;
            _processFlowFreeSqlRepository=processFlowFreeSqlRepository;
    }

        
        /// <summary>
        /// 通过Id查找数据对象
        /// </summary>
        /// <returns>返回数据集合</returns>
        
        public async Task<ApiResult<BCEvidenceAddOrEditDto>> GetAsync(long id)
        {
            var res = new ApiResult<BCEvidenceAddOrEditDto>();
            //1.获取对应的存证数据
            var entity = await _bCEvidenceFreeSqlRepository.Select
            .WhereDynamic(id)
            .ToOneAsync();
            ///获取全局交易
            var serviceList =await _bCEvidenceFreeSqlRepository.Select.Where(x => x.ServiceId == entity.ServiceId&&x.EvidenceState==1).ToListAsync();
            //2.获取指定匹配的流程数据
            var flow=await _processFlowFreeSqlRepository.Where(x=>x.Id== entity.BusinessFlowId).FirstAsync();

            TraceEvidenceContractService traceEvidenceContractService = new TraceEvidenceContractService();
            //3. 获取区块链的业务数据
            var callResult = await traceEvidenceContractService.CallGetServiceListAsync(entity.ServiceId);
            if (callResult.EviDataDtos != null)
            {
                flow.FlowContent= DetailFlowContent(flow.FlowContent, entity.BehaviorTypeId, callResult.EviDataDtos);
                for (int i = 0; i < callResult.EviDataDtos.Count; i++) {
                   var serviceObj= serviceList.Where(x => x.BehaviorTypeId == callResult.EviDataDtos[i].BehaviorTypeId).FirstOrDefault();
                    callResult.EviDataDtos[i].TransHash = serviceObj.TranscationHash;
                }
            }

            var entityDto = _mapper.Map<BCEvidenceAddOrEditDto>(entity);
            entityDto.FlowContent = flow.FlowContent;
            entityDto.EviDataList = callResult.EviDataDtos;
            return res.Ok(entityDto);
        }

        /// <summary>
        /// 将指定流程渲染为实时状态模式
        /// </summary>
        /// <param name="flowContent"></param>
        /// <param name="eviDataDtos"></param>
        /// <returns></returns>
        public string DetailFlowContent(string flowContent, string localBehaviorId, List<EviDataDto> eviDataDtos) {

            var dataJOjbect = flowContent.ToJObject();
            var properties = dataJOjbect.Properties();
            //获取nodes数组
            var nodes = (JArray)dataJOjbect["nodes"];
            for (int i = 0; i < nodes.Count; i++)
            {
                JObject node = nodes[i] as JObject;
                var temp = node.Property("id").Value.ToString();
               var eviData= eviDataDtos.Where(x => x.BehaviorTypeId == temp).FirstOrDefault();
                if (eviData!=null)
                {
                    string tempName = "";
                    var attrs = (JObject)(node.Property("attrs").Value);
                   
                    if ((attrs).Property("text") != null)
                    {
                        JObject text = (JObject)(attrs).Property("text").Value;
                        tempName = text.Property("text").Value.ToString();
                    }
                    //对已经完成存证内容，进行标记处理
                    attrs.Remove("body");
                    attrs.Remove("label");
                    attrs.Add("body", JToken.FromObject(new
                    {
                        stroke = "#000",
                        fill = "#00e6ac"
                    }));
                    string fillColor = "#33ffff";
                    if (eviData.BehaviorTypeId == localBehaviorId)
                    {
                        tempName = " ▼ " + tempName;
                        fillColor = "#ffd966";
                    }

                    attrs.Add("label", JToken.FromObject(new
                    {
                        text = tempName,
                        fill = fillColor
                    }));

                }

            }
            flowContent = dataJOjbect.ToIndentedJson();
            //将处理好的json 重新返回
            return flowContent;
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
            return ApiResult.Ok(result.EviDataDtos);
        }


        [HttpPost]
        public async Task<IApiResult> CreateBCEvidenceAsync(BCEvidenceCreateDto input)
        {
            TraceEvidenceContractService traceEvidenceContractService = new TraceEvidenceContractService();
            input.DataValue = input.DataValue + $",\"业务上链用户id\"：{this.LoginUser.Id}";
            var registerEvidenceData=new RegisterTraceEvidence() {
              ServiceId = input.ServiceId,
              TypeName = input.TypeName,
              DataValue = input.DataValue,
              BusinessFlowId=input.BusinessFlowId,
              BehaviorTypeId=input.BehaviorTypeId
            };

            var callResult = await traceEvidenceContractService.CallGetServiceListAsync(input.ServiceId);
            if (callResult.EviDataDtos != null) {
               var count= callResult.EviDataDtos.Where(x => x.BusinessFlowId == input.BusinessFlowId
               //&& x.DataValue.Contains(input.DataValue)
               && x.BehaviorTypeId==input.BehaviorTypeId).Count();
                if (count > 0) {
                    return ApiResult.NotOk("存证行为已存在！不能重复上链！");
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
            var state= await traceEvidenceContractService.GetTransStateByTxHash(entity.TranscationHash);

            entity.EvidenceState = state==true?1:0;
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
