using System;
using System.Collections.Generic;
using System.Text;
using YC.DapperFrameWork;
using YC.DapperFrameWork.DbExtensions;

namespace YC.CodeGenerate
{
    public class ReadWriteSeparationFactory : IReadWriteSeparationFactory
    {
        public LambdaUnitOfWork GetLambdaUnitOfWork(DbOperationType operationType = DbOperationType.Write, UnitOfWorkType uowType = UnitOfWorkType.DefaultUnitOfWork, string configFilePath = "")
        {
            throw new NotImplementedException();
        }

        public IUnitOfWork GetUnitOfWork(DbOperationType operationType = DbOperationType.Write, UnitOfWorkType uowType = UnitOfWorkType.DefaultUnitOfWork, string configFilePath = "")
        {
            IUnitOfWork unitOfWork = null;
            DbConfig.dbConfigFilePath = configFilePath;
            var dbconfig = new DbConfig();

            try
            {
                if (uowType == UnitOfWorkType.DefaultUnitOfWork)
                {
                    switch (operationType)
                    {
                        case DbOperationType.Write:
                            unitOfWork = dbconfig.IsOpenReadWriteSeparation ? new DefaultUnitOfWork(dbconfig.WirteDConcectionString) : new DefaultUnitOfWork(dbconfig.DefaultDbConnectionString); break;
                        case DbOperationType.Read:
                            unitOfWork = dbconfig.IsOpenReadWriteSeparation ? new DefaultUnitOfWork(dbconfig.ReadDConcectionString) : new DefaultUnitOfWork(dbconfig.DefaultDbConnectionString); break;
                        default:
                            unitOfWork = dbconfig.IsOpenReadWriteSeparation ? new DefaultUnitOfWork(dbconfig.WirteDConcectionString) : new DefaultUnitOfWork(dbconfig.DefaultDbConnectionString); break;

                    }
                }
                if (uowType == UnitOfWorkType.LambdaUnitOfWork)
                {
                    switch (operationType)
                    {
                        case DbOperationType.Write:
                            unitOfWork = dbconfig.IsOpenReadWriteSeparation ? new LambdaUnitOfWork(dbconfig.WirteDConcectionString) : new LambdaUnitOfWork(dbconfig.DefaultDbConnectionString); break;
                        case DbOperationType.Read:
                            unitOfWork = dbconfig.IsOpenReadWriteSeparation ? new LambdaUnitOfWork(dbconfig.ReadDConcectionString) : new LambdaUnitOfWork(dbconfig.DefaultDbConnectionString); break;
                        default:
                            unitOfWork = dbconfig.IsOpenReadWriteSeparation ? new LambdaUnitOfWork(dbconfig.WirteDConcectionString) : new LambdaUnitOfWork(dbconfig.DefaultDbConnectionString); break;

                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return unitOfWork;

        }

    
    }
}
