
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
using YC.Core.Domain.Entity;
using YC.Model;

namespace YC.ApplicationService
{
    /// <summary>
    ///  权限业务实现接口
    /// </summary>
    public class SysPermissionAppService : FreeSqlBaseApplicationService, ISysPermissionAppService
    {
        private readonly IFreeSqlRepository<SysPermission, Int64> _sysPermissionFreeSqlRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public SysPermissionAppService(IFreeSqlRepository<SysPermission, Int64> sysPermissionFreeSqlRepository,
        IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager, IMapper mapper) : base(httpContextAccessor, cacheManager)
        {
            _sysPermissionFreeSqlRepository = sysPermissionFreeSqlRepository;
            _cacheManager = cacheManager;
            _mapper = mapper;
        }


        /// <summary>
        /// 通过Id查找数据对象
        /// </summary>
        /// <returns>返回数据集合</returns>

        public async Task<ApiResult<SysPermissionAddOrEditDto>> GetAsync(long id)
        {
            var res = new ApiResult<SysPermissionAddOrEditDto>();

            var entity = await _sysPermissionFreeSqlRepository.Select
            .WhereDynamic(id)
            .ToOneAsync();

            var entityDto = _mapper.Map<SysPermissionAddOrEditDto>(entity);
            return res.Ok(entityDto);
        }


        [HttpPost]
        public async Task<IApiResult> GetPageSysPermissionListAsync(PageInput<PageInputDto> input)
        {
            Expression<Func<SysPermission, bool>> exp = null;
            if (input.Filter != null)
            {
                exp = a => a.Label.Contains(input.Filter.QueryString);
            }
            var list = await _sysPermissionFreeSqlRepository.Select.Where(exp)
                .Count(out var total).OrderByDescending(true, a => a.Id).Page(input.CurrentPage, input.PageSize)
                .ToListAsync();

            var data = new PageOutput<SysPermissionAddOrEditDto>()
            {
                List = _mapper.Map<List<SysPermissionAddOrEditDto>>(list),
                Total = total
            };

            return ApiResult.Ok(data);
        }


        [HttpPost]
        public async Task<IApiResult> CreateSysPermissionAsync(SysPermissionAddOrEditDto input)
        {

            input.Id = "0";//做一个处理，要不然automapper 无法转换
            var entity = _mapper.Map<SysPermission>(input);
            var obj = await _sysPermissionFreeSqlRepository.InsertAsync(entity);

            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk();
            }
            return ApiResult.Ok();
        }


        public async Task<IApiResult> DeleteSysPermissionByIdAsync(long id)
        {
            var result = false;
            if (id > 0)
            {
                var list = await _sysPermissionFreeSqlRepository.Select.OrderBy(x => x.Type).ToListAsync();
                var tempList = _mapper.Map<List<SysPermissionDto>>(list);
               
               var childrenList=GetChildrenListByPid(id, ref tempList,new List<SysPermissionDto>()).Select(x=>long.Parse(x.Id));
                List<long> idsList = new List<long>();
                idsList.Add(id);//将自己和自己所有的递归子集都删除
                if (childrenList.Count()> 0) {
                    idsList.AddRange(childrenList);
                }
                result = (await _sysPermissionFreeSqlRepository.DeleteAsync(x=> idsList.Contains(x.Id)))> 0;
            }

            return ApiResult.Result(result);
        }


        public async Task<IApiResult> UpdateSysPermissionAsync(SysPermissionAddOrEditDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Id))
            {
                return ApiResult.NotOk();
            }
            long id = Convert.ToInt64(input?.Id);
          
            var obj = await _sysPermissionFreeSqlRepository.GetAsync(id);
            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk("对象不存在！");
            }

            _mapper.Map(input, obj);
            await _sysPermissionFreeSqlRepository.UpdateAsync(obj);

            return ApiResult.Ok();
        }

        #region 树形代码


        [HttpPost]
        public async Task<IApiResult> GetSysPermissionListAsync(PageInputDto input)
        {

            Expression<Func<SysPermission, bool>> exp = null;
            if (input.QueryString != null)
            {
                exp = a => a.Label.Contains(input.QueryString);
            }
            var list = await _sysPermissionFreeSqlRepository.Select.WhereIf(input.QueryString.NotNull(), exp).OrderBy(x => x.Type).ToListAsync();
            //var tempList = GetTree(0, _mapper.Map<List<SysPermissionDto>>(list));

            var data = new PageOutput<SysPermissionDto>()
            {
                List = _mapper.Map<List<SysPermissionDto>>(list),
            };
            //var stringJson = tempList.ToJson();
            return ApiResult.Ok(data);
        }

        [HttpPost]
        public async Task<IApiResult> GetSysPermissionFilterByPidAsync(long pid)
        {

            var list = await _sysPermissionFreeSqlRepository.Select.OrderBy(x => x.Type).ToListAsync();
            var tempList = _mapper.Map<List<SysPermissionDto>>(list);
            FilterListByPid(pid, ref tempList);

            var data = new PageOutput<SysPermissionDto>()
            {
                List = _mapper.Map<List<SysPermissionDto>>(tempList),
            };
            //var stringJson = tempList.ToJson();
            return ApiResult.Ok(data);
        }

        //private static List<SysPermissionDto> GetTree(long id, List<SysPermissionDto> list)
        //{

        //    var pList = list.Where(x => long.Parse(x.ParentId) == id).ToList();
        //    List<SysPermissionDto> trees = new List<SysPermissionDto>();//这一步关键

        //    if (pList.Count > 0)
        //    {//说明第一个级别有内容

        //        for (int i = 0; i < pList.Count; i++)
        //        {
        //            var temp = new SysPermissionDto();
        //            temp = pList[i];
        //            temp.Children = GetTree(long.Parse(pList[i].Id), list);
        //            temp.HasChildren = temp.Children.Count > 0 ? true : false;
        //            trees.Add(temp);
        //        }
        //    }

        //    return trees;

        //}

        /// <summary>
        /// 递归过滤指定的id的所以子集
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        private static void FilterListByPid(long id, ref List<SysPermissionDto> list)
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

        private static List<SysPermissionDto> GetChildrenListByPid(long id, ref List<SysPermissionDto> list, List<SysPermissionDto> childrenList)
        {

            var pList = list.Where(x => long.Parse(x.ParentId) == id).ToList();

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
