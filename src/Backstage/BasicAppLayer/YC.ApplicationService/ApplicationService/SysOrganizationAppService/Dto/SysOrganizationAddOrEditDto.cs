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
    /// 组织机构Dto
    public partial class SysOrganizationAddOrEditDto
    {
       /// <summary>
///名称
/// </summary>
public String Label {get;set;}
/// <summary>
///所属上级
/// </summary>
public Nullable<Int64> ParentId {get;set;}
/// <summary>
///节点类型
/// </summary>
public Nullable<Int32> OrganType {get;set;}
/// <summary>
///序号
/// </summary>
public Nullable<Int32> Sort {get;set;}
/// <summary>
///岗位编号
/// </summary>
public Nullable<Int32> PostId {get;set;}
/// <summary>
///传真
/// </summary>
public String Fax {get;set;}
/// <summary>
///联系电话
/// </summary>
public String Telephone {get;set;}
/// <summary>
///通讯地址
/// </summary>
public String Address {get;set;}
/// <summary>
///助记符
/// </summary>
public String Memoni {get;set;}
/// <summary>
///备注
/// </summary>
public String Remark {get;set;}
/// <summary>
///权限
/// </summary>
public Nullable<Int32> RangeType {get;set;}
/// <summary>
///权限范围
/// </summary>
public String Range {get;set;}
/// <summary>
///联系人
/// </summary>
public String Linkman {get;set;}
 /// <summary>
///主键Id
/// </summary>
public string Id {get;set;}

    }
}


