
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FreeSql;
using YC.Core;
using YC.Core.Autofac;

namespace YC.FreeSqlFrameWork
{
    public interface IFreeSqlRepository<TEntity, TKey> : IDependencyInjectionSupport, IBaseRepository<TEntity, TKey> where TEntity : class
    {
        public  ITenant Tenant { get; }
        public IFreeSql FreeSql { get; }
    }


    public interface IFreeSqlRepository<TEntity> : IDependencyInjectionSupport, IFreeSqlRepository<TEntity, long> where TEntity : class
    {
       
    }
}
