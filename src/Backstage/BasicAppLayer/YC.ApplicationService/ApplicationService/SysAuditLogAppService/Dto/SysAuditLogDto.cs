using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//----------------------------------------------
//简介：YC.ApplicationService  Dto 数据传输对象
//
//
//
//auther：林宣名
//----------------------------------------------
namespace YC.ApplicationService.Dto
{
    /// Dto
    public partial class SysAuditLogDto
    {
         public string TypeName { get; set; }
        /// <summary>
///主键Id
/// </summary>
public string Id {get;set;}
 /// <summary>
///拦截Key
/// </summary>
public String Key {get;set;}
 /// <summary>
///IP
/// </summary>
public String IP {get;set;}
 /// <summary>
///浏览器
/// </summary>
public String Browser {get;set;}
 /// <summary>
///操作系统
/// </summary>
public String Os {get;set;}
 /// <summary>
///设备
/// </summary>
public String Device {get;set;}
 /// <summary>
///浏览器信息
/// </summary>
public String BrowserInfo {get;set;}
 /// <summary>
///耗时（毫秒）
/// </summary>
public Int64 ElapsedMilliseconds {get;set;}
 /// <summary>
///用户id
/// </summary>
public Int64 UserId {get;set;}
 /// <summary>
///用户账号
/// </summary>
public String UserAccount {get;set;}
 /// <summary>
///请求参数
/// </summary>
public String ParamsString {get;set;}
 /// <summary>
///开始执行时间
/// </summary>
public DateTime StartTime {get;set;}
 /// <summary>
///结束执行时间
/// </summary>
public DateTime StopTime {get;set;}
 /// <summary>
///请求方式
/// </summary>
public String RequestMethod {get;set;}
 /// <summary>
///请求Api
/// </summary>
public String RequestApi {get;set;}

    }
}


