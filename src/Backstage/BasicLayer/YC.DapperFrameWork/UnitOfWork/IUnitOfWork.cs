using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using YC.Core.Autofac;
using YC.DapperFrameWork.LambdaExtensions;
using YC.DapperFrameWork.Sql;

namespace YC.DapperFrameWork
{
    public interface IUnitOfWork: IDisposable, IDependencyInjectionSupport
    {

        Guid Id { get; }
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
      
        IDbTransaction Begin();
        IDbTransaction GetTransaction();
        void Commit();
        void Rollback();

        IDatabase Db { get; set; }
    }
}
