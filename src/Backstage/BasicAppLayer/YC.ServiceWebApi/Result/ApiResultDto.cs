using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YC.ServiceWebApi.Result
{
    /// <summary>
    /// 统一返回接口数据
    /// </summary>
    /// <typeparam name="T">特定返回的数据</typeparam>
    public class ApiResultDto<T>
    {

        /// <summary>
        /// 状态标识
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回的特定接口数据
        /// </summary>
        public T Value { get; set; }


    }

}
