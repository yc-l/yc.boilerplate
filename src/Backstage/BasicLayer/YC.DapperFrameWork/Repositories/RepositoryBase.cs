using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Common;
using YC.Common.ShareUtils;
using YC.Core;
using YC.Core.Autofac;
using YC.Core.Domain.Entity;

namespace YC.DapperFrameWork
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, new()

    {
        public RepositoryBase(IUnitOfWork unitOfWork, ITenant tenant=null)
        {
            this._unitOfWork = unitOfWork;
        }
        IUnitOfWork _unitOfWork = null;
        IUnitOfWork IRepository<TEntity>.unitOfWork
        {
            get { return _unitOfWork; }
            set { this._unitOfWork = value; }
        }

        private RepositoryUtils.Dialect _dbtype;


        #region  前置服务

        /// <summary>
        /// 修改固定动态操作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TEntity SetUpdate(TEntity input)
        {
            System.Reflection.PropertyInfo[] pr = input.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pi in pr)
            {
                // object obj = Activator.CreateInstance(pi.PropertyType);//创建指定类型实例

                Type baseEntityType = typeof(BaseEntity);
                if (pi.CanWrite)
                {//是否可以写入
                    if (pi.Name == "LastModificationTimeStamp")
                    {
                        if (baseEntityType.GetProperty(pi.Name) != null)
                        {
                            pi.SetValue(input, Convert.ChangeType(UnixDateUtils.ConvertDateTimeLong(DateTime.Now), pi.PropertyType), null);
                        }
                    }


                }
            }
            return input;
        }

        /// <summary>
        /// 新增固定动态操作
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TEntity SetCreate(TEntity input)
        {
            System.Reflection.PropertyInfo[] pr = input.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pi in pr)
            {
                // object obj = Activator.CreateInstance(pi.PropertyType);//创建指定类型实例
                BaseEntity be = new BaseEntity();
                Type baseEntityType = typeof(BaseEntity);
                if (pi.CanWrite)
                {//是否可以写入
                    if (pi.Name == "CreationTimeStamp")
                    {
                        if (baseEntityType.GetProperty(pi.Name) != null)
                        {
                            pi.SetValue(input, Convert.ChangeType(UnixDateUtils.ConvertDateTimeLong(DateTime.Now), pi.PropertyType), null);
                        }
                    }
                    if (pi.Name == "IsActive")
                    {
                        if (baseEntityType.GetProperty(pi.Name) != null)
                        {
                            pi.SetValue(input, Convert.ChangeType(true, pi.PropertyType), null);
                        }
                    }

                    if (pi.Name == "IsDeleted")
                    {
                        if (baseEntityType.GetProperty(pi.Name) != null)
                        {
                            pi.SetValue(input, Convert.ChangeType(false, pi.PropertyType), null);
                        }
                    }

                }
            }
            return input;
        }


        /// <summary>
        /// 删除固定动态操作,软删除,本仓储暂未使用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TEntity SetDelete(TEntity input)
        {
            System.Reflection.PropertyInfo[] pr = input.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pi in pr)
            {
                // object obj = Activator.CreateInstance(pi.PropertyType);//创建指定类型实例

                Type baseEntityType = typeof(BaseEntity);
                if (pi.CanWrite)
                {//是否可以写入
                    if (pi.Name == "DeletionTime")
                    {
                        if (baseEntityType.GetProperty(pi.Name) != null)
                        {
                            pi.SetValue(input, Convert.ChangeType(UnixDateUtils.ConvertDateTimeLong(DateTime.Now), pi.PropertyType), null);
                        }
                    }
                    if (pi.Name == "IsDeleted")
                    {
                        if (baseEntityType.GetProperty(pi.Name) != null)
                        {
                            pi.SetValue(input, Convert.ChangeType(true, pi.PropertyType), null);
                        }
                    }

                }
            }
            return input;
        }
        #endregion

        #region 同步操作，单个操作,批量操作

        #region 查询
        /// <summary>
        /// 根据Id获取数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public TEntity Get(object id)
        {
            return _unitOfWork.Connection.Get<TEntity>(id);

        }


        /// <summary>
        /// 根据查询条件，返回结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TEntity> GetList(object whereCodition)
        {

            var entityList = _unitOfWork.Connection.GetList<TEntity>(whereCodition).ToList();//new { Age = 10 }
            return entityList;


        }

        /// <summary>
        /// 根据查询条件，返回结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TEntity> GetList(string conditions, object parameters = null)
        {

            var entityList = _unitOfWork.Connection.GetList<TEntity>(conditions, parameters).ToList();//"where Age > @Age",new { Age = 10 }                                                                                             // entityList.Count().IsEqualTo(3);
            return entityList;

        }

        /// <summary>
        /// 返回所有结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TEntity> GetAllList()
        {
           // int temp = new Random().Next(0, 1000000000);
            var entityList = _unitOfWork.Connection.GetList<TEntity>().ToList();
            return entityList;


        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetPageList(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
        {

            var entityList = _unitOfWork.Connection.GetListPaged<TEntity>(pageNumber, rowsPerPage, conditions, orderby).ToList();//1, 3, "Where Name LIKE 'Person 2%'", "age desc"
            return entityList;

        }
        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TKey">主键key</typeparam>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        public TKey Insert<TKey>(TEntity entity)
        {

            entity = SetCreate(entity);
            var id = _unitOfWork.Connection.Insert<TKey, TEntity>(entity);
            var user = _unitOfWork.Connection.Get<TEntity>(id);
            return (TKey)id;

        }

        /// <summary>
        /// 批量新增，一条条执行
        /// </summary>
        /// <typeparam name="TKey">主键key</typeparam>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        public int InsertList<TKey>(List<TEntity> entityList)
        {

            int count = 0;

            for (int i = 0; i < entityList.Count; i++)
            {
                //if (i > 35)
                //   throw new Exception("throw create exception to stop insert operation in order to trigger transaction");

                var temp = SetCreate(entityList[i]);
                var id = _unitOfWork.Connection.Insert<TKey, TEntity>(temp, _unitOfWork.GetTransaction());
                count += 1;

            }

            #region test
            //try
            //{
            //    for (int i = 0; i < entityList.Count; i++)
            //    {
            //        //if (i > 35)
            //        //    throw new Exception("throw create exception to stop insert operation in order to trigger transaction");
            //        var id =_unitOfWork.Connection.Insert<TKey, TEntity>(entityList[i], transaction);

            //        count += 1;
            //    }
            //    transaction.Commit();
            //}
            //catch (Exception ex)
            //{

            //    transaction.Rollback();
            //    throw ex;
            //}

            #endregion
            return count;
        }

        /// <summary>
        /// 批量新增，一次性执行
        /// </summary>
        /// <typeparam name="TKey">主键key</typeparam>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        public int InsertBatchList<TKey>(List<TEntity> entityList)
        {
            List<TEntity> newEntityList = new List<TEntity>();
            int count = 0;

            for (int i = 0; i < entityList.Count; i++)
            {
                var temp = entityList[i];
                temp = SetCreate(temp);
                newEntityList.Add(temp);
            }

            count = _unitOfWork.Connection.InsertList<TKey, TEntity>(newEntityList, _unitOfWork.GetTransaction());

            return count;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <typeparam name="TKey">主键key</typeparam>
        /// <returns></returns>
        public void CreateTable<TKey>()
        {

            int count = 0;

            _unitOfWork.Connection.CreateTable<TKey, TEntity>(_unitOfWork.GetTransaction());

        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(TEntity entity, string whereCondition = null)
        {

            entity = SetUpdate(entity);
            return _unitOfWork.Connection.Update(entity, whereCondition, _unitOfWork.GetTransaction());


        }

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(TEntity entity, string whereCondition = null)
        {

            //entity= SetDelete(entity);
            return _unitOfWork.Connection.Delete(entity, whereCondition, _unitOfWork.GetTransaction());


        }

        /// <summary>
        /// 删除指定Id记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete<TKey>(TKey tKey, string whereCondition = null)
        {

            return _unitOfWork.Connection.Delete<TEntity>(tKey, whereCondition = null, _unitOfWork.GetTransaction());


        }

        /// <summary>
        /// 删除符合条件的记录 sql条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteList(string conditions)
        {

            int count = 0;
            count = _unitOfWork.Connection.DeleteList<TEntity>(conditions, _unitOfWork.GetTransaction());//Where age > 9

            return count;


        }

        /// <summary>
        /// 删除符合条件的记录 参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteList(object whereConditions)
        {

            return _unitOfWork.Connection.DeleteList<TEntity>(whereConditions, _unitOfWork.GetTransaction());//new { age = 9 }

        }

        /// <summary>
        /// 删除符合条件的记录 sql条件+参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteList(string conditions, object parameters = null)
        {

            int result = 0;
            result = _unitOfWork.Connection.DeleteListByParams<TEntity>(conditions, parameters, _unitOfWork.GetTransaction());// "where age >= @Age", new { Age = 9 }

            return result;


        }

        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int GetRecordCount(string condition = "", object parameters = null)
        {
            int result = 0;
            result = _unitOfWork.Connection.RecordCount<TEntity>(condition, parameters);//"where age = 10 or age = 11"
            return result;


        }


        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int GetRecordCount(object whereCondition)
        {

            int result = 0;
            result = _unitOfWork.Connection.RecordCount<TEntity>(whereCondition);//"where age = 10 or age = 11"
            return result;


        }
        #endregion

        #region 异步操作，单个操作，批量操作

        /// <summary>
        /// 根据Id获取数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(object id)
        {

            return await _unitOfWork.Connection.GetAsync<TEntity>(id);


        }


        /// <summary>
        /// 根据查询条件，返回结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListAsync(object whereCodition)
        {

            var entityList = await _unitOfWork.Connection.GetListAsync<TEntity>(whereCodition);//new { Age = 10 }
            return entityList.ToList();


        }

        /// <summary>
        /// 根据查询条件，返回结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListAsync(string conditions, object parameters = null)
        {

            var entityList = await _unitOfWork.Connection.GetListAsync<TEntity>(conditions, parameters);//"where Age > @Age",new { Age = 10 }                                                                                                       // entityList.Count().IsEqualTo(3);
            return entityList.ToList();


        }

        /// <summary>
        /// 返回所有结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetAllListAsync()
        {

            var entityList = await _unitOfWork.Connection.GetListAsync<TEntity>();
            return entityList.ToList();


        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> GetPageListAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null)
        {

            var entityList = await _unitOfWork.Connection.GetListPagedAsync<TEntity>(pageNumber, rowsPerPage, conditions, orderby);//1, 3, "Where Name LIKE 'Person 2%'", "age desc"
            return entityList.ToList();


        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TKey">主键key</typeparam>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        public async Task<TKey> InsertAsync<TKey>(TEntity entity)
        {

            entity = SetCreate(entity);
            var id = await _unitOfWork.Connection.InsertAsync<TKey, TEntity>(entity);
            var user = _unitOfWork.Connection.Get<TEntity>(id);
            return (TKey)id;

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TKey">主键key</typeparam>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        public async Task<int> InsertBatchListAsync<TKey>(List<TEntity> entityList)
        {

            int count = 0;

            for (int i = 0; i < entityList.Count; i++)
            {
                var temp = entityList[i];
                temp = SetCreate(temp);
                //if (i > 35)
                //    throw new Exception("throw create exception to stop insert operation in order to trigger transaction");
                //配合事务，是批量一次性提交，如果没有事务就是一条条提交的
                var id = await _unitOfWork.Connection.InsertAsync<TKey, TEntity>(temp, _unitOfWork.GetTransaction());
                count += 1;
            }
            return count;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {

            entity = SetUpdate(entity);
            return await _unitOfWork.Connection.UpdateAsync(entity, _unitOfWork.GetTransaction());

        }

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {

            return await _unitOfWork.Connection.DeleteAsync(entity, _unitOfWork.GetTransaction());

        }

        /// <summary>
        /// 删除指定Id记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<TKey>(TKey tKey)
        {

            return await _unitOfWork.Connection.DeleteAsync<TEntity>(tKey, _unitOfWork.GetTransaction());

        }

        /// <summary>
        /// 删除符合条件的记录 sql条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> DeleteListAsync(string conditions)
        {

            int count = 0;

            count = await _unitOfWork.Connection.DeleteAsync<TEntity>(conditions, _unitOfWork.GetTransaction());//Where age > 9
            return count;


        }

        /// <summary>
        /// 删除符合条件的记录 参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> DeleteListAsync(object whereConditions)
        {

            return await _unitOfWork.Connection.DeleteListAsync<TEntity>(whereConditions, _unitOfWork.GetTransaction());//new { age = 9 }

        }

        /// <summary>
        /// 删除符合条件的记录 sql条件+参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> DeleteListAsync(string conditions, object parameters = null)
        {

            int result = 0;
            result = await _unitOfWork.Connection.DeleteListAsync<TEntity>(conditions, parameters, _unitOfWork.GetTransaction());// "where age >= @Age", new { Age = 9 }

            return result;


        }

        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> GetRecordCountAsync(string condition = "", object parameters = null)
        {

            int result = 0;
            result = await _unitOfWork.Connection.RecordCountAsync<TEntity>(condition, parameters);//"where age = 10 or age = 11"
            return result;

        }


        /// <summary>
        /// 查询记录条数
        /// </summary>   
        /// <param name="condition"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<int> GetRecordCountAsync(object whereCondition)
        {

            int result = 0;
            result = await _unitOfWork.Connection.RecordCountAsync<TEntity>(whereCondition);//"where age = 10 or age = 11"
            return result;
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }

            #endregion
        }
    }
}
