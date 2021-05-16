

using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FreeSql;
using YC.Core;
using System.Threading;

namespace YC.FreeSqlFrameWork
{
    public  class FreeSqlRepository<TEntity, TKey> : BaseRepository<TEntity, TKey>, IFreeSqlRepository<TEntity, TKey> where TEntity : class, new()
    {
          ITenant IFreeSqlRepository<TEntity, TKey>.Tenant { get { return this._tenant; } }
         IFreeSql IFreeSqlRepository<TEntity, TKey>.FreeSql { get { return this._freeSql; } }
        public ITenant _tenant;
        public IFreeSql _freeSql;
        public FreeSqlRepository(IdleBus<IFreeSql> idleBus, ITenant tenant) : base(idleBus.Get(tenant.TenantId), null, null)
        {
            _freeSql = idleBus.Get(tenant.TenantId);
            _tenant = tenant;
        }

       
    }

    public class FreeSqlRepository<TEntity> : BaseRepository<TEntity, long>, IFreeSqlRepository<TEntity> where TEntity : class, new()
    {
        ITenant IFreeSqlRepository<TEntity, long>.Tenant { get { return this._tenant; } }
        IFreeSql IFreeSqlRepository<TEntity, long>.FreeSql { get { return this._freeSql; } }
        public  ITenant _tenant;
        public IFreeSql _freeSql;
        public FreeSqlRepository(IdleBus<IFreeSql> idleBus, ITenant tenant) : base(idleBus.Get(tenant.TenantId), null, null)
        {
            _freeSql = idleBus.Get(tenant.TenantId);
            _tenant = tenant;
        }


    }

}
