using Dapper;
using System;
using Xunit;
using YC.Common.ShareUtils;
using YC.DapperFrameWork;

namespace YC.Boilerplate.XUnitTest
{
    public class SqlFileUnitTest
    {
        [Fact]
        public void ExcuteSqlFileTest()
        {
            string connectionString = "Server=127.0.0.1;Port=3307;User Id=root;Password=123456;Database=bigDataDB;";
            IUnitOfWork unitOfWork = new DefaultUnitOfWork(connectionString);
            var connection = unitOfWork.Connection;
            string sqlFile = System.Environment.CurrentDirectory + @"/DbSqlFile/bigdatadb.sql";
            string sqlContent = "";
            var isRead = FileUtils.ReadFile(sqlFile, out sqlContent);
            if (isRead && !string.IsNullOrWhiteSpace(sqlContent))
            {
                var count = connection.Execute(sqlContent);
                Assert.True(count > 0);
            }



        }
    }
}
