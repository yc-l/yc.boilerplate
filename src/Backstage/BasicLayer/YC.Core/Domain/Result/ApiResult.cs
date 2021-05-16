using Newtonsoft.Json;

namespace YC.Core
{
    /// <summary>
    /// 响应数据输出
    /// </summary>
    public class ApiResult<T> : IApiResult<T>
    {
        /// <summary>
        /// 是否成功标记
        /// </summary>
        [JsonIgnore]
        public bool State { get; private set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int Code => State ? 200 : 400;

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
        public ApiResult<T> Ok(T data, string msg = null)
        {
            State = true;
            Data = data;
            Message = msg;

            return this;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public ApiResult<T> NotOk(string msg = null, T data = default(T))
        {
            State = false;
            Message = msg;
            Data = data;

            return this;
        }
    }

    /// <summary>
    /// 响应数据静态输出
    /// </summary>
    public static partial class ApiResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static IApiResult Ok<T>(T data = default(T), string msg = null)
        {
            return new ApiResult<T>().Ok(data, msg);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <returns></returns>
        public static IApiResult Ok()
        {
            return Ok<string>();
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static IApiResult NotOk<T>(string msg = null, T data = default(T))
        {
            return new ApiResult<T>().NotOk(msg, data);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static IApiResult NotOk(string msg = null)
        {
            return new ApiResult<string>().NotOk(msg);
        }

        /// <summary>
        /// 根据布尔值返回结果
        /// </summary>
        /// <param name="success"></param>
        /// <returns></returns>
        public static IApiResult Result<T>(bool success)
        {
            return success ? Ok<T>() : NotOk<T>();
        }

        /// <summary>
        /// 根据布尔值返回结果
        /// </summary>
        /// <param name="success"></param>
        /// <returns></returns>
        public static IApiResult Result(bool success)
        {
            return success ? Ok() : NotOk();
        }

        public static IApiResult Result<T>(bool success, T data)
        {
            return success ? Ok<T>(data) : NotOk<T>(data: data);
        }

        public static IApiResult Result<T>(bool success, T data, string msg)
        {
            return success ? Ok<T>(data, msg) : NotOk<T>(msg, data);
        }
    }
}
