using System;
using System.Collections.Generic;
using System.Text;

namespace YC.DapperFrameWork
{
   public interface IMicroDbFactory
    {
        IUnitOfWork GetUnitOfWork(DbOperationType operationType = DbOperationType.Write, UnitOfWorkType uowType = UnitOfWorkType.DefaultUnitOfWork, string configFilePath = "");
        LambdaUnitOfWork GetLambdaUnitOfWork(DbOperationType operationType = DbOperationType.Write, UnitOfWorkType uowType = UnitOfWorkType.DefaultUnitOfWork, string configFilePath = "");
    }
}
