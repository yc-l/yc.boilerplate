using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.FreeSqlFrameWork
{
    public class FreeSqlUtils
    {
        public static IFreeSql GetFreeSql(int dbType,string connectionString)
        {
            var freeSqlBuilder = new FreeSqlBuilder()
                      .UseConnectionString((DataType)dbType, connectionString)
                      .UseLazyLoading(false)
                      .UseNoneCommandParameter(true);
            var fsql = freeSqlBuilder.Build();
            return fsql;
        }
    }
}
