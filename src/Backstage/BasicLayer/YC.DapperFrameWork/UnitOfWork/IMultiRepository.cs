using System;
using System.Collections.Generic;
using System.Text;
using YC.Core.Autofac;

namespace YC.DapperFrameWork
{
    public interface IMultiRepository<TEntity> : IDependencyInjectionSupport where TEntity : class, new()
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TKey">主键key</typeparam>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        TKey Insert<TKey>(TEntity entity);
    }
}
