using System.Text.Json.Serialization;

namespace YC.Core
{
    /// <summary>
    /// 响应数据输出接口
    /// </summary>
    public interface IApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonIgnore]
        bool State { get; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get;}
    }

    /// <summary>
    /// 响应数据输出泛型接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiResult<T> : IApiResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        T Data { get; }
    }
}
