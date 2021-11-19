using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.Micro.UserWebService;
using static YC.Micro.UserWebService.IUserService;
using static YC.Micro.AggregateServiceWebApi.IBookService;
using YC.Micro.AggregateServiceWebApi;
using YC.Micro.Core;
using YC.Micro.Consul;
using YC.Micro.Configuration;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;

namespace YC.Micro.AggregateServiceWebApi.Controllers
{
    [Authorize("ApiScope")]
    public class IndexController : BaseController
    {
        private readonly ILogger<IndexController> _logger;
        private  IUserServiceClient _userServiceClient;
        private  IBookServiceClient _bookServiceClient;
        private readonly ClientInterceptor _clientInterceptor;
        private readonly ILoadBalance _loadBalance;

        public IndexController(ILogger<IndexController> logger, IUserServiceClient userServiceClient,
            IBookServiceClient bookServiceClient, ClientInterceptor clientInterceptor, IServiceDiscovery serviceDiscovery,
             ILoadBalance loadBalance):base(serviceDiscovery)
        {
            _logger = logger;
            _userServiceClient = userServiceClient;
            _bookServiceClient = bookServiceClient;
            _clientInterceptor = clientInterceptor;
            _loadBalance = loadBalance;
        }

        [HttpGet]
        public async Task<UserDtoList> GetUserListAsync(int id)
        {
            // 直接配置的，那么就采用这种，
            //var list = await _userServiceClient.GetUserListAsync(new UserFormRequest
            //{
            //    Id = id
            //}, TokenHeader);

            // 覆盖，使用不校验tls
            _userServiceClient = new IUserServiceClient(GetGrpcChannelByNoCheckTls(ServiceRegisterConfig.UserServiceName));

        var list = await _userServiceClient.GetUserListAsync(new UserFormRequest
            {
                Id = id
            }, TokenHeader);
            return list;
        }

        [HttpGet]
        public async Task<BookDtoList> GetBookListAsync(string queryFilterString, int currentPage, int pageSize)
        {

            _bookServiceClient = new IBookServiceClient(GetGrpcChannelBySslCert(ServiceRegisterConfig.BookServiceName));
            var list = await _bookServiceClient.GetBookListAsync(new BookFormRequest
            {
                QueryFilterString = queryFilterString,
                CurrentPage = currentPage,
                PageSize = pageSize
            }, TokenHeader);
            return list;
        }
    }
}