using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Core.Cache
{
    public interface ICacheManager
    {
        #region 验证缓存是否存在
        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        bool Exists(string key);
        #endregion

        #region 添加缓存
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        bool Add(string key, object value);

       

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);

       
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <returns></returns>
        bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false);

     
        #endregion

        #region  删除缓存
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        bool Remove(string key);

        

        /// <summary>
        /// 批量删除缓存
        /// </summary>
        /// <param name="key">缓存Key集合</param>
        /// <returns></returns>
        void RemoveAll(IEnumerable<string> keys);

     
        #endregion

        #region 获取缓存
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;

       

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        object Get(string key);

        

        /// <summary>
        /// 获取缓存集合
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        IDictionary<string, object> GetAll(IEnumerable<string> keys);

        #endregion

        #region 修改缓存

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <returns></returns>
        bool Replace(string key, object value);

       

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        bool Replace(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);
      

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        /// <returns></returns>
        bool Replace(string key, object value, TimeSpan expiresIn, bool isSliding = false);

       
        #endregion

        #region 异步


        ///// <summary>
        ///// 验证缓存项是否存在（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <returns></returns>
        //Task<bool> ExistsAsync(string key);

        ///// <summary>
        ///// 添加缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <param name="value">缓存Value</param>
        ///// <returns></returns>
        //Task<bool> AddAsync(string key, object value);

        ///// <summary>
        ///// 添加缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <param name="value">缓存Value</param>
        ///// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        ///// <param name="expiressAbsoulte">绝对过期时长</param>
        ///// <returns></returns>
        //Task<bool> AddAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);

        ///// <summary>
        ///// 添加缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <param name="value">缓存Value</param>
        ///// <param name="expiresIn">缓存时长</param>
        ///// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        ///// <returns></returns>
        //Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false);

        ///// <summary>
        ///// 删除缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <returns></returns>
        //Task<bool> RemoveAsync(string key);

        ///// <summary>
        ///// 批量删除缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key集合</param>
        ///// <returns></returns>
        //Task RemoveAllAsync(IEnumerable<string> keys);

        ///// <summary>
        ///// 获取缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <returns></returns>
        //Task<T> GetAsync<T>(string key) where T : class;


        ///// <summary>
        ///// 获取缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <returns></returns>
        //Task<object> GetAsync(string key);

        ///// <summary>
        ///// 获取缓存集合（异步方式）
        ///// </summary>
        ///// <param name="keys">缓存Key集合</param>
        ///// <returns></returns>
        //Task<IDictionary<string, object>> GetAllAsync(IEnumerable<string> keys);

        ///// <summary>
        ///// 修改缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <param name="value">新的缓存Value</param>
        ///// <returns></returns>
        //Task<bool> ReplaceAsync(string key, object value);
        ///// <summary>
        ///// 修改缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <param name="value">新的缓存Value</param>
        ///// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        ///// <param name="expiressAbsoulte">绝对过期时长</param>
        ///// <returns></returns>
        //Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);

        ///// <summary>
        ///// 修改缓存（异步方式）
        ///// </summary>
        ///// <param name="key">缓存Key</param>
        ///// <param name="value">新的缓存Value</param>
        ///// <param name="expiresIn">缓存时长</param>
        ///// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
        ///// <returns></returns>
        //Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false);
        #endregion
    }
}
