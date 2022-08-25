using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YC.ServiceWebApi.Result;
using ServiceWebApi.Dto;
using YC.Common;
using YC.Common.ShareUtils;
using YC.ApplicationService;
using Newtonsoft.Json.Linq;
using YC.ServiceWebApi.Tenant;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using YC.Core;
using YC.Model.DbEntity;

using YC.Core;

using MongoDB.Driver;
using YC.MongoDB;
using YC.Core.Cache;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using YC.ServiceWebApi.Dto;
using Autofac.Extras.DynamicProxy;
using YC.Core.Autofac;
using YC.ServiceWebApi.Filter;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YC.ServiceWebApi.Controllers
{
    /// <summary>
    /// 接口服务身份认证
    /// </summary>
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    public class IdentityController : BaseController
    {
        public readonly ISysUserAppService _sysUserService;
        public readonly IUserManager _userManager;
        public ICacheManager _cacheManager;

        public IdentityController(ISysUserAppService sysUserService, IUserManager userManager, ICacheManager cacheManager)
        {
            _sysUserService = sysUserService;
            _userManager = userManager;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 获取token，通过登录
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="pwd">用户密码</param>
        /// <returns>返回登录结果</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult GetTokenByLogin([FromBody] LoginUserDto loginUserDto)
        {
           // Test test = new Test();
           //var data= test.GetUser();
           // NatashaRepository natashaRepository = new NatashaRepository();
           // natashaRepository.NatashaDoWork();

            //登录，先去数据库做验证，成功了，说明可以进行token创建，往payLoad字典中加入,如果没有传TenantId 默认就为默认租户
            //IApiResult<UserDto> result = _userManager.UserLogin(loginUserDto.UserId, loginUserDto.Pwd, loginUserDto.GuidKey, loginUserDto.ValidateCode, loginUserDto.TenantId == 0 ? 1 : loginUserDto.TenantId);
            //改造版本，支持中央用户库，再到具体租户库
            IApiResult<UserDto> result = _userManager.UserLogin(loginUserDto.UserId, loginUserDto.Pwd, loginUserDto.GuidKey, loginUserDto.ValidateCode, 0);

            return new JsonResult(result);
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="token">原始token</param>
        /// <returns></returns>
        [HttpPost]
        public IApiResult RefreshToken(string token)
        {
            var msg = "";
            var tokenStr = _userManager.RefreshToken(token);
            var state = string.IsNullOrWhiteSpace(tokenStr) ? false : true;
            msg = state == true ? "刷新token获取成功！" : "token过期,请重新登录！";
            return ApiResult.Result<string>(state, tokenStr, msg);
        }

        /// <summary>
        /// 上传图片,通过Form表单提交
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public IApiResult UploadImage([FromForm] UploadFileDto uploadFileDto)
        {
            var file = uploadFileDto.File;
            if (file == null)
            {
                return ApiResult.NotOk("文件为空！");
            }
            //返回的文件地址
            string filename = "";
            var now = DateTime.Now;
            //文件存储路径
            var filePath = string.Format("/Uploads/{0}/{1}/{2}/", now.ToString("yyyy"), now.ToString("yyyyMM"), now.ToString("yyyyMMdd"));
            //获取当前web目录
            var webRootPath = System.Environment.CurrentDirectory;
            if (!Directory.Exists(webRootPath + filePath))
            {
                Directory.CreateDirectory(webRootPath + filePath);
            }

            try
            {
                if (file != null)
                {
                    #region 图片文件的条件判断

                    //文件后缀
                    var fileExtension = Path.GetExtension(file.FileName);

                    //判断后缀是否是图片
                    const string fileFilt = ".gif|.jpg|.jpeg|.png";
                    if (fileExtension == null)
                    {
                        //break;
                        return ApiResult.NotOk("上传的文件没有后缀!");
                    }
                    if (fileFilt.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                    {
                        //break;
                        return ApiResult.NotOk("请上传jpg、png、gif格式的图片!");
                    }

                    //判断文件大小
                    long length = file.Length;
                    if (length > 1024 * 1024 * 2) //2M
                    {
                        //break;
                        return ApiResult.NotOk("上传的文件不能大于2M!");
                    }

                    #endregion 图片文件的条件判断

                    var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                    var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                    var saveName = strDateTime + strRan + fileExtension;

                    //插入图片数据
                    using (FileStream fs = System.IO.File.Create(webRootPath + filePath + saveName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    filename = filePath + saveName;
                }

                var user = _userManager.LoginUser;
                var updateResult = _sysUserService.UpdateUserAvatar(user.Id, filename);
                if (updateResult.State)
                {
                    string imgStr = ImageUtils.GetBase64FromImage(webRootPath + filename);
                    return ApiResult.Ok<string>(imgStr);
                }
                else
                {
                    return updateResult;
                }
                //return ApiResult.Ok("上传成功!");
            }
            catch (Exception ex)
            {
                return ApiResult.NotOk("上传失败!" + ex.ToString());
            }
        }

        [HttpPost]
        public IApiResult DefaultUploadImage()
        {
            //var file = Request.Form.Files;
            var result = _sysUserService.UploadUserAvatar(Request.Form.Files);
            return result;
        }

        /// <summary>
        /// 返回一个guid
        /// </summary>
        /// <returns></returns>
        //[ServiceFilter(typeof(SeckillFilterAttribute))]
        [HttpPost]
        public IApiResult GetGuid()
        {
            return ApiResult.Result<string>(true, Guid.NewGuid().ToString(), "");
        }

        /// <summary>
        /// 返回图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetImage(string path)
        {
            var imgPath = System.Environment.CurrentDirectory + path;
            //从图片中读取byte
            var imgByte = System.IO.File.ReadAllBytes(imgPath);
            //从图片中读取流
            var imgStream = new MemoryStream(System.IO.File.ReadAllBytes(imgPath));
            var fileExtension = Path.GetExtension(imgPath);
            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(imgStream)
                //或者
                //Content = new ByteArrayContent(imgByte)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/" + fileExtension);
            return resp;
        }

        /// <summary>
        /// 返回验证码如图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetVerificationCode(string guidKey)
        {
            if (string.IsNullOrWhiteSpace(guidKey))
            {
                throw new Exception(DefaultConfig.DefaultAppConfig.ExceptionKey + "验证码指定Key不存在！");
            }
            string verificationCode = "";
            var imageMemoryStream = VerificationCodeUtils.CreateVerificationCodeImage(out verificationCode);
            // _userManager.SetSession(DefaultConfig.SESSION_VERIFICATIONCODE, verificationCode);
            _cacheManager.Add(guidKey, verificationCode);
            var bytes = imageMemoryStream.GetBuffer();
            return new FileContentResult(bytes, "image/jpeg");
        }
    }
}