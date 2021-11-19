using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using YC.Common.ShareUtils;
using YC.Micro.Configuration;
using YC.Micro.Consul;
using static YC.Micro.UserWebService.IUserService;

namespace YC.Micro.AggregateServiceWebApi.Controllers
{
    [Route("AggregateService/Api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IServiceDiscovery _serviceDiscovery;
        public BaseController(IServiceDiscovery serviceDiscovery) {
            _serviceDiscovery = serviceDiscovery;
        }
        
        public Metadata TokenHeader
        {
            get
            {
                var task = Task.Run(async () =>
                {
                    return await GetHeadersWithToken();
                });
                return task.Result;
            }
        }

        /// <summary>
        /// 内部方法，如果public swagger 会认为接口，要求标识[HttpGet]等这样的标识，
        /// 要不然会报错，查看详细错误：https://localhost:5000/swagger/v1/swagger.json
        /// </summary>
        /// <returns></returns>
        protected async Task<Metadata> GetHeadersWithToken()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var headers = new Metadata { { "Authorization", $"Bearer  {accessToken}" } };
            return headers;
        }

      
        protected  GrpcChannel GetGrpcChannelByNoCheckTls(string serviceName) {

            string url = _serviceDiscovery.GetLoadBalanceActivityServiceAddress(serviceName);
            //consul 配置注册 需要使用前面用到的_userServiceClient注入对象，使用WithHost 改变地址来使用

            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            // 1、建立连接
            GrpcChannel grpcChannel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { HttpHandler = httpHandler });

            return grpcChannel;
        }

      
        protected GrpcChannel GetGrpcChannelBySslCert(string serviceName)
        {
           
            string url = _serviceDiscovery.GetLoadBalanceActivityServiceAddress(serviceName);
            var credentials = CallCredentials.FromInterceptor( async(context, metadata) =>
            {
                metadata = await GetHeadersWithToken();
            });

            var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
            });
            return channel;
        }

       
    }
}