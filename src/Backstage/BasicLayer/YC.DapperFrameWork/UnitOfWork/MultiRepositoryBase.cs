using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YC.DapperFrameWork
{
    public class MultiRepositoryBase<TEntity> : IMultiRepository<TEntity> where TEntity : class, new()
    {
        public IDbConnection connection;
        IUnitOfWork _iUnitOfWorkManager;
        public MultiRepositoryBase(IUnitOfWork iUnitOfWorkManager)
        {
            _iUnitOfWorkManager = iUnitOfWorkManager;

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TKey">主键key</typeparam>
        /// <param name="entity">新增实体</param>
        /// <returns></returns>
        public TKey Insert<TKey>(TEntity entity)
        {
            
           
                var id = connection.Insert<TKey, TEntity>(entity);
                var user = connection.Get<TEntity>(id);
                return (TKey)id;
            
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
            var tp = type.GetProperties().Where(p => p.GetCustomAttributes(true).Any(attr => attr.GetType().Name == typeof(System.ComponentModel.DataAnnotations.KeyAttribute).Name)).ToList();
            return tp.Any() ? tp : type.GetProperties().Where(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
        }

        private static string GetTableName(object entity)
        {
            var type = entity.GetType();
            return GetTableName(type);
        }


    }
}
