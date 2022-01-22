using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YC.ServiceWebApi.Controllers
{
    /// <summary>
    /// 域控制器
    /// </summary>
   //[Authorize]
   //[AllowAnonymous]
    public abstract class AreaController : BaseController
    {
    }
}
