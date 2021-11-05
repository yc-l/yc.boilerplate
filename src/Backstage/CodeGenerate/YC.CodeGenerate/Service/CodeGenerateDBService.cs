using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Transactions;
using YC.Common;
using YC.Core;
using YC.Core.Attribute;
using YC.Common.ShareUtils;
using YC.CodeGenerate.Dto;

namespace YC.CodeGenerate
{
    public class CodeGenerateDBService : IDisposable
    {

        public IDbConnection connection;
        private RepositoryUtils.Dialect _dbtype;
        public string dbConfigFilePath = System.Environment.CurrentDirectory + "\\DefaultConfig.json";

        // public DatabaseConfig DatabaseConfig = CodeGenerateConfig<DatabaseConfig>.GetJsonList(dbConfigFilePath);
        public IDbConnection GetOpenConnection(string DbConnectionString = null, RepositoryUtils.Dialect dbtype = RepositoryUtils.Dialect.MySQL)
        {

            _dbtype = dbtype;

            if (_dbtype == RepositoryUtils.Dialect.MySQL)
            {
                connection = new MySqlConnection(DbConnectionString);

                RepositoryUtils.SetDialect(RepositoryUtils.Dialect.MySQL);
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
            var t = connection.State;
            connection.Open();
            return connection;
        }

        /// <summary>
        /// 初始化连接操作处理，处理异常操作状态
        /// </summary>
        public void SetInitConnection(string DbConnectionString = null, RepositoryUtils.Dialect dbtype = RepositoryUtils.Dialect.MySQL)
        {
            if (connection == null)
            {
                connection = GetOpenConnection(DbConnectionString, dbtype);
            }
            else if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            else if (connection.State != ConnectionState.Open)
            {
                connection = GetOpenConnection(DbConnectionString, dbtype);
            }


        }

        public bool CreateDB(out string msg, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            bool result = false;
            msg = "";
            try
            {
                DatabaseConfig databaseConfig = new DbConfig().DatabaseConfig;
                SetInitConnection(databaseConfig.DefaultMySqlConnectionString, databaseConfig.GetDbType());
                string dbName = databaseConfig.DefaultDBConnectionString.Split(';').AsEnumerable().Where(x => x.Contains("Database")).FirstOrDefault().Split('=')[1];

                using (connection)
                {

                    List<string> databaseList = connection.Query<string>("show databases;", null, transaction, true, commandTimeout).AsList<string>();
                    if ((databaseList.Any(x => x.Contains(dbName)) && databaseConfig.IsCoverExistDb) || (!databaseList.Any(x => x.Contains(dbName))))
                    {
                        //只有不存在数据库，或已经设置覆盖同名数据库的，才允许进行如下操作
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("drop database if exists `{0}`;create database `{0}` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci; ", dbName);
                        sb.AppendLine(" set collation_server = utf8_general_ci;");
                        sb.AppendLine(" set collation_connection = utf8_general_ci;");

                        int count = connection.Execute(sb.ToString(), null, transaction, commandTimeout);
                        result = true;
                        msg += "执行返回结果：" + count;
                        return result;
                    }
                    else
                    {
                        msg += "已经存在同名数据库，请修改将要创建的数据库名称！";
                        return result;
                    }

                }

            }
            catch (Exception ex)
            {

                msg += ex.Message;
            }
            return result;
        }

        public bool CreateTable(GenerateDbTableConfig config, out string msg,  bool isCovered = false, string AssemblyName = "YC.Model", IDbTransaction transaction = null, int? commandTimeout = null)
        {
            int i = 0;
            int modelCount = 0;
            bool resultState = false;
            msg = "";
            DatabaseConfig databaseConfig = new DbConfig().DatabaseConfig;
            string dbName = databaseConfig.DefaultDBConnectionString.Split(';').AsEnumerable().Where(x => x.Contains("Database")).FirstOrDefault().Split('=')[1];
            SetInitConnection(databaseConfig.DefaultDBConnectionString, databaseConfig.GetDbType());
            using (connection)
            {

                try
                {
                    List<Assembly> assemblyList = new List<Assembly>();
                    assemblyList.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(AssemblyName)));
                    List<Type> domainList = new List<Type>();

                    foreach (var assembly in assemblyList)
                    {
                        var list = assembly.GetExportedTypes();
                        foreach (var l in list)
                        {

                            if (l.IsDefined(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), false))
                            {

                                string tableName = ((System.ComponentModel.DataAnnotations.Schema.TableAttribute)Attribute.GetCustomAttribute(l, typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute))).Name;
                                if (config.GenerateDbTableEntityList.Exists(x => x == l.Name))
                                    domainList.Add(l);
                            }
                            //var t = ((System.ComponentModel.DataAnnotations.Schema.TableAttribute)Attribute.GetCustomAttribute(l, typeof
                        }

                        foreach (var d in domainList)
                        {
                            Type baseType = d;
                            int count = 0;
                            string fullName = baseType.FullName;//YC.Core.Domain.Order
                                                                // string name = baseType.Name;//Order
                            string tableName = ((System.ComponentModel.DataAnnotations.Schema.TableAttribute)Attribute.GetCustomAttribute(baseType, typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute))).Name;
                            modelCount = domainList.Count;
                            #region 每张表进行生成操作
                            try
                            {
                                StringBuilder sb = new StringBuilder();
                                if (isCovered)
                                {
                                    sb.AppendLine($"DROP TABLE IF EXISTS {tableName};");
                                }

                                sb.AppendLine($" CREATE TABLE IF NOT EXISTS `{tableName}`");
                                sb.AppendLine(" (");

                                IEnumerable<PropertyInfo> props = baseType.GetProperties();
                                string priKeySql = "";

                                ///字段数据类型映射
                                foreach (var p in props)
                                {
                                    try
                                    {
                                        var noppedAttr = p.GetCustomAttributes().Where(x => x.GetType().Name == typeof(NotMappedAttribute).Name);
                                        if (noppedAttr.Any())
                                        {//如果存在NotMaped 说明不映射直接跳过
                                            continue;
                                        }


                                        var columnAttr = p.GetCustomAttributes().Where(x => x.GetType().Name == typeof(ColumnAttribute).Name);
                                        if (columnAttr.Any())
                                        {
                                            var tempColumnAttr = (System.ComponentModel.DataAnnotations.Schema.ColumnAttribute)columnAttr.ToList()[0];
                                            sb.AppendFormat("`{0}`", tempColumnAttr.Name);
                                        }
                                        else
                                        {
                                            sb.AppendFormat("`{0}`", p.Name);//字段

                                        }

                                        #region 数据类型处理

                                        var peropertyTypeName = p.PropertyType.Name;
                                        if (peropertyTypeName.ToLower().Contains("Nullable`1".ToLower()))
                                        {
                                            peropertyTypeName = p.PropertyType.GetProperties()[1].PropertyType.Name;
                                        }

                                        peropertyTypeName = peropertyTypeName.ToLower();
                                        //可空类型没有做处理
                                        string[] typeArray = { "nullable`1","guid", "string", "long", "float", "double","int64", "int32", "datetime", "bool","boolean","byte",
                                        "uint","decimal","char"
                                    };
                                        var typeExist = typeArray.Any(x => x.Contains(peropertyTypeName));
                                        if (!typeExist)
                                        {
                                            throw new Exception("当前映射字段存在未知的数据类型！");
                                        }
                                        switch (peropertyTypeName)
                                        {
                                            case "string":

                                                var stringLengthAttr = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.StringLengthAttribute>();
                                                if (stringLengthAttr != null)
                                                {
                                                    sb.AppendFormat(" {0} ", "varchar(" + stringLengthAttr.MaximumLength + ")");
                                                }
                                                else
                                                {//没有标识 stringLength，默认赋予100
                                                    sb.AppendFormat(" {0} ", "varchar(100)");
                                                }

                                                break;

                                            case "guid":
                                                if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.StringLengthAttribute), false))
                                                {
                                                    var stringLengthAttr1 = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.StringLengthAttribute>();
                                                    if (stringLengthAttr1 != null)
                                                    {
                                                        sb.AppendFormat(" {0} ", "varchar(" + stringLengthAttr1.MaximumLength + ")");
                                                    }
                                                    else
                                                    {//没有标识 stringLength，默认赋予100
                                                        sb.AppendFormat(" {0} ", "varchar(68)");
                                                    }
                                                }
                                                else
                                                {
                                                    sb.AppendFormat(" {0} ", "varchar(68)");
                                                }
                                                break;

                                            case "byte": sb.AppendFormat(" {0} ", "bit"); break;
                                            case "long": sb.AppendFormat(" {0} ", "bigint"); break;
                                            case "float": sb.AppendFormat(" {0} ", "float"); break;
                                            case "double": sb.AppendFormat(" {0} ", "double"); break;
                                            case "int32": sb.AppendFormat(" {0} ", "int"); break;
                                            case "int64": sb.AppendFormat(" {0} ", "bigint"); break;
                                            case "datetime": sb.AppendFormat(" {0} ", "datetime"); break;
                                            case "bool": sb.AppendFormat(" {0} ", "bit"); break;
                                            case "boolean": sb.AppendFormat(" {0} ", "bit"); break;
                                            case "char": sb.AppendFormat(" {0} ", "char"); break;
                                            //case "byte": sb.AppendFormat(" {0} ", "binary"); break;
                                            //case "byte[]": sb.AppendFormat(" {0} ", "varbinary"); break;
                                            case "decimal":
                                                var decimalPrecisionAttribute = p.GetCustomAttribute<DecimalPrecisionAttribute>();
                                                if (decimalPrecisionAttribute != null)
                                                {
                                                    sb.AppendFormat(" {0} ", "decimal(" + decimalPrecisionAttribute.Precision + "," + decimalPrecisionAttribute.Scale + ")");
                                                }
                                                break;
                                        }
                                        #endregion
                                        #region 主键处理
                                        if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false))
                                        {//说明是主键
                                         // `runoob_id` INT UNSIGNED AUTO_INCREMENT,

                                            sb.AppendFormat(" PRIMARY KEY ");

                                            //if (tableName == "Users")
                                            //{
                                            //    sb.AppendFormat(" PRIMARY KEY aaaa");
                                            //    // throw new Exception("测试回滚！");
                                            //}
                                        }
                                        #endregion
                                        #region 非空处理
                                        var requireAttr = p.GetCustomAttributes().Where(x => x.GetType().Name == typeof(RequiredAttribute).Name);

                                        if (requireAttr.Any())
                                        {//存在 Required，说明要非空
                                            sb.AppendFormat(" {0} ", "NOT NULL");
                                        }
                                        #endregion
                                        #region 自增处理
                                        if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute), false))
                                        {
                                            var databaseGeneratedAttr = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute>();
                                            if (databaseGeneratedAttr != null)
                                            {

                                                //判断自增
                                                if (databaseGeneratedAttr.DatabaseGeneratedOption == System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
                                                {
                                                    sb.AppendFormat(" {0} ", "AUTO_INCREMENT");
                                                }

                                            }
                                        }

                                        #endregion
                                        #region 备注处理

                                        if (p.IsDefined(typeof(System.ComponentModel.DisplayNameAttribute), false))
                                        {
                                            var displayNameAttr = p.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>();
                                            sb.AppendFormat("COMMENT '{0}'", displayNameAttr.DisplayName);

                                        }
                                        else if (p.IsDefined(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false))
                                        {
                                            var displayAttr = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>();
                                            sb.AppendFormat("COMMENT '{0}'", displayAttr.Name);
                                        }


                                        #endregion

                                        sb.AppendLine(" ");
                                        sb.Append(",");

                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                                //删除最后的“,”
                                if (sb[sb.Length - 1].Equals(','))
                                {
                                    sb = sb.Remove(sb.Length - 1, 1);
                                }


                                //拼接主键
                                sb.AppendLine(priKeySql);

                                sb.Append(")AUTO_INCREMENT = 1,ENGINE = InnoDB,DEFAULT CHARSET=utf8;");
                                string resultSql = sb.ToString();



                                count = connection.Execute(resultSql, null, transaction, commandTimeout);

                                msg += "表：" + tableName + ",生成成功! 执行" + count + ",\r\n";
                                i++;

                            }
                            catch (Exception ex)
                            {

                                msg += "表：" + tableName + ",生成失败！执行" + count + "," + ex.ToString() + "\r\n";
                                throw ex;
                            }
                            #endregion
                        }
                    }
                    resultState = true;

                }
                catch (Exception ex)
                {


                    msg += ex.ToString();
                }
            }

            msg += $"总共{ modelCount}个实体，共有{i}张表生成.";
            return resultState;

        }



        //是否回收完毕
        bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~CodeGenerateDBService()
        {
            Dispose(false);
        }

        //这里的参数表示示是否需要释放那些实现IDisposable接口的托管对象
        //这里的参数表示示是否需要释放那些实现IDisposable接口的托管对象
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return; //如果已经被回收，就中断执行
            if (disposing)
            {
                //TODO:释放那些实现IDisposable接口的托管对象
            }
            //TODO:释放非托管资源，设置对象为null
            _disposed = true;
        }
    }
}
