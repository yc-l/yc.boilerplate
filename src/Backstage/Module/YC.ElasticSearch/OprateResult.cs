using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.ElasticSearch
{
    public class OprateResult<T>
    {

    
        /// <summary>
        /// 是否成功标记
        /// </summary>

        public bool State { get; private set; }

     
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="msg">消息</param>
        public static OprateResult<T> Ok( string msg = null, T data= default(T))
        {
           var result= new OprateResult<T>();
            result.State = true;
            result.Message = msg;
            result.Data = data;
            return result;
        }

  
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static OprateResult<T> NoOk(string msg = null, T data = default(T))
        {
            var result = new OprateResult<T>();
            result.State = false;
            result.Message = msg;
            result.Data = data;
            return result;
        }

       

    }
}
