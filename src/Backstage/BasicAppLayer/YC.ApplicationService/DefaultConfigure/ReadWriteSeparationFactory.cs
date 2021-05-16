using System;
using System.Collections.Generic;
using System.Text;
using YC.DapperFrameWork;
using YC.DapperFrameWork.DbExtensions;

namespace YC.ApplicationService
{
    public class ReadWriteSeparationFactory : IReadWriteSeparationFactory
    {
        public LambdaUnitOfWork GetLambdaUnitOfWork(DbOperationType operationType = DbOperationType.Write, UnitOfWorkType uowType = UnitOfWorkType.DefaultUnitOfWork, string configFilePath = "")
        {
            throw new NotImplementedException();
        }



        public IUnitOfWork GetUnitOfWork(DbOperationType operationType = DbOperationType.Write, UnitOfWorkType uowType = UnitOfWorkType.DefaultUnitOfWork, string configFilePath = "")
        {
            throw new NotImplementedException();
        }
    }
}
