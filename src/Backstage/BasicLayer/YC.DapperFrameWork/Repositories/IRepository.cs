using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using YC.Core.Autofac;

namespace YC.DapperFrameWork
{
    public interface IRepository<TEntity> : IDisposable, IDependencyInjectionSupport where TEntity : class, new()
    {


        #region 同步操作，单个操作,批量操作
        IUnitOfWork unitOfWork { get; set; }
        /// <summary>
        /// 根据Id获取数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        TEntity Get(object id);

        /// <summary>
        /// 根据查询条件，返回结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<TEntity> GetList(object whereCodition);

        /// <summary>
        /// 根据查询条件，返回结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<TEntity> GetList(string conditions, object parameters = null);

        /// <summary>
        /// 返回所有结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<TEntity> GetAllList();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetPageList(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null);

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TKey">主键key</typeparam>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        TKey Insert<TKey>(TEntity entity);

 
        int InsertBatchList<TKey>(List<TEntity> entityList);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(TEntity entity, string whereCondition = null);

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(TEntity entity, string whereCondition = null);

        /// <summary>
        /// 删除指定Id记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete<TKey>(TKey tKey, string whereCondition = null);

        /// <summary>
        /// 删除符合条件的记录 sql条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int DeleteList(string conditions);

        /// <summary>
        /// 删除符合条件的记录 参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int DeleteList(object whereConditions);

        /// <summary>
        /// 删除符合条件的记录 sql条件+参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int DeleteList(string conditions, object parameters = null);

        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int GetRecordCount(string condition = "", object parameters = null);


        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int GetRecordCount(object whereCondition);
        #endregion

        #region 异步操作，单个操作，批量操作

        /// <summary>
        /// 根据Id获取数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(object id);


        /// <summary>
        /// 根据查询条件，返回结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(object whereCodition);

        /// <summary>
        /// 根据查询条件，返回结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(string conditions, object parameters = null);

        /// <summary>
        /// 返回所有结果集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllListAsync();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetPageListAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null);


        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TKey">主键key</typeparam>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        Task<TKey> InsertAsync<TKey>(TEntity entity);

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TKey">主键key</typeparam>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        Task<int> InsertBatchListAsync<TKey>(List<TEntity> entityList);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// 删除指定Id记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteAsync<TKey>(TKey tKey);

        /// <summary>
        /// 删除符合条件的记录 sql条件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteListAsync(string conditions);

        /// <summary>
        /// 删除符合条件的记录 参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteListAsync(object whereConditions);

        /// <summary>
        /// 删除符合条件的记录 sql条件+参数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteListAsync(string conditions, object parameters = null);

        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> GetRecordCountAsync(string condition = "", object parameters = null);


        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> GetRecordCountAsync(object whereCondition);

        #endregion
    }
}
