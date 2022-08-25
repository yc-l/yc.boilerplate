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
using FreeSql;
using System.Transactions;

namespace YC.ApplicationService
{
    /// <summary>
    ///  租户管理业务实现接口
    /// </summary>
    public class SysTenantAppService : FreeSqlBaseApplicationService, ISysTenantAppService
    {
        private readonly IFreeSqlRepository<SysTenant, Int64> _sysTenantFreeSqlRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        public IdleBus<IFreeSql> _idleBus;
        /// <summary>
        /// 构造函数自动注入我们所需要的类或接口
        /// </summary>
        public SysTenantAppService(IFreeSqlRepository<SysTenant, Int64> sysTenantFreeSqlRepository,
        IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager, IdleBus<IFreeSql> idleBus, IMapper mapper) : base(httpContextAccessor, cacheManager)
        {
            _sysTenantFreeSqlRepository = sysTenantFreeSqlRepository;
            _cacheManager = cacheManager;
            _mapper = mapper;
            _idleBus = idleBus;
        }

        /// <summary>
        /// 通过Id查找数据对象
        /// </summary>
        /// <returns>返回数据集合</returns>

        public async Task<ApiResult<SysTenantAddOrEditDto>> GetAsync(long id)
        {
            var res = new ApiResult<SysTenantAddOrEditDto>();

            var entity = await _sysTenantFreeSqlRepository.Select
            .WhereDynamic(id)
            .ToOneAsync();

            var entityDto = _mapper.Map<SysTenantAddOrEditDto>(entity);
            return res.Ok(entityDto);
        }

        [HttpPost]
        public async Task<IApiResult> GetPageSysTenantListAsync(PageInput<PageInputDto> input)
        {
            Expression<Func<SysTenant, bool>> exp = null;
            if (input.Filter != null)
            {
                //exp = a => a.Name.Contains(input.Filter.QueryString);
            }
            var list = await _sysTenantFreeSqlRepository.Select.WhereIf(input.Filter.QueryString.NotNull(), exp)
                .Count(out var total).OrderByDescending(true, a => a.Id).Page(input.CurrentPage, input.PageSize)
                .ToListAsync();

            ///返回数据必须是明确实体，要不然可能存在json映射死循环
            var data = new PageOutput<SysTenantAddOrEditDto>()
            {
                List = _mapper.Map<List<SysTenantAddOrEditDto>>(list),
                Total = total
            };

            data.List.ForEach(x =>
            {
                x.DbTypeName = Enum.GetName(typeof(DataType), x.DbType);
            });
            return ApiResult.Ok(data);
        }

        [HttpPost]
        public async Task<IApiResult> CreateSysTenantAsync(SysTenantAddOrEditDto input)
        {
            input.Id = "0";//做一个处理，要不然automapper 无法转换
            var entity = _mapper.Map<SysTenant>(input);
            bool isExist = _sysTenantFreeSqlRepository.Select.Any(x => x.TenantId == input.TenantId);
            if (isExist)
            {
                return ApiResult.NotOk("租户Id已经存在！");
            }
            var obj = await _sysTenantFreeSqlRepository.InsertAsync(entity);

            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk();
            }
            return ApiResult.Ok();
        }

        public async Task<IApiResult> DeleteSysTenantByIdAsync(long id)
        {
            var result = false;
            if (id > 0)
            {
                result = (await _sysTenantFreeSqlRepository.DeleteAsync(m => m.Id == id)) > 0;
            }

            return ApiResult.Result(result);
        }

        public async Task<IApiResult> UpdateSysTenantAsync(SysTenantAddOrEditDto input)
        {
            if (string.IsNullOrWhiteSpace(input.Id))
            {
                return ApiResult.NotOk("租户管理Id 不能为空!");
            }
            long id = Convert.ToInt64(input?.Id);

            var obj = await _sysTenantFreeSqlRepository.GetAsync(id);
            if (!(obj?.Id > 0))
            {
                return ApiResult.NotOk("对象不存在！");
            }

            _mapper.Map(input, obj);
            //除了自身外，不允许出现同样的tenantid，必须保证tenanTid唯一
            bool isExist = _sysTenantFreeSqlRepository.Select.Any(x => x.TenantId == input.TenantId && x.Id != obj.Id);
            if (isExist)
            {
                return ApiResult.NotOk("租户Id已经存在！");
            }
            await _sysTenantFreeSqlRepository.UpdateAsync(obj);

            return ApiResult.Ok();
        }

        public void TestTranscation()
        {

            var freeSql1 = _idleBus.Get(1);
            var freeSql2 = _idleBus.Get(2);

            var st1Repository = freeSql1.GetRepository<SysTenant>();
            var st2Repository = freeSql2.GetRepository<SysTenant>();
            freeSql1.Transaction(() => {

                var t1 = st1Repository.Insert(new SysTenant() { TenantId = 1, DbConnectionString = "aaa" });
                var t2 = st2Repository.Insert(new SysTenant() { TenantId = 2, DbConnectionString = "bb" });
                throw new Exception("测试");

            });
            //using (TransactionScope scope = new TransactionScope())//mysql 不支持
            //{
            //    try
            //    {
            //        var t1 =  st1Repository.Insert(new SysTenant() { TenantId = 1, DbConnectionString = "aaa" });
            //        var t2 =  st2Repository.Insert(new SysTenant() { TenantId = 2, DbConnectionString = "bb" });
            //        throw new Exception("测试");
            //        scope.Complete();
            //    }
            //    catch (Exception ex)
            //    {

                 

            //    }


            //    #region freesql 事务 无法达到效果
            //    //    using (var uowManager = new UnitOfWorkManager(freeSql1)) //使用 UnitOfWorkManager 管理类事务
            //    //{
            //    //    using (var uow = uowManager.Begin())
            //    //    {
            //    //        var st1Repository = freeSql1.GetRepository<SysTenant>();
            //    //        var st2Repository = freeSql2.GetRepository<SysTenant>();
            //    //            st1Repository.UnitOfWork = uow;
            //    //            st2Repository.UnitOfWork = uow;
            //    //            var t1 = await st1Repository.InsertAsync(new SysTenant() { TenantId = 1, DbConnectionString = "aaa" });
            //    //            var t2 = await st2Repository.InsertAsync(new SysTenant() { TenantId = 2, DbConnectionString = "bb" });
            //    //            uow.Commit();

            //    //    }
            //    //} 
            //    #endregion
            //}
        }
    }
}