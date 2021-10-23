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


namespace YC.Micro.AggregateServiceWebApi.Controllers
{

    public class IndexController : BaseController
    {

        private readonly ILogger<IndexController> _logger;
        private readonly IUserServiceClient _userServiceClient;
        private readonly IBookServiceClient _bookServiceClient;
        public IndexController(ILogger<IndexController> logger, IUserServiceClient userServiceClient,
            IBookServiceClient bookServiceClient)
        {
            _logger = logger;
            _userServiceClient = userServiceClient;
            _bookServiceClient = bookServiceClient;
        }
           
        [HttpGet]
        public async Task< UserDtoList>  GetUserListAsync(int id)
        {
           
            var list = await _userServiceClient.GetUserListAsync(new UserFormRequest
            {
                Id = id
            });
            return list;
        }

        [HttpGet]
        public async Task<BookDtoList> GetBookListAsync(string queryFilterString, int currentPage, int pageSize)
        {
            var list = await _bookServiceClient.GetBookListAsync(new BookFormRequest
            {
                QueryFilterString = queryFilterString,
                CurrentPage = currentPage,
                PageSize = pageSize
            });
            return list;
        }

    }
}
