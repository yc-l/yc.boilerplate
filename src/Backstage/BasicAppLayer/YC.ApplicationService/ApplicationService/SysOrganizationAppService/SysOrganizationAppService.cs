
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


namespace YC.ApplicationService
{
    /// <summary>
    ///  组织机构业务实现接口
    /// </summary>
    public class SysOrganizationAppService : FreeSqlBaseApplicationService, ISysOrganizationAppService
    {
	private readonly IFreeSqlRepository<SysOrganization, Int64> _sysOrganizationFreeSqlRepository; 
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public SysOrganizationAppService(IFreeSqlRepository<SysOrganization,  Int64> sysOrganizationFreeSqlRepository,
        IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager, IMapper mapper) : base(httpContextAccessor, cacheManager)
        {
            _sysOrganizationFreeSqlRepository = sysOrganizationFreeSqlRepository;
              _cacheManager = cacheManager;
           _mapper = mapper;
        }

        
        /// <summary>
        /// 通过Id查找数据对象
        /// </summary>
        /// <returns>返回数据集合</returns>
        
        public async Task<ApiResult<SysOrganizationAddOrEditDto>> GetAsync(long id)
        {
            var res = new ApiResult<SysOrganizationAddOrEditDto>();

            var entity = await _sysOrganizationFreeSqlRepository.Select
            .WhereDynamic(id)
            .ToOneAsync();

            var entityDto = _mapper.Map<SysOrganizationAddOrEditDto>(entity);
            return res.Ok(entityDto);
        }
        
        
        [HttpPost]
        public async Task<IApiResult> GetPageSysOrganizationListAsync(PageInput<PageInputDto> input)
        {
            Expression<Func<SysOrganization, bool>> exp = null;
            if (input.Filter != null)
            {
                //exp = a => a.Name.Contains(input.Filter.QueryString);
            }
            var list = await _sysOrganizationFreeSqlRepository.Select.WhereIf(input.Filter.QueryString.NotNull(), exp)
                .Count(out var total).OrderByDescending(true, a => a.Id).Page(input.CurrentPage, input.PageSize)
                .ToListAsync();

             ///返回数据必须是明确实体，要不然可能存在json映射死循环
            var data = new PageOutput<SysOrganizationAddOrEditDto>()
            {
                List = _mapper.Map<List<SysOrganizationAddOrEditDto>>(list),
                Total = total
            };

           
            return ApiResult.Ok(data);
        }
        
         [HttpPost]
        public async Task<IApiResult> CreateSysOrganizationAsync(SysOrganizationAddOrEditDto input)
        {

            input.Id = "0";//做一个处理，要不然automapper 无法转换
            var entity = _mapper.Map<SysOrganization>(input);
            var obj = await _sysOrganizationFreeSqlRepository.InsertAsync(entity);

            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk();
            }
            return ApiResult.Ok();
        }
        

        public async Task<IApiResult> DeleteSysOrganizationByIdAsync(long id)
        {
            var result = false;
            if (id > 0)
            {
                var list = await _sysOrganizationFreeSqlRepository.Select.OrderBy(x => x.Id).ToListAsync();
                var tempList = _mapper.Map<List<SysOrganizationDto>>(list);
               
               var childrenList=GetChildrenListByPid(id, ref tempList,new List<SysOrganizationDto>()).Select(x=>long.Parse(x.Id));
                List<long> idsList = new List<long>();
                idsList.Add(id);//将自己和自己所有的递归子集都删除
                if (childrenList.Count()> 0) {
                    idsList.AddRange(childrenList);
                }
                result = (await _sysOrganizationFreeSqlRepository.DeleteAsync(x=> idsList.Contains(x.Id)))> 0;
            }

            return ApiResult.Result(result);
        }

        
         public async Task<IApiResult> UpdateSysOrganizationAsync(SysOrganizationAddOrEditDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Id)) {
                return ApiResult.NotOk("组织机构Id 不能为空!");
            }

            long id = Convert.ToInt64(input?.Id);
           
            var obj = await _sysOrganizationFreeSqlRepository.GetAsync(id);
            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk("对象不存在！");
            }

            _mapper.Map(input, obj);
            await _sysOrganizationFreeSqlRepository.UpdateAsync(obj);
          
            return ApiResult.Ok();
        }

       #region 树形代码


        [HttpPost]
        public async Task<IApiResult> GetSysOrganizationListAsync(PageInputDto input)
        {

            Expression<Func<SysOrganization, bool>> exp = null;
            if (input.QueryString != null)
            {
                exp = a => a.Label.Contains(input.QueryString);
            }
            var list = await _sysOrganizationFreeSqlRepository.Select.WhereIf(input.QueryString.NotNull(), exp).OrderBy(x => x.Id).ToListAsync();

            var data = new PageOutput<SysOrganizationDto>()
            {
                List = _mapper.Map<List<SysOrganizationDto>>(list),
            };
          
            return ApiResult.Ok(data);
        }

        [HttpPost]
        public async Task<IApiResult> GetSysOrganizationFilterByPidAsync(long pid)
        {

            var list = await _sysOrganizationFreeSqlRepository.Select.OrderBy(x => x.Id).ToListAsync();
            var tempList = _mapper.Map<List<SysOrganizationDto>>(list);
            FilterListByPid(pid, ref tempList);

            var data = new PageOutput<SysOrganizationDto>()
            {
                List = _mapper.Map<List<SysOrganizationDto>>(tempList),
            };
            //var stringJson = tempList.ToJson();
            return ApiResult.Ok(data);
        }

       
        /// <summary>
        /// 递归过滤指定的id的所以子集
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        private static void FilterListByPid(long id, ref List<SysOrganizationDto> list)
        {

            var pList = list.Where(x => long.Parse(x.ParentId) == id).ToList();

            if (pList.Count > 0)
            {//说明第一个级别有内容
                list = list.Where(x => !pList.Contains(x)).ToList();//先拿到有子节点全部去除，在把plist 查找父节点为plist 里面的内容

                for (int i = 0; i < pList.Count; i++)
                {
                    var isExistChildren = list.Where(x => x.ParentId == pList[i].Id).FirstOrDefault() == null ? false : true;
                    if (isExistChildren)
                    {
                        FilterListByPid(long.Parse(pList[i].Id), ref list);
                    }

                }
            }


        }

        private static List<SysOrganizationDto> GetChildrenListByPid(long id, ref List<SysOrganizationDto> list, List<SysOrganizationDto> childrenList)
        {

            var pList = list.Where(x =>x.ParentId == id.ToString()).ToList();

            if (pList.Count > 0)
            {//说明第一个级别有内容
                childrenList.AddRange(pList);//将子节点集合汇总
                for (int i = 0; i < pList.Count; i++)
                {

                    var isExistChildren = list.Where(x => x.ParentId == pList[i].Id).FirstOrDefault() == null ? false : true;
                    if (isExistChildren)
                    {
                        GetChildrenListByPid(long.Parse(pList[i].Id), ref list, childrenList);
                    }

                }
            }

            return childrenList;
        }


        #endregion
	

        
	
	}
}
