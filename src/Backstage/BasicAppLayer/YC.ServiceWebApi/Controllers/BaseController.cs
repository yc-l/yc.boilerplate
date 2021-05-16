using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YC.ApplicationService;
using YC.Core;

namespace YC.ServiceWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public abstract class BaseController : ControllerBase
    {
       
    }
}
