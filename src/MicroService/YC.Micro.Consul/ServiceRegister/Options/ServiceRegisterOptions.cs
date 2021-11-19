using AutoMapper.Configuration;
using System;
using YC.Micro.Configuration;

namespace YC.Micro.Consul.ServiceRegister.Options
{
    /// <summary>
    /// 节点注册选项
    /// </summary>
    public class ServiceRegisterOptions
    {
        public ServiceRegisterOptions()
        {
            var configuration = DefaultConfig.GetAppsettingsConfigurationEntity<ServiceRegisterSetting>();

            this.ServiceId = configuration.ServiceName + "_" + Guid.NewGuid().ToString();
            this.ServiceName = configuration.ServiceName;
            this.ServiceTags = configuration.ServiceTags;
            this.ServiceAddress = configuration.ServiceAddress;
            this.ConsulAddress = configuration.ConsulAddress;
        }

        // 服务ID
        public string ServiceId { get; set; }

        // 服务名称
        public string ServiceName { get; set; }

        // 服务地址
        public string ServiceAddress { get; set; }

        // 服务标签(版本)
        public string[] ServiceTags { set; get; }

        // 服务注册地址
        public string ConsulAddress { get; set; }
    }
}