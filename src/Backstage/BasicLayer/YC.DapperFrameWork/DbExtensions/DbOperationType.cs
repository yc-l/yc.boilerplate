using System;
using System.Collections.Generic;
using System.Text;

namespace YC.DapperFrameWork
{
    /// <summary>
    /// 数据库操作类别
    /// </summary>
    public enum DbOperationType
    {

        Write = 1,
        Read = 0
    }

    public enum UnitOfWorkType
    {

        DefaultUnitOfWork = 1,
        LambdaUnitOfWork = 0
    }
}
