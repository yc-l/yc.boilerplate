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
using YC.ApplicationService.ApplicationService.Dto;
using YC.Common.ShareUtils;
using YC.Core.Cache;
using YC.ApplicationService.DefaultConfigure;
using YC.FreeSqlFrameWork;
using YC.Core.DynamicApi;
using YC.Core.DynamicApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using YC.Core;
using YC.ApplicationService.SysUserAppService.Dto;
using YC.Core.Domain.Output;
using AutoMapper;
using System.Linq.Expressions;
using YC.ApplicationService.Dto;
using YC.ApplicationService.ApplicationService.SysUserAppService.Dto;
using YC.ApplicationService.ApplicationService.SysUserAppService.Dto.Input;
using System.IO;

namespace YC.ApplicationService.SysUserAppService
{
    public class SysUserAppService : DapperBaseApplicationService, ISysUserAppService
    {
        public IFreeSqlRepository<SysPermission> _sysPermissionSqlRepository;
        public IFreeSqlRepository<SysUserSysRole> _sysUserSysRoleFreeSqlRepository;
        public IFreeSqlRepository<SysUser> _sysUserFreeSqlRepository { get; set; }
        public IFreeSqlRepository<SysRole> _sysRoleFreeSqlRepository;
        public IUserManager _userManager;//这边注入，变成循环依赖组件关系，会报异常
        private ICacheManager _cacheManager;
        public IMapper _mapper;

        public SysUserAppService(IUnitOfWork unitOfWork, IRepository<SysUser> sysUserRepository,
            IFreeSqlRepository<SysPermission> sysPermissionSqlRepository,
            IFreeSqlRepository<SysUserSysRole> sysUserSysRoleFreeSqlRepository,
            IFreeSqlRepository<SysUser> sysUserFreeSqlRepository,
        IHttpContextAccessor httpContextAccessor,
        IFreeSqlRepository<SysRole> sysRoleFreeSqlRepository, ICacheManager cacheManager, IMapper mapper) : base(unitOfWork, httpContextAccessor, cacheManager)
        {
            _sysPermissionSqlRepository = sysPermissionSqlRepository;
            _sysUserSysRoleFreeSqlRepository = sysUserSysRoleFreeSqlRepository;
            _sysUserFreeSqlRepository = sysUserFreeSqlRepository;
            _mapper = mapper;
            _cacheManager = cacheManager;
            _sysRoleFreeSqlRepository = sysRoleFreeSqlRepository;
        }

        public List<SysUser> GetAllSysUserList()
        {
            var resultList = _sysUserFreeSqlRepository.Select.ToList();
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

            var freeObj = _sysUserFreeSqlRepository.Where(x => x.Account == userId && x.Password == pwd).ToList().FirstOrDefault();

            if (freeObj != null)
            {
                return res.Ok(freeObj, "登录成功！");
            }
            else
            {
                return res.NotOk("登录失败！用户名或密码不正确！");
            }
        }

        [HttpPost]
        public async Task<PersonInfoDto> GetUserInfo()
        {
            var data = GetLoginUser();
            var user = await _sysUserFreeSqlRepository.GetAsync(data.Id);
            PersonInfoDto personInfoDto = _mapper.Map<PersonInfoDto>(user);
            if (!string.IsNullOrWhiteSpace(personInfoDto.Avatar))
            {
                var webRootPath = System.Environment.CurrentDirectory;
                string filePath = webRootPath + personInfoDto.Avatar;
                if (File.Exists(filePath))
                {
                    personInfoDto.Avatar = ImageUtils.GetBase64FromImage(filePath);
                }
            }
            return personInfoDto;
        }

        public async Task<IApiResult> ChangePassword(ChangePasswordInputDto input)
        {
            if (input.NewPassword != input.ConfirmPassword)
            {
                return ApiResult.NotOk("修改密码和确认密码不一致！");
            }
            var data = GetLoginUser();
            if (data.Id.ToString() != input.Id)
            {
                return ApiResult.NotOk("修改密码用户与当前登录用户不一致！");
            }
            string inputOldPwd = EncryptUtils.MD5(input.OldPassword);
            var user = await _sysUserFreeSqlRepository.GetAsync(data.Id);
            if (user.Password != inputOldPwd)
            {
                return ApiResult.NotOk("原始密码错误！");
            }
            user.Password = EncryptUtils.MD5(input.NewPassword);
            var result = await _sysUserFreeSqlRepository.UpdateAsync(user);
            if (result > 0)
            {
                return ApiResult.Ok("修改密码成功！");
            }
            else
            {
                return ApiResult.NotOk("修改密码失败！");
            }
        }

        public async Task<IApiResult> UploadUserAvatar(IFormFileCollection formFiles)
        {
            // var file = uploadFileDto.File;
            if (formFiles.Count == 0)
            {
                return ApiResult.NotOk("文件为空！");
            }
            var file = formFiles[0];
            //返回的文件地址
            string filename = "";
            var now = DateTime.Now;
            //文件存储路径
            var filePath = string.Format("/Uploads/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("yyyyMM"), now.ToString("yyyyMMdd"));
            //获取当前web目录
            var webRootPath = System.Environment.CurrentDirectory;
            if (!Directory.Exists(webRootPath + filePath))
            {
                Directory.CreateDirectory(webRootPath + filePath);
            }

            try
            {
                if (file != null)
                {
                    #region 图片文件的条件判断

                    //文件后缀
                    var fileExtension = Path.GetExtension(file.FileName);

                    //判断后缀是否是图片
                    const string fileFilt = ".gif|.jpg|.jpeg|.png";
                    if (fileExtension == null)
                    {
                        //break;
                        return ApiResult.NotOk("上传的文件没有后缀!");
                    }
                    if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                    {
                        //break;
                        return ApiResult.NotOk("请上传jpg、png、gif格式的图片!");
                    }

                    //判断文件大小
                    long length = file.Length;
                    if (length > 1024 * 1024 * 2) //2M
                    {
                        //break;
                        return ApiResult.NotOk("上传的文件不能大于2M!");
                    }

                    #endregion 图片文件的条件判断

                    var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                    var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                    var saveName = strDateTime + strRan + fileExtension;

                    //插入图片数据
                    using (FileStream fs = System.IO.File.Create(webRootPath + filePath + saveName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    filename = filePath + saveName;
                }

                var user = GetLoginUser();
                var result = await UpdateUserAvatar(user.Id, filename);
                if (result.State)
                {
                    string imgStr = ImageUtils.GetBase64FromImage(webRootPath + filename);
                    return ApiResult.Ok<string>(imgStr);
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return ApiResult.NotOk("上传失败!" + ex.ToString());
            }
        }

        public async Task<IApiResult> UpdateUserAvatar(long id, string filePath)
        {
            var tempUser = await _sysUserFreeSqlRepository.GetAsync(id);
            tempUser.Avatar = filePath;
            var updateState = await _sysUserFreeSqlRepository.UpdateAsync(tempUser);
            if (updateState > 0)
            {
                return ApiResult.Ok();
            }
            else
            {
                return ApiResult.NotOk("上传失败!数据更新异常！");
            }
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
            var user = _sysUserFreeSqlRepository.Get(id);
            if (!(user?.Id > 0))
            {
                return res.NotOk("用户不存在！");
            }

            #region 多表连接查询

            var list = _sysUserFreeSqlRepository.Orm.Select<SysUserSysRole, SysRole, SysRolePermission, SysPermission>()
                                 .InnerJoin((a, b, c, d) => a.SysRole_ID == b.Id)
                                .InnerJoin((a, b, c, d) => b.Id == c.RoleId)
                                 .InnerJoin((a, b, c, d) => c.PermissionId == d.Id)
                                .Where((a, b, c, d) => a.SysUser_ID == user.Id).OrderBy((a, b, c, d) => d.Sort)
                                .ToList((a, b, c, d) => new
                                {
                                    roleId = b.Id,
                                    roleName = b.Name,
                                    d
                                });

            #endregion 多表连接查询

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
            entityDto.PermissionList = _mapper.Map<List<SysPermissionDto>>(permissionDistinctList).OrderBy(x => x.Sort).ToList();

            return res.Ok(entityDto);
        }
    }
}