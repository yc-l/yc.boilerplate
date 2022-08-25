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
    /// 租户管理Dto
    public partial class SysTenantDto
    {
         public string TypeName { get; set; }
        /// <summary>
///租户名称
/// </summary>
public String TenantName {get;set;}
 /// <summary>
///数据库类别
/// </summary>
public Int32 DbType {get;set;}
 /// <summary>
///数据库连接字符串
/// </summary>
public String DbConnectionString {get;set;}
 /// <summary>
///是否启用
/// </summary>
public Boolean IsActived {get;set;}
 /// <summary>
///是否默认租户
/// </summary>
public Boolean IsDefaultTenant {get;set;}
 /// <summary>
///主键Id
/// </summary>
public string Id {get;set;}

    }
}


