using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using YC.Core;
using YC.Core.Autofac;
using YC.DapperFrameWork.LambdaExtensions;
using YC.DapperFrameWork.Sql;

namespace YC.DapperFrameWork
{
    public class DefaultUnitOfWork : IUnitOfWork
    {

        public DefaultUnitOfWork(string dbConnectionString = null, ITenant tenant = null, RepositoryUtils.Dialect defaultDbType = RepositoryUtils.Dialect.MySQL)
        {
            _id = Guid.NewGuid();
            if (tenant != null)
            {
                _connection = GetOpenConnection(tenant.TenantDbString, (RepositoryUtils.Dialect)tenant.TenantDbType);

            }
            else
            {
                _connection = GetOpenConnection(dbConnectionString, defaultDbType);
            }


        }
        private RepositoryUtils.Dialect _dbtype;
        public IDbConnection GetOpenConnection(string dbConnectionString = null, RepositoryUtils.Dialect dbtype = RepositoryUtils.Dialect.MySQL)
        {
            _dbtype = dbtype;

            if (_dbtype == RepositoryUtils.Dialect.MySQL)
            {
                _connection = new MySqlConnection(dbConnectionString);

            }
            else if (_dbtype == RepositoryUtils.Dialect.SQLServer)
            {
                _connection = new SqlConnection(dbConnectionString);

            }
            else
            {
                throw new Exception("请指定数据库类型！");
            }
            RepositoryUtils.SetDialect(_dbtype);

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
        IDbTransaction _transaction = null;
        Guid _id = Guid.Empty;
        private bool _disposed;

        IDbConnection IUnitOfWork.Connection
        {
            get { return _connection; }
        }
        IDbTransaction IUnitOfWork.Transaction
        {
            get { return _transaction; }
        }
        Guid IUnitOfWork.Id
        {
            get { return _id; }
        }
        //这里没有用
        public IDatabase Db { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IDbTransaction Begin()
        {
            _transaction = _transaction ?? _connection.BeginTransaction();
            return _transaction;

        }

        public IDbTransaction GetTransaction()
        {
            return _transaction;
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                //有疑虑 一个工作单元里面如果上述出现异常，就重新创建一个中国单元
                //_transaction.Dispose();
                _transaction = _connection.BeginTransaction();

            }
            // Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            //Dispose();
        }

        //工作单元销毁时候调用进行回收
        public void Dispose()
        {
            dispose(true);
            //   _connection.Dispose();
            GC.SuppressFinalize(this);
        }
        private void dispose(bool disposing)
        {

            if (disposing)
            {
                if (_transaction != null)
                {

                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_connection != null)
                {
                    _connection.Close();
                    _connection.Dispose();
                    _connection = null;
                }
            }

        }
        ~DefaultUnitOfWork()
        {
            dispose(true);
        }
    }
}
