
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace YC.Common.ShareUtils
{
    /// <summary>
    /// 自动化生成数据库
    /// </summary>
   public class CreateDBUtils
    {
        /// <summary>
        /// 测试自动化创建数据库，初始化数据，但是异常时候无法回滚以创建的数据库，可以回滚数据
        /// excuteSql 返回受影响的条数
        /// </summary>
       /// <param name="createDBName"></param>
       /// <param name="sqlFilePath"></param>
       /// <param name="error"></param>
       /// <returns></returns>
       //public static bool CreateDB(string createDBName,string sqlFilePath, out string error) {

       //    string msg = "";
       //    int tag = 0;
       //     error = "";
       //     bool result = false;

       //    string databaseName = createDBName;

       //    #region 1、判断需要生成的数据库是否存在
       //    string sqlDBeExists = "if exists(select 1 from sysdatabases where name='" + databaseName + "')";
       //    sqlDBeExists += " select 1 else select 0";

       //    string masterString = "Data Source=.;Initial Catalog=master;User ID=sa;Password=linbin";
       //    DbSQLUtils.connectionString = masterString;
       //    string localDBString = "Data Source=.;Initial Catalog=" + databaseName + ";User ID=sa;Password=linbin";

       //    //判断是否存在数据库，1 有
       //    object objResult = DbSQLUtils.GetSingle(sqlDBeExists);
       //    int isCreate = 0;

       //    if (Convert.ToInt32(objResult) == 0)
       //    {
       //        isCreate = DbSQLUtils.ExecuteSql("create database [" + databaseName + "] ");
       //    }
       //    #endregion

       //    #region 2、生成数据

       //    using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
       //    {
       //        try
       //        {

       //            if (Convert.ToInt32(objResult) == 0)
       //            {
       //                #region 第一次自动化创建数据库和表

       //                //创建成功数据库，自动创建表
       //                if (isCreate == -1)//表示成功，受影响的条数为1 -符号忽略
       //                {
       //                    string templateContent;
       //                    string path = sqlFilePath;//注意路径拼接符 要"\" 当异常时候，文件存在资源占用，无法读取
       //                    //1、得到数据库sql 模板
       //                    bool Status = FileUtils.GetDBSqlTemplate(path, out templateContent);

       //                    try
       //                    {
       //                        //注意这个时候要切换数据库连接配置，使用当前创建的数据库
       //                        DbSQLUtils.connectionString = localDBString;
       //                        int createtableStatus = DbSQLUtils.ExecuteSql(templateContent);
       //                        //表示创建成功表单
       //                        if (createtableStatus > 0)
       //                        {

       //                            tag = 1;
       //                            msg = "创建多表成功！";

       //                        }
       //                        else
       //                        {
       //                            msg += "创建表失败！";

       //                            //表示创建失败，需要回滚
       //                            throw new Exception(msg);

       //                        }
       //                    }
       //                    catch (Exception)
       //                    {

       //                        throw;
       //                    }

       //                    string tt = "";
       //                    //判断创建表结果
       //                }
       //                else
       //                {

       //                    msg = "自动化创建数据库" + databaseName + "失败！请再次点击！";
       //                }
       //                #endregion
       //            }
       //            else
       //            {
       //                #region 已存在数据库状况下
       //                //已经存在数据库，判断是否存在表，如果没有创建
       //                //判断最后一张表是否存在，如果创建表示创建成功，如果没有，就回滚（分布式事务）
       //                string sql = "select name from sys.tables go";

       //                //注意这个时候要切换数据库连接配置，使用当前创建的数据库
       //                DbSQLUtils.connectionString = localDBString;
       //                DataSet ds = DbSQLUtils.Query(sql);
       //                List<string> tableList = new List<string>();
       //                foreach (DataRow dr in ds.Tables[0].Rows)
       //                {

       //                    tableList.Add(dr["name"].ToString());
       //                }


       //                #endregion
       //            }

       //            transactionScope.Complete();
       //        }
       //        catch (Exception ex)
       //        {

       //            Transaction.Current.Rollback();
       //            DbSQLUtils.connectionString = masterString;
       //            try
       //            {
       //                int dropDB = DbSQLUtils.ExecuteSql("drop database " + databaseName);
       //            }
       //            catch (Exception)
       //            {

       //                throw;
       //            }
       //            msg = "操作失败！</br>" + ex.ToString();

       //        }
       //    }
       //    #endregion

       //    #region 3、返回结果
       //    if (tag > 0)
       //    {
       //        error += msg;

       //        return result;
       //    }
       //    else
       //    {
       //        result = true;
       //        return result;
       //    }
       //    #endregion
       //}
    }
}
