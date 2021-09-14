
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
using YC.ApplicationService.ApplicationService.SysRoleAppService.Dto;

namespace YC.ApplicationService
{
    /// <summary>
    ///  角色业务实现接口
    /// </summary>
    public class SysRoleAppService : FreeSqlBaseApplicationService, ISysRoleAppService
    {
        private readonly IFreeSqlRepository<SysRole, Int64> _sysRoleFreeSqlRepository;
        private readonly IFreeSqlRepository<SysRolePermission, Int64> _sysRolePermissionFreeSqlRepository;
        private readonly IFreeSqlRepository<SysPermission, Int64> _sysPermissionFreeSqlRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public SysRoleAppService(IFreeSqlRepository<SysRole, Int64> sysRoleFreeSqlRepository,
             IFreeSqlRepository<SysRolePermission, Int64> sysRolePermissionFreeSqlRepository,
             IFreeSqlRepository<SysPermission, Int64> sysPermissionFreeSqlRepository,
        IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager, IMapper mapper) : base(httpContextAccessor, cacheManager)
        {
            _sysRoleFreeSqlRepository = sysRoleFreeSqlRepository;
            _sysRolePermissionFreeSqlRepository = sysRolePermissionFreeSqlRepository;
            _cacheManager = cacheManager;
            _mapper = mapper;
            _sysPermissionFreeSqlRepository = sysPermissionFreeSqlRepository;
        }


        /// <summary>
        /// 通过Id查找数据对象
        /// </summary>
        /// <returns>返回数据集合</returns>

        public async Task<ApiResult<SysRoleAddOrEditDto>> GetAsync(long id)
        {
            var res = new ApiResult<SysRoleAddOrEditDto>();

            var entity = await _sysRoleFreeSqlRepository.Select
            .WhereDynamic(id)
            .ToOneAsync();

            var entityDto = _mapper.Map<SysRoleAddOrEditDto>(entity);
            return res.Ok(entityDto);
        }



        [HttpPost]
        public async Task<IApiResult> GetPageSysRoleListAsync(PageInput<PageInputDto> input)
        {
            Expression<Func<SysRole, bool>> exp = null;
            if (input.Filter != null)
            {
                exp = a => a.Name.Contains(input.Filter.QueryString);
            }
            var list = await _sysRoleFreeSqlRepository.Select.Where(exp)
                .Count(out var total).OrderByDescending(true, a => a.Id).Page(input.CurrentPage, input.PageSize)
                .ToListAsync();

            var data = new PageOutput<SysRoleAddOrEditDto>()
            {
                List = _mapper.Map<List<SysRoleAddOrEditDto>>(list),
                Total = total
            };

            return ApiResult.Ok(data);
        }

        public async Task<IApiResult> GetSysRoleListAsync()
        {
            List<SysRole> list = new List<SysRole>();

            list = await _sysRoleFreeSqlRepository.Select.OrderBy(x => x.Id).ToListAsync();

            var rolePermissionList = await _sysRolePermissionFreeSqlRepository.Select.ToListAsync();

            List<SysRoleAddOrEditDto> tempList = new List<SysRoleAddOrEditDto>();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].PermissionIds = rolePermissionList.Where(t => t.RoleId == list[i].Id).Select(t => t.PermissionId).ToList();
            }

            var data = new PageOutput<SysRoleAddOrEditDto>()
            {
                List = _mapper.Map<List<SysRoleAddOrEditDto>>(list),
            };
            //var stringJson = tempList.ToJson();
            return ApiResult.Ok(data);

        }



        [HttpPost]
        public async Task<IApiResult> CreateSysRoleAsync(SysRoleAddOrEditDto input)
        {

            input.Id = "0";//做一个处理，要不然automapper 无法转换
            var entity = _mapper.Map<SysRole>(input);
            var obj = await _sysRoleFreeSqlRepository.InsertAsync(entity);

            if (!(obj?.Id > 0))
            { 
                return ApiResult.NotOk();
            }
            return ApiResult.Ok();
        }


        public async Task<IApiResult> DeleteSysRoleByIdAsync(long id)
        {
            var result = false;
            if (id > 0)
            {
                result = (await _sysRoleFreeSqlRepository.DeleteAsync(m => m.Id == id)) > 0;
            }

            return ApiResult.Result(result);
        }


        public async Task<IApiResult> UpdateSysRoleAsync(SysRoleAddOrEditDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Id))
            {
                return ApiResult.NotOk();
            }
            long id = Convert.ToInt64(input?.Id);
            if (!(id > 0))
            {
                return ApiResult.NotOk();
            }

            var obj = await _sysRoleFreeSqlRepository.GetAsync(id);
            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk("对象不存在！");
            }

            _mapper.Map(input, obj);
            await _sysRoleFreeSqlRepository.UpdateAsync(obj);

            return ApiResult.Ok();
        }

        public async Task<IApiResult> UpdateSysRolePermissionsAsync(UpdateSysRolePermissionsDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Id))
            {
                return ApiResult.NotOk("角色Id不能为空！");
            }
            long id = Convert.ToInt64(input?.Id);

            //if (input.PermissionIds.Length == 0)
            //{
            //    return ApiResult.NotOk("权限集合不能为空！");
            //}
            var obj = await _sysRoleFreeSqlRepository.GetAsync(id);
            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk("对象不存在！");
            }

            //删除再次添加
            using (var uow
               = _sysRolePermissionFreeSqlRepository.Orm.CreateUnitOfWork())
            {
                _sysRolePermissionFreeSqlRepository.UnitOfWork = uow;
                //uow.Orm.Delete<SysRolePermission>().Where(x => x.RoleId == id).ExecuteAffrows();
                var deleteIds = await _sysRolePermissionFreeSqlRepository.DeleteAsync(x => x.RoleId == obj.Id);
                List<SysRolePermission> sysRolePermissionList = new List<SysRolePermission>();

                var permissionsAllList = await _sysPermissionFreeSqlRepository.Select.ToListAsync();//获取所有权限列表集合

                List<SysPermission> parentPermissionList = new List<SysPermission>();//最后得到父节点集合
                var tempInputPermissionIds = _mapper.Map<List<long>>(input.PermissionIds);
                List<long> childrenParentIds = permissionsAllList.Where(x => tempInputPermissionIds.Contains(x.Id)).Select(x => x.ParentId).ToList();//获取子节点对应的父节点集合

                GetParentListById(childrenParentIds, permissionsAllList, ref parentPermissionList);//得到父节点的集合

                parentPermissionList.ForEach(x =>
                {//过滤添加

                    if (!tempInputPermissionIds.Exists(t => t == x.Id))
                        tempInputPermissionIds.Add(x.Id);
                });

                tempInputPermissionIds = tempInputPermissionIds.Distinct<long>().ToList();//去重
                for (int i = 0; i < tempInputPermissionIds.Count; i++)
                {
                    SysRolePermission sysRolePermission = new SysRolePermission();
                    sysRolePermission.PermissionId = tempInputPermissionIds[i];
                    sysRolePermission.RoleId = id;
                    sysRolePermission.CreationTime = DateTime.Now;
                    sysRolePermission.CreatorUserId = this.LoginUser.Id;
                    //uow.Orm.Insert<SysRolePermission>(sysRolePermission).ExecuteAffrows();
                    sysRolePermissionList.Add(sysRolePermission);

                }
                await _sysRolePermissionFreeSqlRepository.InsertAsync(sysRolePermissionList);
                _sysRolePermissionFreeSqlRepository.UnitOfWork.Commit();
            }



            return ApiResult.Ok();
        }

        /// <summary>
        /// 前端没有回传 选中的子节点对应的父节点，例如 ：用户管理，当只勾选用户列表，没有勾选全部，用户管理的id前端控件没有将他checked，只有回传一个用户列表权限id
        ///需要通过递归，找到对应的父节点，一起存储
        /// </summary>
        /// <param name="chidrenIds"></param>
        /// <param name="allList"></param>
        /// <param name="parentList"></param>
        private static void GetParentListById(List<long> childrenParentIds, List<SysPermission> allList, ref List<SysPermission> parentList)
        {

            var pList = allList.Where(x => childrenParentIds.Contains(x.Id)).ToList();

            if (pList.Count > 0)
            {//说明第一个级别有内容

                for (int i = 0; i < pList.Count; i++)
                {
                    if (!parentList.Exists(x => x.Id == pList[i].Id))
                    {
                        parentList.Add(pList[i]);//将父节点集合汇总
                    }
                    var data=allList.Where(x => x.Id == pList[i].ParentId).FirstOrDefault();

                    var isExistParent = data == null ? false : true;
                    if (isExistParent)
                    {
                        GetParentListById(new List<long>() { data.ParentId }, allList, ref parentList);
                    }

                }
            }

        }

    }
}
