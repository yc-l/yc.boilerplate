using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YC.ApplicationService.Dto;
using YC.Common.ShareUtils;
using YC.Core;
using YC.Core.Cache;
using YC.Core.Domain;
using YC.Core.Domain.Output;
using YC.Core.DynamicApi;
using YC.Core.DynamicApi.Attributes;
using YC.FreeSqlFrameWork;

namespace YC.ApplicationService.Base
{
   
    public class FreeSqlEntityApplicationService<T,TKey> : BaseApplicationService, IEntityApplicationService<T,TKey> where T : class, new()
    {
        public readonly IFreeSqlRepository<T,TKey> _entityFreeSqlRepository;
        public readonly IMapper _mapper;
        public FreeSqlEntityApplicationService(IHttpContextAccessor httpContextAccessor, IFreeSqlRepository<T,TKey> entityFreeSqlRepository, IMapper mapper, ICacheManager cacheManager)
         : base(httpContextAccessor, cacheManager)
        {
            _mapper = mapper;
            _entityFreeSqlRepository = entityFreeSqlRepository;
        }

        public async virtual Task<IApiResult> CreateAsync(T input)
        {
            input = SetIdKey(input);
            var obj = await _entityFreeSqlRepository.InsertAsync(input);
          
            string keyName = "";
            keyName = GetKeyName<T>(input);
            var objId = obj?.GetType().GetProperty(keyName).GetValue(obj);
            if (objId == null)
            {
                return ApiResult.NotOk();
            }
            return ApiResult.Ok();
        }

        public async virtual Task<IApiResult> DeleteByIdAsync(TKey id)
        {
            var result = false;
            string keyName = "";
            if (id != null)
            {
                T t = new T();
                keyName = GetKeyName<T>(t);
                var keyProperInfo = t.GetType().GetProperty(keyName);
                result = (await _entityFreeSqlRepository.DeleteAsync((TKey)id)) > 0;
            }

            return ApiResult.Result(result);
        }

        public async virtual Task<IApiResult<T>> GetAsync(TKey id)
        {
            var res = new ApiResult<T>();

            var entity = await _entityFreeSqlRepository.GetAsync(id);
         
            return res.Ok(entity);
        }

        public async virtual Task<IApiResult> GetPageEntityListAsync(PageInput<PageInputDto> input = default)
        {
            Expression<Func<T, bool>> exp = null;
            if (input == null)
            {
                input = new PageInput<PageInputDto>();
            }
            else if (input.Filter == null) {
                input.Filter = new PageInputDto();
            }
            T t = new T();
            string keyName = "";
            keyName = GetKeyName<T>(t);
            var keyProperInfo = t.GetType().GetProperty(keyName);
            var list = await _entityFreeSqlRepository.Select.WhereIf(input.Filter.QueryString.NotNull(), exp)
                .Count(out var total).OrderByPropertyName(keyName, true).Page(input.CurrentPage, input.PageSize)
                .ToListAsync();

            ///返回数据必须是明确实体，要不然可能存在json映射死循环
            var data = new PageOutput<T>()
            {
                List = list,
                Total = total
            };


            return ApiResult.Ok(data);
        }

        public async virtual Task<IApiResult> UpdateEntityAsync(T input)
        {
          
            string keyName = "";
            keyName = GetKeyName<T>(input);
           
            object inputId = input.GetType().GetProperty(keyName).GetValue(input);

            if (string.IsNullOrWhiteSpace(inputId?.ToString()))
            {
                return ApiResult.NotOk("Id 不能为空!");
            }

            var changeCount = await _entityFreeSqlRepository.UpdateAsync(input);
            if (changeCount > 0)
            {
                return ApiResult.Ok();
            }
            else
            {
                return ApiResult.NotOk();
            }

        }


    }
}
