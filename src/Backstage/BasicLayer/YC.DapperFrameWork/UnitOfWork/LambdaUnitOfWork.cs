using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using YC.Core;
using YC.Core.Autofac;
using YC.DapperFrameWork.LambdaExtensions;
using YC.DapperFrameWork.Mapper;
using YC.DapperFrameWork.Sql;

namespace YC.DapperFrameWork
{

    public interface ILambdaUnitOfWork : IUnitOfWork
    {
       
    }

    public class LambdaUnitOfWork : ILambdaUnitOfWork
    {

        public SqlGeneratorImpl sqlGenerator { get; set; }
        public LambdaUnitOfWork(string dbConnectionString = null, ITenant tenant = null, RepositoryUtils.Dialect dbtype = RepositoryUtils.Dialect.MySQL)
        {
            _id = Guid.NewGuid();
            if (tenant != null)
            {
                _connection = GetOpenConnection(tenant.TenantDbString, dbtype);
             
            }
            else
            {
                _connection = GetOpenConnection(dbConnectionString, dbtype);
            }

            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new MySqlDialect());
            sqlGenerator = new SqlGeneratorImpl(config);
            Db = new Database(_connection, sqlGenerator);

        }
        private RepositoryUtils.Dialect _dbtype;
        public IDbConnection GetOpenConnection(string dbConnectionString = null, RepositoryUtils.Dialect dbtype = RepositoryUtils.Dialect.MySQL)
        {
            _dbtype = dbtype;

            if (_dbtype == RepositoryUtils.Dialect.MySQL)
            {
                _connection = new MySqlConnection(dbConnectionString);

                RepositoryUtils.SetDialect(_dbtype);
            }
            else
            {
                throw new Exception("请指定数据库类型！");
            }

            #region 其他数据库类型配置,需要引用其他数据库dll，查看SimpleCRUD

            //if (_dbtype == RepositoryUtils.Dialect.PostgreSQL)
            //{
            //    connection = new NpgsqlConnection(String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "5432", "postgres", "postgrespass", "testdb"));
            //    RepositoryUtils.SetDialect(RepositoryUtils.Dialect.PostgreSQL);
            //}
            //else if (_dbtype == RepositoryUtils.Dialect.SQLite)
            //{
            //    connection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            //    RepositoryUtils.SetDialect(RepositoryUtils.Dialect.SQLite);
            //}
            //else if (_dbtype == RepositoryUtils.Dialect.MySQL)
            //{
            //    connection = new MySqlConnection(String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", "localhost", "3306", "root", "admin", "testdb"));
            //    RepositoryUtils.SetDialect(RepositoryUtils.Dialect.MySQL);
            //}
            //else
            //{
            //    connection = new SqlConnection(@"Data Source = .\sqlexpress;Initial Catalog=DapperSimpleCrudTestDb;Integrated Security=True;MultipleActiveResultSets=true;");
            //    RepositoryUtils.SetDialect(RepositoryUtils.Dialect.SQLServer);
            //} 
            #endregion

            _connection.Open();
            return _connection;
        }


        IDbConnection _connection = null;

        Guid _id = Guid.Empty;
        private bool _disposed;

        IDbConnection IUnitOfWork.Connection
        {
            get { return Db.Connection; }
        }
        IDbTransaction IUnitOfWork.Transaction
        {
            get { return Db.Transaction; }
        }
        Guid IUnitOfWork.Id
        {
            get { return _id; }
        }

        public IDatabase Db { get; set; }

        public IDbTransaction Begin()
        {
            Db.BeginTransaction();

            return Db.Transaction;

        }

        public IDbTransaction GetTransaction()
        {
            return Db.Transaction;
        }

        public void Commit()
        {
            try
            {
                Db.Commit();
            }
            catch
            {
                Db.Rollback();
                throw;
            }
            finally
            {
                //有疑虑 一个工作单元里面如果上述出现异常，就重新创建一个中国单元
                //_transaction.Dispose();
                Db.BeginTransaction();

            }
            // Dispose();
        }

        public void Rollback()
        {
            Db.Rollback();
            //Dispose();
        }

        //工作单元销毁时候调用进行回收
        public void Dispose()
        {
            Db.Dispose();
            //   _connection.Dispose();
            GC.SuppressFinalize(this);
        }
     
        ~LambdaUnitOfWork()
        {
            Db.Dispose();
        }

    }

}
