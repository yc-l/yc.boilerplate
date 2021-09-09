
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
    ///  数据字典业务实现接口
    /// </summary>
    public class SysDataDictionaryAppService : FreeSqlBaseApplicationService, ISysDataDictionaryAppService
    {
	private readonly IFreeSqlRepository<SysDataDictionary, Int64> _sysDataDictionaryFreeSqlRepository; 
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public SysDataDictionaryAppService(IFreeSqlRepository<SysDataDictionary,  Int64> sysDataDictionaryFreeSqlRepository,
        IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager, IMapper mapper) : base(httpContextAccessor, cacheManager)
        {
            _sysDataDictionaryFreeSqlRepository = sysDataDictionaryFreeSqlRepository;
              _cacheManager = cacheManager;
           _mapper = mapper;
        }

        
        /// <summary>
        /// 通过Id查找数据对象
        /// </summary>
        /// <returns>返回数据集合</returns>
        
        public async Task<ApiResult<SysDataDictionaryAddOrEditDto>> GetAsync(long id)
        {
            var res = new ApiResult<SysDataDictionaryAddOrEditDto>();

            var entity = await _sysDataDictionaryFreeSqlRepository.Select
            .WhereDynamic(id)
            .ToOneAsync();

            var entityDto = _mapper.Map<SysDataDictionaryAddOrEditDto>(entity);
            return res.Ok(entityDto);
        }
        
        
        [HttpPost]
        public async Task<IApiResult> GetPageSysDataDictionaryListAsync(PageInput<PageInputDto> input)
        {
            Expression<Func<SysDataDictionary, bool>> exp = null;
            if (input.Filter != null)
            {
                exp = a => a.Label.Contains(input.Filter.QueryString)
                 || a.Key.Contains(input.Filter.QueryString);
            }
            var list = await _sysDataDictionaryFreeSqlRepository.Select.WhereIf(input.Filter.QueryString.NotNull(), exp)
                .Count(out var total).OrderByDescending(true, a => a.Id).Page(input.CurrentPage, input.PageSize)
                .ToListAsync();

             ///返回数据必须是明确实体，要不然可能存在json映射死循环
            var data = new PageOutput<SysDataDictionaryAddOrEditDto>()
            {
                List = _mapper.Map<List<SysDataDictionaryAddOrEditDto>>(list),
                Total = total
            };

           
            return ApiResult.Ok(data);
        }
        
         [HttpPost]
        public async Task<IApiResult> CreateSysDataDictionaryAsync(SysDataDictionaryAddOrEditDto input)
        {

            input.Id = "0";//做一个处理，要不然automapper 无法转换
            var entity = _mapper.Map<SysDataDictionary>(input);
           var isExist=await _sysDataDictionaryFreeSqlRepository.Where(x => x.Key==input.Key).AnyAsync();
            if (isExist) {
                return ApiResult.NotOk("指定的Key已经存在!");
            }
            var obj = await _sysDataDictionaryFreeSqlRepository.InsertAsync(entity);

            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk();
            }
            return ApiResult.Ok();
        }
        

        public async Task<IApiResult> DeleteSysDataDictionaryByIdAsync(long id)
        {
            var result = false;
            if (id > 0)
            {
                var list = await _sysDataDictionaryFreeSqlRepository.Select.OrderBy(x => x.Type).ToListAsync();
                var tempList = _mapper.Map<List<SysDataDictionaryDto>>(list);
               
               var childrenList=GetChildrenListByPid(id, ref tempList,new List<SysDataDictionaryDto>()).Select(x=>long.Parse(x.Id));
                List<long> idsList = new List<long>();
                idsList.Add(id);//将自己和自己所有的递归子集都删除
                if (childrenList.Count()> 0) {
                    idsList.AddRange(childrenList);
                }
                result = (await _sysDataDictionaryFreeSqlRepository.DeleteAsync(x=> idsList.Contains(x.Id)))> 0;
            }

            return ApiResult.Result(result);
        }

        
         public async Task<IApiResult> UpdateSysDataDictionaryAsync(SysDataDictionaryAddOrEditDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Id)) {
                return ApiResult.NotOk("数据字典Id 不能为空!");
            }

            long id = Convert.ToInt64(input?.Id);
           
            var obj = await _sysDataDictionaryFreeSqlRepository.GetAsync(id);
            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk("对象不存在！");
            }

            _mapper.Map(input, obj);
            var isExist = await _sysDataDictionaryFreeSqlRepository.Where(x => x.Key.Equals(input.Key)).AnyAsync();
            if (isExist)
            {
                return ApiResult.NotOk("指定的Key已经存在!");
            }
           await _sysDataDictionaryFreeSqlRepository.UpdateAsync(obj);
          
            return ApiResult.Ok();
        }

       #region 树形代码


        [HttpPost]
        public async Task<IApiResult> GetSysDataDictionaryListAsync(PageInputDto input)
        {

            Expression<Func<SysDataDictionary, bool>> exp = null;
            if (input.QueryString != null)
            {
                exp = a => a.Label.Contains(input.QueryString);
            }
            var list = await _sysDataDictionaryFreeSqlRepository.Select.WhereIf(input.QueryString.NotNull(), exp).OrderBy(x => x.Type).ToListAsync();

            var data = new PageOutput<SysDataDictionaryDto>()
            {
                List = _mapper.Map<List<SysDataDictionaryDto>>(list),
            };
          
            return ApiResult.Ok(data);
        }

        [HttpPost]
        public async Task<IApiResult> GetSysDataDictionaryFilterByPidAsync(long pid)
        {

            var list = await _sysDataDictionaryFreeSqlRepository.Select.OrderBy(x => x.Type).ToListAsync();
            var tempList = _mapper.Map<List<SysDataDictionaryDto>>(list);
            FilterListByPid(pid, ref tempList);

            var data = new PageOutput<SysDataDictionaryDto>()
            {
                List = _mapper.Map<List<SysDataDictionaryDto>>(tempList),
            };
            //var stringJson = tempList.ToJson();
            return ApiResult.Ok(data);
        }

       
        /// <summary>
        /// 递归过滤指定的id的所以子集
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        private static void FilterListByPid(long id, ref List<SysDataDictionaryDto> list)
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

        private static List<SysDataDictionaryDto> GetChildrenListByPid(long id, ref List<SysDataDictionaryDto> list, List<SysDataDictionaryDto> childrenList)
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
