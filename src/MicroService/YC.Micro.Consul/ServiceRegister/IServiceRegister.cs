namespace YC.Micro.Consul.ServiceRegister
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public interface IServiceRegister
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        void Register();

        /// <summary>
        /// 撤销服务
        /// </summary>
        void Deregister();
    }
}
