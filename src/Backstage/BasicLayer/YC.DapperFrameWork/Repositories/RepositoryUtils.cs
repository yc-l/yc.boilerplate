using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;
using YC.Core;

namespace Dapper
{
    /// <summary>
    /// Main class for Dapper.SimpleCRUD extensions
    /// </summary>
    public static partial class RepositoryUtils
    {

        static RepositoryUtils()
        {
            SetDialect(_dialect);
        }

        private static Dialect _dialect = Dialect.MySQL;
        private static string _encapsulation;
        public static string _getIdentitySql;
        private static string _getPagedListSql;

        private static readonly ConcurrentDictionary<Type, string> TableNames = new ConcurrentDictionary<Type, string>();
        private static readonly ConcurrentDictionary<string, string> ColumnNames = new ConcurrentDictionary<string, string>();

        private static readonly ConcurrentDictionary<string, string> StringBuilderCacheDict = new ConcurrentDictionary<string, string>();
        private static bool StringBuilderCacheEnabled = true;

        private static ITableNameResolver _tableNameResolver = new TableNameResolver();
        private static IColumnNameResolver _columnNameResolver = new ColumnNameResolver();

        /// <summary>
        /// Append a Cached version of a strinbBuilderAction result based on a cacheKey
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="cacheKey"></param>
        /// <param name="stringBuilderAction"></param>
        private static void StringBuilderCache(StringBuilder sb, string cacheKey, Action<StringBuilder> stringBuilderAction)
        {
            if (StringBuilderCacheEnabled && StringBuilderCacheDict.TryGetValue(cacheKey, out string value))
            {
                sb.Append(value);
                return;
            }

            StringBuilder newSb = new StringBuilder();
            stringBuilderAction(newSb);
            value = newSb.ToString();
            StringBuilderCacheDict.AddOrUpdate(cacheKey, value, (t, v) => value);
            sb.Append(value);
        }

        /// <summary>
        /// Returns the current dialect name
        /// </summary>
        /// <returns></returns>
        public static string GetDialect()
        {
            return _dialect.ToString();
        }

        /// <summary>
        /// Sets the database dialect 
        /// </summary>
        /// <param name="dialect"></param>
        public static void SetDialect(Dialect dialect)
        {
            switch (dialect)
            {
                case Dialect.PostgreSQL:
                    _dialect = Dialect.PostgreSQL;
                    _encapsulation = "\"{0}\"";
                    _getIdentitySql = string.Format("SELECT LASTVAL() AS id");
                    _getPagedListSql = "Select {SelectColumns} from {TableName} {WhereClause} Order By {OrderBy} LIMIT {RowsPerPage} OFFSET (({PageNumber}-1) * {RowsPerPage})";
                    break;
                case Dialect.SQLite:
                    _dialect = Dialect.SQLite;
                    _encapsulation = "\"{0}\"";
                    _getIdentitySql = string.Format("SELECT LAST_INSERT_ROWID() AS id");
                    _getPagedListSql = "Select {SelectColumns} from {TableName} {WhereClause} Order By {OrderBy} LIMIT {RowsPerPage} OFFSET (({PageNumber}-1) * {RowsPerPage})";
                    break;
                case Dialect.MySQL:
                    _dialect = Dialect.MySQL;
                    _encapsulation = "`{0}`";
                    _getIdentitySql = string.Format("SELECT LAST_INSERT_ID() AS id");
                    _getPagedListSql = "Select {SelectColumns} from {TableName} {WhereClause} Order By {OrderBy} LIMIT {Offset},{RowsPerPage}";
                    break;
                default:
                    _dialect = Dialect.SQLServer;
                    _encapsulation = "[{0}]";
                    _getIdentitySql = string.Format("SELECT CAST(SCOPE_IDENTITY()  AS BIGINT) AS [id]");
                    _getPagedListSql = "SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {OrderBy}) AS PagedNumber, {SelectColumns} FROM {TableName} {WhereClause}) AS u WHERE PagedNumber BETWEEN (({PageNumber}-1) * {RowsPerPage} + 1) AND ({PageNumber} * {RowsPerPage})";
                    break;
            }
        }

        #region 创建表，待完善
        /// <summary>
        /// 创建表
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="entityToInsertList"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static void CreateTable<TKey, TEntity>(this IDbConnection connection, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            int count = 0;
            try
            {
                var baseType = typeof(TEntity);

                StringBuilder sb = new StringBuilder();

                #region 1、获取指定 类的Attribute
                IsMappedToCreateAttribute att =
                    (IsMappedToCreateAttribute)Attribute.GetCustomAttribute(baseType, typeof(IsMappedToCreateAttribute));
                string tableName = "";
                if (att.IsCreate == IsCreate.Yes)//如果标签是Yes
                {
                    string fullName = baseType.FullName;//YC.Core.Domain.Order
                                                        // string name = baseType.Name;//Order
                    tableName = ((System.ComponentModel.DataAnnotations.Schema.TableAttribute)Attribute.GetCustomAttribute(baseType, typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute))).Name;
                }
                #endregion

                sb.AppendFormat(" CREATE TABLE IF NOT EXISTS `{0}`", tableName);
                sb.AppendLine(" (");

                IEnumerable<PropertyInfo> props = baseType.GetProperties();
                string priKeySql = "";
                foreach (var p in props)
                {

                    var noppedAttr = p.GetCustomAttributes().Where(x => x.GetType().Name == typeof(NotMappedAttribute).Name);
                    if (noppedAttr.Any())
                    {//如果存在NotMaped 说明不映射直接跳过
                        continue;
                    }

                    sb.AppendFormat("`{0}`", p.Name);

                    #region 数据类型处理
                    var peropertyTypeName = p.PropertyType.Name.ToLower();

                    string[] typeArray = { "guid", "string", "long", "float", "double", "int32", "datetime" };
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
                            var stringLengthAttr1 = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.StringLengthAttribute>();
                            if (stringLengthAttr1 != null)
                            {
                                sb.AppendFormat(" {0} ", "varchar(" + stringLengthAttr1.MaximumLength + ")");
                            }
                            else
                            {//没有标识 stringLength，默认赋予100
                                sb.AppendFormat(" {0} ", "varchar(100)");
                            }
                            break;

                        case "long": sb.AppendFormat(" {0} ", "long"); break;
                        case "float": sb.AppendFormat(" {0} ", "float"); break;
                        case "double": sb.AppendFormat(" {0} ", "double"); break;
                        case "int32": sb.AppendFormat(" {0} ", "int"); break;
                        case "datetime":
                            sb.AppendFormat(" {0} ", "datetime"); break;

                            // case "decimal": sb.AppendFormat(" {0} ", "double"); break;
                    }
                    #endregion
                    #region 主键处理
                    var keyAttr = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>();
                    if (keyAttr != null)
                    {//说明是主键
                     // `runoob_id` INT UNSIGNED AUTO_INCREMENT,

                        sb.AppendFormat(" PRIMARY KEY ");
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
                    var databaseGeneratedAttr = p.GetCustomAttribute<System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute>();
                    if (databaseGeneratedAttr != null)
                    {

                        //判断自增
                        if (databaseGeneratedAttr.DatabaseGeneratedOption == System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
                        {
                            sb.AppendFormat(" {0} ", "AUTO_INCREMENT");
                        }

                    }
                    #endregion
                    #region 备注处理
                    var displayNameAttr = p.GetCustomAttribute<System.ComponentModel.DisplayNameAttribute>();

                    sb.AppendFormat("COMMENT '{0}'", displayNameAttr.DisplayName);
                    #endregion

                    sb.AppendLine(" ");
                    sb.Append(",");


                }
                //删除最后的“,”
                if (sb[sb.Length - 1].Equals(','))
                {
                    sb = sb.Remove(sb.Length - 1, 1);
                }


                //拼接主键
                sb.AppendLine(priKeySql);

                sb.Append(")AUTO_INCREMENT = 10000,ENGINE = InnoDB,DEFAULT CHARSET=utf8; ");
                string resultSql = sb.ToString();

                count = connection.Execute(resultSql, null, transaction, commandTimeout);
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        #endregion

        /// <summary>
        /// Sets the table name resolver
        /// </summary>
        /// <param name="resolver">The resolver to use when requesting the format of a table name</param>
        public static void SetTableNameResolver(ITableNameResolver resolver)
        {
            _tableNameResolver = resolver;
        }

        /// <summary>
        /// Sets the column name resolver
        /// </summary>
        /// <param name="resolver">The resolver to use when requesting the format of a column name</param>
        public static void SetColumnNameResolver(IColumnNameResolver resolver)
        {
            _columnNameResolver = resolver;
        }

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>By default filters on the Id column</para>
        /// <para>-Id column name can be overridden by adding an attribute on your primary key property [Key]</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a single entity by a single id from table T</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Returns a single entity by a single id from table T.</returns>
        public static T Get<T>(this IDbConnection connection, object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();

            if (!idProps.Any())
                throw new ArgumentException("Get<T> only supports an entity with a [Key] or Id property");

            var name = GetTableName(currenttype);
            var sb = new StringBuilder();
            sb.Append("Select ");
            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties<T>().ToArray());
            sb.AppendFormat(" from {0} where ", name);

            for (var i = 0; i < idProps.Count; i++)
            {
                if (i > 0)
                    sb.Append(" and ");
                sb.AppendFormat("{0} = @{1}", GetColumnName(idProps[i]), idProps[i].Name);
            }
            //多租户
            // sb.Append(" and tenantId=xx ");
            var dynParms = new DynamicParameters();
            if (idProps.Count == 1)
                dynParms.Add("@" + idProps.First().Name, id);
            else
            {
                foreach (var prop in idProps)
                    dynParms.Add("@" + prop.Name, id.GetType().GetProperty(prop.Name).GetValue(id, null));
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("Get<{0}>: {1} with Id: {2}", currenttype, sb, id));

            return connection.Query<T>(sb.ToString(), dynParms, transaction, true, commandTimeout).FirstOrDefault();
        }

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>whereConditions is an anonymous type to filter the results ex: new {Category = 1, SubCategory=2}</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a list of entities that match where conditions</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="whereConditions"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Gets a list of entities with optional exact match where conditions</returns>
        public static IEnumerable<T> GetList<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select ");
            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties<T>().ToArray());
            sb.AppendFormat(" from {0}", name);

            if (whereprops.Any())//有where
            {
                sb.Append(" where ");
                //如果多租户
                //sb.Append(" tenantId=xx ");
                //sb.Append(" and ");

                BuildWhere<T>(sb, whereprops, whereConditions);
            }
            else
            {//没有条件
                //sb.Append(" where ");
                ////如果多租户
                //sb.Append(" tenantId=xx ");
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("GetList<{0}>: {1}", currenttype, sb));

            return connection.Query<T>(sb.ToString(), whereConditions, transaction, true, commandTimeout);
        }

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>conditions is an SQL where clause and/or order by clause ex: "where name='bob'" or "where age>=@Age"</para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a list of entities that match where conditions</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Gets a list of entities with optional SQL where conditions</returns>
        public static IEnumerable<T> GetList<T>(this IDbConnection connection, string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            sb.Append("Select ");
            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties<T>().ToArray());
            sb.AppendFormat(" from {0}", name);

            sb.Append(" " + conditions);
            //多租户
            // sb.Append(" and tenantId=xx ");
            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("GetList<{0}>: {1}", currenttype, sb));

            return connection.Query<T>(sb.ToString(), parameters, transaction, true, commandTimeout);
        }

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Returns a list of all entities</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <returns>Gets a list of all entities</returns>
        public static IEnumerable<T> GetList<T>(this IDbConnection connection)
        {
            return connection.GetList<T>(new { });
        }

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age" - not required </para>
        /// <para>orderby is a column or list of columns to order by ex: "lastname, age desc" - not required - default is by primary key</para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns a list of entities that match where conditions</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="pageNumber"></param>
        /// <param name="rowsPerPage"></param>
        /// <param name="conditions"></param>
        /// <param name="orderby"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Gets a paged list of entities with optional exact match where conditions</returns>
        public static IEnumerable<T> GetListPaged<T>(this IDbConnection connection, int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (string.IsNullOrEmpty(_getPagedListSql))
                throw new Exception("GetListPage is not supported with the current SQL Dialect");

            if (pageNumber < 1)
                throw new Exception("Page must be greater than 0");

            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();
            if (!idProps.Any())
                throw new ArgumentException("Entity must have at least one [Key] property");

            var name = GetTableName(currenttype);
            var sb = new StringBuilder();
            var query = _getPagedListSql;
            if (string.IsNullOrEmpty(orderby))
            {
                orderby = GetColumnName(idProps.First());
            }

            //create a new empty instance of the type to get the base properties
            BuildSelect(sb, GetScaffoldableProperties<T>().ToArray());
            query = query.Replace("{SelectColumns}", sb.ToString());
            query = query.Replace("{TableName}", name);
            query = query.Replace("{PageNumber}", pageNumber.ToString());
            query = query.Replace("{RowsPerPage}", rowsPerPage.ToString());
            query = query.Replace("{OrderBy}", orderby);

            //多租户
            //conditions += "and tenantId=xx ";
            query = query.Replace("{WhereClause}", conditions);
            query = query.Replace("{Offset}", ((pageNumber - 1) * rowsPerPage).ToString());

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("GetListPaged<{0}>: {1}", currenttype, query));

            return connection.Query<T>(query, parameters, transaction, true, commandTimeout);
        }

        /// <summary>
        /// <para>Inserts a row into the database</para>
        /// <para>By default inserts into the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Insert filters out Id column and any columns with the [Key] attribute</para>
        /// <para>Properties marked with attribute [Editable(false)] and complex types are ignored</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns the ID (primary key) of the newly inserted record if it is identity using the int? type, otherwise null</para>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entityToInsert"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The ID (primary key) of the newly inserted record if it is identity using the int? type, otherwise null</returns>
        public static int? Insert<TEntity>(this IDbConnection connection, TEntity entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return Insert<int?, TEntity>(connection, entityToInsert, transaction, commandTimeout);
        }

        /// <summary>
        /// <para>Inserts a row into the database, using ONLY the properties defined by TEntity</para>
        /// <para>By default inserts into the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Insert filters out Id column and any columns with the [Key] attribute</para>
        /// <para>Properties marked with attribute [Editable(false)] and complex types are ignored</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns the ID (primary key) of the newly inserted record if it is identity using the defined type, otherwise null</para>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entityToInsert"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The ID (primary key) of the newly inserted record if it is identity using the defined type, otherwise null</returns>
        public static TKey Insert<TKey, TEntity>(this IDbConnection connection, TEntity entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {

            var idProps = GetIdProperties(entityToInsert).ToList();

            if (!idProps.Any())
                throw new ArgumentException("Insert<T> only supports an entity with a [Key] or Id property");

            var keyHasPredefinedValue = false;
            var baseType = typeof(TKey);
            var underlyingType = Nullable.GetUnderlyingType(baseType);
            var keytype = underlyingType ?? baseType;
            if (keytype != typeof(int) && keytype != typeof(uint) && keytype != typeof(long) && keytype != typeof(ulong) && keytype != typeof(short) && keytype != typeof(ushort) && keytype != typeof(Guid) && keytype != typeof(string))
            {
                throw new Exception("Invalid return type");
            }

            var name = GetTableName(entityToInsert);
            var sb = new StringBuilder();
            sb.AppendFormat("insert into {0}", name);
            sb.Append(" (");
            BuildInsertParameters<TEntity>(sb);
            sb.Append(") ");
            sb.Append("values");
            sb.Append(" (");
            BuildInsertValues<TEntity>(sb);
            sb.Append(")");

            if (keytype == typeof(Guid))
            {
                var guidvalue = (Guid)idProps.First().GetValue(entityToInsert, null);
                if (guidvalue == Guid.Empty)
                {
                    var newguid = SequentialGuid();
                    idProps.First().SetValue(entityToInsert, newguid, null);
                }
                else
                {
                    keyHasPredefinedValue = true;
                }
                sb.Append(";select '" + idProps.First().GetValue(entityToInsert, null) + "' as id");
            }

            if (keytype == typeof(string))
            {
                var guidvalue = (string)idProps.First().GetValue(entityToInsert, null);
                if (guidvalue == string.Empty)
                {
                    var newguid = SequentialGuid();
                    idProps.First().SetValue(entityToInsert, newguid, null);
                }
                else
                {
                    keyHasPredefinedValue = true;
                }
                sb.Append(";select '" + idProps.First().GetValue(entityToInsert, null) + "' as id");
            }



            if ((keytype == typeof(int) || keytype == typeof(long)) && Convert.ToInt64(idProps.First().GetValue(entityToInsert, null)) == 0)
            {
                sb.Append(";" + _getIdentitySql);
            }
            else
            {
                keyHasPredefinedValue = true;
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("Insert: {0}", sb));

            var r = connection.Query(sb.ToString(), entityToInsert, transaction, true, commandTimeout);

            if (keytype == typeof(Guid) || keyHasPredefinedValue)
            {
                return (TKey)idProps.First().GetValue(entityToInsert, null);
            }
            return (TKey)r.First().id;
        }


        /// <summary>
        /// 批量插入数据，一次性
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="connection"></param>
        /// <param name="entityToInsertList"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>

        public static int InsertList<TKey, TEntity>(this IDbConnection connection, List<TEntity> entityToInsertList, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (entityToInsertList == null)
            {
                throw new ArgumentException("批量插入数据集为空！");
            }

            if (entityToInsertList.Count == 0)
            {
                throw new ArgumentException("批量插入数据集总条数为0！");
            }

            var entityToInsert = entityToInsertList[0];

            var idProps = GetIdProperties(entityToInsertList[0]).ToList();

            if (!idProps.Any())
                throw new ArgumentException("Insert<T> only supports an entity with a [Key] or Id property");

            var keyHasPredefinedValue = false;
            var baseType = typeof(TKey);
            var underlyingType = Nullable.GetUnderlyingType(baseType);
            var keytype = underlyingType ?? baseType;
            if (keytype != typeof(int) && keytype != typeof(uint) && keytype != typeof(long) && keytype != typeof(ulong) && keytype != typeof(short) && keytype != typeof(ushort) && keytype != typeof(Guid) && keytype != typeof(string))
            {
                throw new Exception("Invalid return type");
            }

            var name = GetTableName(entityToInsertList[0]);
            var sb = new StringBuilder();
            sb.AppendFormat("insert into {0}", name);
            sb.Append(" (");
            BuildInsertParameters<TEntity>(sb);
            sb.Append(") ");

            sb.Append("values");
            sb.Append(" (");
            BuildInsertValues<TEntity>(sb);
            sb.Append(")");


            for (int i = 0; i < entityToInsertList.Count; i++)
            {

                if (keytype == typeof(Guid))
                {
                    var guidvalue = (Guid)idProps.First().GetValue(entityToInsertList[i], null);
                    if (guidvalue == Guid.Empty)
                    {
                        var newguid = SequentialGuid();
                        idProps.First().SetValue(entityToInsertList[i], newguid, null);
                    }
                    else
                    {
                        keyHasPredefinedValue = true;
                    }

                }
                else if (keytype == typeof(string))
                {
                    var guidvalue = (string)idProps.First().GetValue(entityToInsertList[i], null);
                    if (string.IsNullOrEmpty(guidvalue))
                    {
                        var newguid = SequentialGuid().ToString();
                        idProps.First().SetValue(entityToInsertList[i], newguid, null);
                    }
                    else
                    {
                        keyHasPredefinedValue = true;
                    }

                }


            }
            int count = 0;
            //IDbTransaction trans = connection.BeginTransaction();
            //transaction = transaction ?? trans;//无论如何这里操作都要提供事务，这样会insert 效率会提高很多，如果没有事务，dapper 插入数据是还是一条条插入


            count = connection.Execute(sb.ToString(), entityToInsertList, transaction, commandTimeout, CommandType.Text);

            return count;
        }


        /// <summary>
        /// <para>Updates a record or records in the database with only the properties of TEntity</para>
        /// <para>By default updates records in the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Updates records where the Id property and properties with the [Key] attribute match those in the database.</para>
        /// <para>Properties marked with attribute [Editable(false)] and complex types are ignored</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns number of rows affected</para>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of affected records</returns>
        public static int Update<TEntity>(this IDbConnection connection, TEntity entityToUpdate, string whereCondition = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var masterSb = new StringBuilder();
            StringBuilderCache(masterSb, $"{typeof(TEntity).FullName}_Update", sb =>
            {
                var idProps = GetIdProperties(entityToUpdate).ToList();

                if (!idProps.Any())
                    throw new ArgumentException("Entity must have at least one [Key] or Id property");

                var name = GetTableName(entityToUpdate);

                sb.AppendFormat("update {0}", name);

                sb.AppendFormat(" set ");
                BuildUpdateSet(entityToUpdate, sb);
                sb.Append(" where ");

                if (whereCondition != null)
                {
                    sb.Append(whereCondition + " and ");
                    //多租户
                    // sb.Append(" tenantId=xx and  ");
                }
                else
                {
                    //多租户
                    //sb.Append(" where ");
                    // sb.Append(" tenantId=xx ");
                }

                BuildWhere<TEntity>(sb, idProps, entityToUpdate);



                if (Debugger.IsAttached)
                    Trace.WriteLine(String.Format("Update: {0}", sb));
            });
            return connection.Execute(masterSb.ToString(), entityToUpdate, transaction, commandTimeout);
        }

        /// <summary>
        /// <para>Deletes a record or records in the database that match the object passed in</para>
        /// <para>-By default deletes records in the table matching the class name</para>
        /// <para>Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>Returns the number of records affected</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="entityToDelete"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected</returns>
        public static int Delete<T>(this IDbConnection connection, T entityToDelete, string whereCondition = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var masterSb = new StringBuilder();
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_Delete", sb =>
            {

                var idProps = GetIdProperties(entityToDelete).ToList();

                if (!idProps.Any())
                    throw new ArgumentException("Entity must have at least one [Key] or Id property");

                var name = GetTableName(entityToDelete);

                sb.AppendFormat("delete from {0}", name);

                sb.Append(" where ");

                if (whereCondition != null)
                {
                    sb.Append(whereCondition + " and ");
                    //多租户
                    // sb.Append(" tenantId=xx and ");

                }
                else
                {
                    //多租户
                    // sb.Append(" tenantId=xx ");
                }

                BuildWhere<T>(sb, idProps, entityToDelete);

                if (Debugger.IsAttached)
                    Trace.WriteLine(String.Format("Delete: {0}", sb));
            });
            return connection.Execute(masterSb.ToString(), entityToDelete, transaction, commandTimeout);
        }

        /// <summary>
        /// <para>Deletes a record or records in the database by ID</para>
        /// <para>By default deletes records in the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Deletes records where the Id property and properties with the [Key] attribute match those in the database</para>
        /// <para>The number of records affected</para>
        /// <para>Supports transaction and command timeout</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected</returns>
        public static int Delete<T>(this IDbConnection connection, object id, string whereCondition = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var idProps = GetIdProperties(currenttype).ToList();


            if (!idProps.Any())
                throw new ArgumentException("Delete<T> only supports an entity with a [Key] or Id property");

            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            sb.AppendFormat("Delete from {0} where ", name);
            if (whereCondition != null)
            {
                sb.Append(whereCondition + " and ");
                //多租户
                // sb.Append(" tenantId=xx and ");
            }
            else
            {
                //多租户
                // sb.Append(" and tenantId=xx ");
            }
            for (var i = 0; i < idProps.Count; i++)
            {
                if (i > 0)
                    sb.Append(" and ");
                sb.AppendFormat("{0} = @{1}", GetColumnName(idProps[i]), idProps[i].Name);
            }

            var dynParms = new DynamicParameters();
            if (idProps.Count == 1)
                dynParms.Add("@" + idProps.First().Name, id);
            else
            {
                foreach (var prop in idProps)
                    dynParms.Add("@" + prop.Name, id.GetType().GetProperty(prop.Name).GetValue(id, null));
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("Delete<{0}> {1}", currenttype, sb));

            return connection.Execute(sb.ToString(), dynParms, transaction, commandTimeout);
        }

        /// <summary>
        /// <para>Deletes a list of records in the database</para>
        /// <para>By default deletes records in the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Deletes records where that match the where clause</para>
        /// <para>whereConditions is an anonymous type to filter the results ex: new {Category = 1, SubCategory=2}</para>
        /// <para>The number of records affected</para>
        /// <para>Supports transaction and command timeout</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="whereConditions"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected</returns>
        public static int DeleteList<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var masterSb = new StringBuilder();
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_DeleteWhere{whereConditions?.GetType()?.FullName}", sb =>
            {
                var currenttype = typeof(T);
                var name = GetTableName(currenttype);

                var whereprops = GetAllProperties(whereConditions).ToArray();
                sb.AppendFormat("Delete from {0}", name);
                if (whereprops.Any())
                {
                    sb.Append(" where ");
                    //多租户
                    // sb.Append(" tenantId=xx and  ");
                    BuildWhere<T>(sb, whereprops);
                }
                else
                {

                    //多租户
                    //sb.Append(" where ");
                    // sb.Append(" tenantId=xx   ");
                }

                if (Debugger.IsAttached)
                    Trace.WriteLine(String.Format("DeleteList<{0}> {1}", currenttype, sb));
            });
            return connection.Execute(masterSb.ToString(), whereConditions, transaction, commandTimeout);
        }

        /// <summary>
        /// <para>Deletes a list of records in the database</para>
        /// <para>By default deletes records in the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Deletes records where that match the where clause</para>
        /// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age"</para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// <para>Supports transaction and command timeout</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of records affected</returns>
        public static int DeleteListByParams<T>(this IDbConnection connection, string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var masterSb = new StringBuilder();

            StringBuilderCache(masterSb, $"{typeof(T).FullName}_DeleteWhere{conditions}", sb =>
            {
                if (string.IsNullOrEmpty(conditions))
                    throw new ArgumentException("DeleteList<T> requires a where clause");
                if (!conditions.ToLower().Contains("where"))
                    throw new ArgumentException("DeleteList<T> requires a where clause and must contain the WHERE keyword");

                var currenttype = typeof(T);
                var name = GetTableName(currenttype);

                sb.AppendFormat("Delete from {0}", name);
                sb.Append(" " + conditions);
                //多租户
                // sb.Append(" and tenantId=xx ");

                if (Debugger.IsAttached)
                    Trace.WriteLine(String.Format("DeleteList<{0}> {1}", currenttype, sb));
            });
            return connection.Execute(masterSb.ToString(), parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Returns a number of records entity by a single id from table T</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age" - not required </para>
        /// <para>parameters is an anonymous type to pass in named parameter values: new { Age = 15 }</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="conditions"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Returns a count of records.</returns>
        public static int RecordCount<T>(this IDbConnection connection, string conditions = "", object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var name = GetTableName(currenttype);
            var sb = new StringBuilder();
            sb.Append("Select count(1)");
            sb.AppendFormat(" from {0}", name);
            sb.Append(" " + conditions);

            //多租户
            // sb.Append(" tenantId=xx   ");
            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("RecordCount<{0}>: {1}", currenttype, sb));

            return connection.ExecuteScalar<int>(sb.ToString(), parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// <para>By default queries the table matching the class name</para>
        /// <para>-Table name can be overridden by adding an attribute on your class [Table("YourTableName")]</para>
        /// <para>Returns a number of records entity by a single id from table T</para>
        /// <para>Supports transaction and command timeout</para>
        /// <para>whereConditions is an anonymous type to filter the results ex: new {Category = 1, SubCategory=2}</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="whereConditions"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Returns a count of records.</returns>
        public static int RecordCount<T>(this IDbConnection connection, object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var currenttype = typeof(T);
            var name = GetTableName(currenttype);

            var sb = new StringBuilder();
            var whereprops = GetAllProperties(whereConditions).ToArray();
            sb.Append("Select count(1)");
            sb.AppendFormat(" from {0}", name);
            if (whereprops.Any())
            {
                sb.Append(" where ");
                // sb.Append(" tenantId=xx and  ");
                BuildWhere<T>(sb, whereprops);
            }
            else
            {
                //多租户
                //sb.Append(" where ");
                // sb.Append(" tenantId=xx ");
            }

            if (Debugger.IsAttached)
                Trace.WriteLine(String.Format("RecordCount<{0}>: {1}", currenttype, sb));

            return connection.ExecuteScalar<int>(sb.ToString(), whereConditions, transaction, commandTimeout);
        }

        //build update statement based on list on an entity
        private static void BuildUpdateSet<T>(T entityToUpdate, StringBuilder masterSb)
        {
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_BuildUpdateSet", sb =>
            {
                var nonIdProps = GetUpdateableProperties(entityToUpdate).ToArray();

                for (var i = 0; i < nonIdProps.Length; i++)
                {
                    var property = nonIdProps[i];

                    sb.AppendFormat("{0} = @{1}", GetColumnName(property), property.Name);
                    if (i < nonIdProps.Length - 1)
                        sb.AppendFormat(", ");
                }
            });
        }

        //build select clause based on list of properties skipping ones with the IgnoreSelect and NotMapped attribute
        private static void BuildSelect(StringBuilder masterSb, IEnumerable<PropertyInfo> props)
        {
            StringBuilderCache(masterSb, $"{props.CacheKey()}_BuildSelect", sb =>
            {
                var propertyInfos = props as IList<PropertyInfo> ?? props.ToList();
                var addedAny = false;
                for (var i = 0; i < propertyInfos.Count(); i++)
                {
                    var property = propertyInfos.ElementAt(i);

                    if (property.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(IgnoreSelectAttribute).Name || attr.GetType().Name == typeof(NotMappedAttribute).Name)) continue;

                    if (addedAny)
                        sb.Append(",");
                    sb.Append(GetColumnName(property));
                    //if there is a custom column name add an "as customcolumnname" to the item so it maps properly
                    if (property.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name) != null)
                        sb.Append(" as " + Encapsulate(property.Name));
                    addedAny = true;
                }
            });
        }

        private static void BuildWhere<TEntity>(StringBuilder sb, IEnumerable<PropertyInfo> idProps, object whereConditions = null)
        {
            var propertyInfos = idProps.ToArray();
            for (var i = 0; i < propertyInfos.Count(); i++)
            {
                var useIsNull = false;

                //match up generic properties to source entity properties to allow fetching of the column attribute
                //the anonymous object used for search doesn't have the custom attributes attached to them so this allows us to build the correct where clause
                //by converting the model type to the database column name via the column attribute
                var propertyToUse = propertyInfos.ElementAt(i);
                var sourceProperties = GetScaffoldableProperties<TEntity>().ToArray();
                for (var x = 0; x < sourceProperties.Count(); x++)
                {
                    if (sourceProperties.ElementAt(x).Name == propertyToUse.Name)
                    {
                        if (whereConditions != null && propertyToUse.CanRead && (propertyToUse.GetValue(whereConditions, null) == null || propertyToUse.GetValue(whereConditions, null) == DBNull.Value))
                        {
                            useIsNull = true;
                        }
                        propertyToUse = sourceProperties.ElementAt(x);
                        break;
                    }
                }
                sb.AppendFormat(
                    useIsNull ? "{0} is null" : "{0} = @{1}",
                    GetColumnName(propertyToUse),
                    propertyToUse.Name);

                if (i < propertyInfos.Count() - 1)
                    sb.AppendFormat(" and ");
            }
        }

        //build insert values which include all properties in the class that are:
        //Not named Id
        //Not marked with the Editable(false) attribute
        //Not marked with the [Key] attribute (without required attribute)
        //Not marked with [IgnoreInsert]
        //Not marked with [NotMapped]
        public static void BuildInsertValues<T>(StringBuilder masterSb)
        {
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_BuildInsertValues", sb =>
            {

                var props = GetScaffoldableProperties<T>().ToArray();
                for (var i = 0; i < props.Count(); i++)
                {
                    var property = props.ElementAt(i);
                    if (property.PropertyType != typeof(Guid) && property.PropertyType != typeof(string)
                          && property.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(KeyAttribute).Name)
                          && property.GetCustomAttributes(true).All(attr => attr.GetType().Name != typeof(RequiredAttribute).Name))
                        continue;
                    if (property.GetCustomAttributes(true).Any(attr =>
                        attr.GetType().Name == typeof(IgnoreInsertAttribute).Name ||
                        attr.GetType().Name == typeof(NotMappedAttribute).Name ||
                        attr.GetType().Name == typeof(ReadOnlyAttribute).Name && IsReadOnly(property))
                    ) continue;

                    //如果满足属性 名称为Id，且非必须，属性类别不是guid，不是string，直接跳过，不生成该字段，如果字段是id，且int，自增，在这里不生成，数据库那边插入，会自动生成

                    if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) && property.GetCustomAttributes(true).All(attr => attr.GetType().Name != typeof(RequiredAttribute).Name) && property.PropertyType != typeof(Guid) && property.PropertyType != typeof(string)) continue;

                    sb.AppendFormat("@{0}", property.Name);
                    if (i < props.Count() - 1)
                        sb.Append(", ");
                }
                if (sb.ToString().EndsWith(", "))
                    sb.Remove(sb.Length - 2, 2);
            });
        }



        //build insert parameters which include all properties in the class that are not:
        //marked with the Editable(false) attribute
        //marked with the [Key] attribute
        //marked with [IgnoreInsert]
        //named Id
        //marked with [NotMapped]
        public static void BuildInsertParameters<T>(StringBuilder masterSb)
        {
            StringBuilderCache(masterSb, $"{typeof(T).FullName}_BuildInsertParameters", sb =>
            {
                var props = GetScaffoldableProperties<T>().ToArray();

                for (var i = 0; i < props.Count(); i++)
                {
                    var property = props.ElementAt(i);
                    if (property.PropertyType != typeof(Guid) && property.PropertyType != typeof(string)
                          && property.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(KeyAttribute).Name)
                          && property.GetCustomAttributes(true).All(attr => attr.GetType().Name != typeof(RequiredAttribute).Name))
                        continue;
                    if (property.GetCustomAttributes(true).Any(attr =>
                        attr.GetType().Name == typeof(IgnoreInsertAttribute).Name ||
                        attr.GetType().Name == typeof(NotMappedAttribute).Name ||
                        attr.GetType().Name == typeof(ReadOnlyAttribute).Name && IsReadOnly(property))) continue;

                    //如果满足属性 名称为Id，且非必须，属性类别不是guid，不是string，直接跳过，不生成该字段，如果字段是id，且int，自增，在这里不生成，数据库那边插入，会自动生成

                    if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) && property.GetCustomAttributes(true).All(attr => attr.GetType().Name != typeof(RequiredAttribute).Name) && property.PropertyType != typeof(Guid) && property.PropertyType != typeof(string)) continue;

                    sb.Append(GetColumnName(property));
                    if (i < props.Count() - 1)
                        sb.Append(", ");
                }
                if (sb.ToString().EndsWith(", "))
                    sb.Remove(sb.Length - 2, 2);
            });
        }


        public static void BuildCreateTable<T>(StringBuilder masterSb)
        {


            StringBuilderCache(masterSb, $"{typeof(T).FullName}_BuildInsertParameters", sb =>
            {
                var props = GetScaffoldableProperties<T>().ToArray();

                for (var i = 0; i < props.Count(); i++)
                {
                    var property = props.ElementAt(i);
                    if (property.PropertyType != typeof(Guid) && property.PropertyType != typeof(string)
                          && property.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(KeyAttribute).Name)
                          && property.GetCustomAttributes(true).All(attr => attr.GetType().Name != typeof(RequiredAttribute).Name))
                        continue;
                    if (property.GetCustomAttributes(true).Any(attr =>
                        attr.GetType().Name == typeof(IgnoreInsertAttribute).Name ||
                        attr.GetType().Name == typeof(NotMappedAttribute).Name ||
                        attr.GetType().Name == typeof(ReadOnlyAttribute).Name && IsReadOnly(property))) continue;

                    if (property.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) && property.GetCustomAttributes(true).All(attr => attr.GetType().Name != typeof(RequiredAttribute).Name) && property.PropertyType != typeof(Guid)) continue;


                    sb.Append(string.Format(" `{0}` INT UNSIGNED AUTO_INCREMENT,", GetColumnName(property), "INT", "UNSIGNED", "AUTO_INCREMENT"));
                    if (i < props.Count() - 1)
                        sb.Append(", ");
                }
                if (sb.ToString().EndsWith(", "))
                    sb.Remove(sb.Length - 2, 2);
            });

            //            CREATE TABLE IF NOT EXISTS `runoob_tbl`(
            //   `runoob_id` INT UNSIGNED AUTO_INCREMENT,
            //   `runoob_title` VARCHAR(100) NOT NULL,
            //   `runoob_author` VARCHAR(40) NOT NULL,
            //   `submission_date` DATE,
            //   PRIMARY KEY( `runoob_id` )
            //)ENGINE = InnoDB DEFAULT CHARSET = utf8;
        }

        //Get all properties in an entity
        private static IEnumerable<PropertyInfo> GetAllProperties<T>(T entity) where T : class
        {
            if (entity == null) return new PropertyInfo[0];
            return entity.GetType().GetProperties();
        }

        //Get all properties that are not decorated with the Editable(false) attribute
        private static IEnumerable<PropertyInfo> GetScaffoldableProperties<T>()
        {
            IEnumerable<PropertyInfo> props = typeof(T).GetProperties();

            props = props.Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(EditableAttribute).Name && !IsEditable(p)) == false);


            return props.Where(p => p.PropertyType.IsSimpleType() || IsEditable(p));
        }

        //Determine if the Attribute has an AllowEdit key and return its boolean state
        //fake the funk and try to mimic EditableAttribute in System.ComponentModel.DataAnnotations 
        //This allows use of the DataAnnotations property in the model and have the SimpleCRUD engine just figure it out without a reference
        private static bool IsEditable(PropertyInfo pi)
        {
            var attributes = pi.GetCustomAttributes(false);
            if (attributes.Length > 0)
            {
                dynamic write = attributes.FirstOrDefault(x => x.GetType().Name == typeof(EditableAttribute).Name);
                if (write != null)
                {
                    return write.AllowEdit;
                }
            }
            return false;
        }


        //Determine if the Attribute has an IsReadOnly key and return its boolean state
        //fake the funk and try to mimic ReadOnlyAttribute in System.ComponentModel 
        //This allows use of the DataAnnotations property in the model and have the SimpleCRUD engine just figure it out without a reference
        private static bool IsReadOnly(PropertyInfo pi)
        {
            var attributes = pi.GetCustomAttributes(false);
            if (attributes.Length > 0)
            {
                dynamic write = attributes.FirstOrDefault(x => x.GetType().Name == typeof(ReadOnlyAttribute).Name);
                if (write != null)
                {
                    return write.IsReadOnly;
                }
            }
            return false;
        }

        //Get all properties that are:
        //Not named Id
        //Not marked with the Key attribute
        //Not marked ReadOnly
        //Not marked IgnoreInsert
        //Not marked NotMapped
        private static IEnumerable<PropertyInfo> GetUpdateableProperties<T>(T entity)
        {
            var updateableProperties = GetScaffoldableProperties<T>();
            //remove ones with ID
            updateableProperties = updateableProperties.Where(p => !p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
            //remove ones with key attribute
            updateableProperties = updateableProperties.Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(KeyAttribute).Name) == false);
            //remove ones that are readonly
            updateableProperties = updateableProperties.Where(p => p.GetCustomAttributes(true).Any(attr => (attr.GetType().Name == typeof(ReadOnlyAttribute).Name) && IsReadOnly(p)) == false);
            //remove ones with IgnoreUpdate attribute
            updateableProperties = updateableProperties.Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(IgnoreUpdateAttribute).Name) == false);
            //remove ones that are not mapped
            updateableProperties = updateableProperties.Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(NotMappedAttribute).Name) == false);

            return updateableProperties;
        }

        //Get all properties that are named Id or have the Key attribute
        //For Inserts and updates we have a whole entity so this method is used
        private static IEnumerable<PropertyInfo> GetIdProperties(object entity)
        {
            var type = entity.GetType();
            return GetIdProperties(type);
        }

        //Get all properties that are named Id or have the Key attribute
        //For Get(id) and Delete(id) we don't have an entity, just the type so this method is used
        private static IEnumerable<PropertyInfo> GetIdProperties(Type type)
        {
            var tp = type.GetProperties().Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(KeyAttribute).Name)).ToList();
            return tp.Any() ? tp : type.GetProperties().Where(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
        }

        //Gets the table name for this entity
        //For Inserts and updates we have a whole entity so this method is used
        //Uses class name by default and overrides if the class has a Table attribute
        private static string GetTableName(object entity)
        {
            var type = entity.GetType();
            return GetTableName(type);
        }

        //Gets the table name for this type
        //For Get(id) and Delete(id) we don't have an entity, just the type so this method is used
        //Use dynamic type to be able to handle both our Table-attribute and the DataAnnotation
        //Uses class name by default and overrides if the class has a Table attribute
        private static string GetTableName(Type type)
        {
            string tableName;

            if (TableNames.TryGetValue(type, out tableName))
                return tableName;

            tableName = _tableNameResolver.ResolveTableName(type);

            TableNames.AddOrUpdate(type, tableName, (t, v) => tableName);

            return tableName;
        }

        private static string GetColumnName(PropertyInfo propertyInfo)
        {
            string columnName, key = string.Format("{0}.{1}", propertyInfo.DeclaringType, propertyInfo.Name);

            if (ColumnNames.TryGetValue(key, out columnName))
                return columnName;

            columnName = _columnNameResolver.ResolveColumnName(propertyInfo);

            ColumnNames.AddOrUpdate(key, columnName, (t, v) => columnName);

            return columnName;
        }

        private static string Encapsulate(string databaseword)
        {
            return string.Format(_encapsulation, databaseword);
        }
        /// <summary>
        /// Generates a GUID based on the current date/time
        /// http://stackoverflow.com/questions/1752004/sequential-guid-generator-c-sharp
        /// </summary>
        /// <returns></returns>
        public static Guid SequentialGuid()
        {
            var tempGuid = Guid.NewGuid();
            var bytes = tempGuid.ToByteArray();
            var time = DateTime.Now;
            bytes[3] = (byte)time.Year;
            bytes[2] = (byte)time.Month;
            bytes[1] = (byte)time.Day;
            bytes[0] = (byte)time.Hour;
            bytes[5] = (byte)time.Minute;
            bytes[4] = (byte)time.Second;
            // bytes[6] = (byte)time.Millisecond;

            //16 bytes long.
            return new Guid(bytes);
        }

        /// <summary>
        /// Database server dialects
        /// </summary>
        public enum Dialect
        {
            MySQL = 0,
            SQLServer = 1,
            PostgreSQL = 2,
            //Oracle = 3,
            SQLite = 4

        }

        public interface ITableNameResolver
        {
            string ResolveTableName(Type type);
        }

        public interface IColumnNameResolver
        {
            string ResolveColumnName(PropertyInfo propertyInfo);
        }

        public class TableNameResolver : ITableNameResolver
        {
            public virtual string ResolveTableName(Type type)
            {
                var tableName = Encapsulate(type.Name);

                var tableattr = type.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(TableAttribute).Name) as dynamic;
                if (tableattr != null)
                {
                    tableName = Encapsulate(tableattr.Name);
                    try
                    {
                        if (!String.IsNullOrEmpty(tableattr.Schema))
                        {
                            string schemaName = Encapsulate(tableattr.Schema);
                            tableName = String.Format("{0}.{1}", schemaName, tableName);
                        }
                    }
                    catch (RuntimeBinderException)
                    {
                        //Schema doesn't exist on this attribute.
                    }
                }

                return tableName;
            }
        }

        public class ColumnNameResolver : IColumnNameResolver
        {
            public virtual string ResolveColumnName(PropertyInfo propertyInfo)
            {
                var columnName = Encapsulate(propertyInfo.Name);

                var columnattr = propertyInfo.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType().Name == typeof(ColumnAttribute).Name) as dynamic;
                if (columnattr != null)
                {
                    columnName = Encapsulate(columnattr.Name);
                    if (Debugger.IsAttached)
                        Trace.WriteLine(String.Format("Column name for type overridden from {0} to {1}", propertyInfo.Name, columnName));
                }
                return columnName;
            }
        }
    }

    /// <summary>
    /// Optional Table attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify the table name of a poco
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// Optional Table attribute.
        /// </summary>
        /// <param name="tableName"></param>
        public TableAttribute(string tableName)
        {
            Name = tableName;
        }
        /// <summary>
        /// Name of the table
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Name of the schema
        /// </summary>
        public string Schema { get; set; }
    }

    /// <summary>
    /// Optional Column attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify the table name of a poco
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// Optional Column attribute.
        /// </summary>
        /// <param name="columnName"></param>
        public ColumnAttribute(string columnName)
        {
            Name = columnName;
        }
        /// <summary>
        /// Name of the column
        /// </summary>
        public string Name { get; private set; }
    }

    /// <summary>
    /// Optional Key attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify the Primary Key of a poco
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
    }

    /// <summary>
    /// Optional NotMapped attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify that the property is not mapped
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMappedAttribute : Attribute
    {
    }

    /// <summary>
    /// Optional Key attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify a required property of a poco
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute
    {
    }

    /// <summary>
    /// Optional Editable attribute.
    /// You can use the System.ComponentModel.DataAnnotations version in its place to specify the properties that are editable
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EditableAttribute : Attribute
    {
        /// <summary>
        /// Optional Editable attribute.
        /// </summary>
        /// <param name="iseditable"></param>
        public EditableAttribute(bool iseditable)
        {
            AllowEdit = iseditable;
        }
        /// <summary>
        /// Does this property persist to the database?
        /// </summary>
        public bool AllowEdit { get; private set; }
    }

    /// <summary>
    /// Optional Readonly attribute.
    /// You can use the System.ComponentModel version in its place to specify the properties that are editable
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ReadOnlyAttribute : Attribute
    {
        /// <summary>
        /// Optional ReadOnly attribute.
        /// </summary>
        /// <param name="isReadOnly"></param>
        public ReadOnlyAttribute(bool isReadOnly)
        {
            IsReadOnly = isReadOnly;
        }
        /// <summary>
        /// Does this property persist to the database?
        /// </summary>
        public bool IsReadOnly { get; private set; }
    }

    /// <summary>
    /// Optional IgnoreSelect attribute.
    /// Custom for Dapper.SimpleCRUD to exclude a property from Select methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreSelectAttribute : Attribute
    {
    }

    /// <summary>
    /// Optional IgnoreInsert attribute.
    /// Custom for Dapper.SimpleCRUD to exclude a property from Insert methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreInsertAttribute : Attribute
    {
    }

    /// <summary>
    /// Optional IgnoreUpdate attribute.
    /// Custom for Dapper.SimpleCRUD to exclude a property from Update methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }

}

internal static class TypeExtension
{
    //You can't insert or update complex types. Lets filter them out.
    public static bool IsSimpleType(this Type type)
    {
        var underlyingType = Nullable.GetUnderlyingType(type);
        type = underlyingType ?? type;
        var simpleTypes = new List<Type>
                               {
                                   typeof(byte),
                                   typeof(sbyte),
                                   typeof(short),
                                   typeof(ushort),
                                   typeof(int),
                                   typeof(uint),
                                   typeof(long),
                                   typeof(ulong),
                                   typeof(float),
                                   typeof(double),
                                   typeof(decimal),
                                   typeof(bool),
                                   typeof(string),
                                   typeof(char),
                                   typeof(Guid),
                                   typeof(DateTime),
                                   typeof(DateTimeOffset),
                                   typeof(TimeSpan),
                                   typeof(byte[])
                               };
        return simpleTypes.Contains(type) || type.IsEnum;
    }

    public static string CacheKey(this IEnumerable<PropertyInfo> props)
    {
        return string.Join(",", props.Select(p => p.DeclaringType.FullName + "." + p.Name).ToArray());
    }
}
