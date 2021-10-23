using System;
using System.Collections.Generic;
using System.Text;
using YC.DapperFrameWork;
using YC.Core.Domain;
using System.Threading.Tasks;
using Dapper;
using YC.DapperFrameWork;
using YC.Core.Autofac;
using Autofac.Extras.DynamicProxy;
using YC.Core.Attribute;
using Microsoft.AspNetCore.Http;
using YC.Model.SysDbEntity;
using System.Linq;
using YC.Micro.UserApplicationService.ApplicationService.Dto;
using YC.Common.ShareUtils;
using YC.Core.Cache;
using YC.Micro.UserApplicationService.DefaultConfigure;
using YC.FreeSqlFrameWork;
using YC.Core.DynamicApi;
using YC.Core.DynamicApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using YC.Core;
using YC.Micro.UserApplicationService.SysUserAppService.Dto;
using YC.Core.Domain.Output;
using AutoMapper;
using System.Linq.Expressions;
using YC.Micro.UserApplicationService.Dto;
using YC.Micro.UserApplicationService.ApplicationService.SysUserAppService.Dto;

namespace YC.Micro.UserApplicationService.SysUserAppService
{


    public class SysUserAppService :  ISysUserAppService
    {

        public IFreeSqlRepository<SysUser> _sysUserFreeSqlRepository;
        ICacheManager _cacheManager;
        public IMapper _mapper;
        public SysUserAppService(
            IFreeSqlRepository<SysUser> sysUserFreeSqlRepository,
        ICacheManager cacheManager, IMapper mapper) 
        {
            _sysUserFreeSqlRepository = sysUserFreeSqlRepository;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }


        public List<SysUser> GetAllSysUserList()
        {
            var resultList = _sysUserRepository.GetAllList();
            return resultList;
        }

        [NoDynamicMethod]
        public IApiResult<SysUser> Login(string userId, string pwd, int tenantId = 1)
        {
           
            var res = new ApiResult<SysUser>();

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(pwd))
            {
               
                return res.NotOk("用户名或者密码不能为空！");
            }
            pwd = EncryptUtils.MD5(pwd);

            //var freeObj = _sysUserFreeSqlRepository.Where(x => x.Account == userId && x.Password == pwd).ToList().FirstOrDefault();
            var resultList = _sysUserRepository.GetList(new { Account = userId, Password = pwd });
            if (resultList.Count > 0)
            {
                return res.Ok(resultList.FirstOrDefault(), "登录成功！");
            }
            else
            {
               
                return res.NotOk( "登录失败！用户名或密码不正确！");
            }

           
        }


        [HttpPost]
        public List<SysUser> GetUserList(object condition)
        {
            //var temp=   this.LoginUser;
            var resultList = _sysUserRepository.GetList(condition);
            return resultList;
        }
        [HttpPost]
        public async Task<IApiResult> GetPageUserListAsync(PageInput<PageInputDto> input)
        {
            Expression<Func<SysUser, bool>> exp = null;
            if (input.Filter != null)
            {
                exp = a => a.Name.Contains(input.Filter.QueryString) ||
                                a.Account.Contains(input.Filter.QueryString) ||
                                a.Email.Contains(input.Filter.QueryString) ||
                                a.Mobile.Contains(input.Filter.QueryString) ||
                                a.Remark.Contains(input.Filter.QueryString);
            }
            var list = await _sysUserFreeSqlRepository.Select.WhereIf(input.Filter.QueryString.NotNull(), exp)
                .Count(out var total).OrderByDescending(true, a => a.Id).Page(input.CurrentPage, input.PageSize)
                .ToListAsync();

            var data = new PageOutput<PageUserOutputDto>()
            {
                List = _mapper.Map<List<PageUserOutputDto>>(list),
                Total = total
            };

            return ApiResult.Ok(data);
        }

        public async Task<ApiResult<UserAddOrEditDto>> GetAsync(long id)
        {
            var res = new ApiResult<UserAddOrEditDto>();

            var entity = await _sysUserFreeSqlRepository.Select
            .WhereDynamic(id)
            .ToOneAsync();
            var userRoles = await _sysUserSysRoleFreeSqlRepository.Orm.Select<SysUser, SysUserSysRole, SysRole>()
                 .InnerJoin((a, b, c) => a.Id == b.SysUser_ID)
                 .InnerJoin((a, b, c) => b.SysRole_ID == c.Id).Where((a, b, c) => a.Id == id)
                 .ToListAsync((a, b, c) => new { a.Id, b.SysRole_ID, c.Name });

            var roleList = await _sysRoleFreeSqlRepository.Select.ToListAsync();
            var entityDto = _mapper.Map<UserAddOrEditDto>(entity);
            // entityDto.RoleInfoList = _mapper.Map<List<RoleInfoDto>>(roleList);//原始角色数据
            entityDto.UserRoleIds = _mapper.Map<string[]>(userRoles.Select(x => x.SysRole_ID));//用户自己的角色信息
            return res.Ok(entityDto);
        }

        [HttpPost]
        public async Task<IApiResult> CreateUserAsync(UserAddOrEditDto input)
        {

            input.Password = EncryptUtils.MD5(input.Password);
            input.Id = "0";//做一个处理，要不然automapper 无法转换
            var entity = _mapper.Map<SysUser>(input);
            bool isExistUser = _sysUserFreeSqlRepository.WhereIf(input.Account.NotNull(), x => x.Account == input.Account).Any();
            if (isExistUser)
            {
                return ApiResult.NotOk("用户账号已存在，请换一个账号！");
            }

            ///事务变更
            using (var uow = _sysUserFreeSqlRepository.Orm.CreateUnitOfWork())
            {
                _sysUserFreeSqlRepository.UnitOfWork = uow;
                _sysUserSysRoleFreeSqlRepository.UnitOfWork = uow;

                var user = await _sysUserFreeSqlRepository.InsertAsync(entity);
                List<SysUserSysRole> sysUserSysRoleList = new List<SysUserSysRole>();

                foreach (var i in input.UserRoleIds)
                {
                    SysUserSysRole sysUserSysRole = new SysUserSysRole();//内部实例化一个，存入list，避免外部同一个对象，由于浅拷贝，导致list存入同一个对象  
                    sysUserSysRole.SysRole_ID = long.Parse(i);
                    sysUserSysRole.SysUser_ID = user.Id;
                    sysUserSysRoleList.Add(sysUserSysRole);
                }
                var sysUserRoles = await _sysUserSysRoleFreeSqlRepository.InsertAsync(sysUserSysRoleList);
                uow.Commit();//提交事务
                if (sysUserSysRoleList.Count == 0)//允许没有角色分配
                {
                    if (!(user?.Id > 0))
                    {
                        return ApiResult.NotOk();
                    }
                }
                else
                {
                    if (!(user?.Id > 0 && sysUserRoles?.Count > 0))//有分配角色，如果存在返回结果为0就报告失败
                    {
                        return ApiResult.NotOk();
                    }
                }

            }
            return ApiResult.Ok();
        }

        public async Task<IApiResult> DeleteUserByIdAsync(long id)
        {
            var result = true;
            if (id > 0)
            {
                using (var uow = _sysUserFreeSqlRepository.Orm.CreateUnitOfWork())
                {
                    _sysUserFreeSqlRepository.UnitOfWork = uow;
                    _sysUserSysRoleFreeSqlRepository.UnitOfWork = uow;
                    var deleteUser = await _sysUserFreeSqlRepository.DeleteAsync(m => m.Id == id);

                    var isExistUserRole = await _sysUserSysRoleFreeSqlRepository.Select.Where(x => x.SysUser_ID == id).AnyAsync();

                    if (isExistUserRole)
                    {
                        var deleteUserRole = await _sysUserSysRoleFreeSqlRepository.DeleteAsync(x => x.SysUser_ID == id);
                        result = deleteUserRole > 0 ? true : false;
                    }
                    if (!(result && deleteUser > 0))//如果存在一个是false，那结果为false，
                        result = false;

                    uow.Commit();
                }
            }

            return ApiResult.Result(result);
        }

        public async Task<IApiResult> UpdateUserAsync(UserAddOrEditDto input)
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

            var user = await _sysUserFreeSqlRepository.GetAsync(id);

            if (!(user?.Id > 0))
            {
                return ApiResult.NotOk("用户不存在！");
            }
            var tempPassword = user.Password;
            _mapper.Map(input, user);
            user.Password = tempPassword;//处理密码，展示不开放修改，后续单独开功能修改

            ///事务变更
            using (var uow = _sysUserFreeSqlRepository.Orm.CreateUnitOfWork())
            {
                _sysUserFreeSqlRepository.UnitOfWork = uow;
                _sysUserSysRoleFreeSqlRepository.UnitOfWork = uow;

                var updateUser = await _sysUserFreeSqlRepository.UpdateAsync(user);
                List<SysUserSysRole> sysUserSysRoleList = new List<SysUserSysRole>();
                //先删除
                var deleteUserRolesCount = await _sysUserSysRoleFreeSqlRepository.DeleteAsync(x => x.SysUser_ID == user.Id);

                foreach (var i in input.UserRoleIds)
                {

                    SysUserSysRole sysUserSysRole = new SysUserSysRole();//内部实例化一个，存入list，避免外部同一个对象，由于浅拷贝，导致list存入同一个对象
                    sysUserSysRole.SysRole_ID = long.Parse(i);
                    sysUserSysRole.SysUser_ID = user.Id;
                    sysUserSysRoleList.Add(sysUserSysRole);
                }

                //再批量新增
                var sysUserRoles = await _sysUserSysRoleFreeSqlRepository.InsertAsync(sysUserSysRoleList);
                uow.Commit();//提交事务

            }

            return ApiResult.Ok();
        }

        /// <summary>
        /// 不将这个方法映射为动态Api，限制安全查询出口，只在登录业务那边去内部请求获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [NoDynamicMethod]
        public IApiResult<UserRolePermissionDto> GetUserRolePermission(long id)
        {
            var res = new ApiResult<UserRolePermissionDto>();
            var user =  _sysUserFreeSqlRepository.Get(id);
            if (!(user?.Id > 0))
            {
                return res.NotOk("用户不存在！");
            }
            #region 多表连接查询
            var list =  _sysUserFreeSqlRepository.Orm.Select<SysUserSysRole, SysRole, SysRolePermission, SysPermission>()
                                 .InnerJoin((a, b, c, d) => a.SysRole_ID == b.Id)
                                .InnerJoin((a, b, c, d) => b.Id == c.RoleId)
                                 .InnerJoin((a, b, c, d) => c.PermissionId == d.Id)
                                .Where((a, b, c, d) => a.SysUser_ID == user.Id).OrderBy((a, b, c, d) => d.Sort)
                                .ToList((a, b, c, d) => new
                                {
                                    roleId = b.Id,
                                    roleName =b.Name,
                                    d
                                });
            #endregion

            if (list.Count == 0)
            {
                return res.NotOk("找不到指定用户的角色权限！");
                ;
            }
            var permissionList = list.Select(x => x.d).ToList();
            var permissionDistinctList = new List<SysPermission>();


            //1、获取去重权限集合
            permissionList.ForEach(x =>//整理去重的权限集合
            {
                if (!permissionDistinctList.Exists(t => t.Id == x.Id))
                {
                    permissionDistinctList.Add(x);
                }
            });
            var permissions = permissionDistinctList.Select(x => x.Id).ToList();
        
             //2、获取所有数据的去重集合
             var distinctAllList = list.Where(x => permissions.Contains(x.d.Id)).ToList();//所有数据去重
            //3、获取角色去重数据
            var roleList = new List<RoleInfoDto>();
            distinctAllList.ForEach(x =>
            {
                if (!roleList.Exists(t => t.Id == x.roleId.ToString()))
                    roleList.Add(new RoleInfoDto() { Id = x.roleId.ToString(), RoleName = x.roleName });
            });

            var entityDto = _mapper.Map<UserRolePermissionDto>(user);
            entityDto.RoleInfoList = roleList;
            entityDto.PermissionList = _mapper.Map<List<SysPermissionDto>>(permissionDistinctList);

            return res.Ok(entityDto);
        }



    }
}
