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
    /// 流程管理Dto
    public partial class ProcessFlowAddOrEditDto
    {
       /// <summary>
///流程内容
/// </summary>
public String FlowContent {get;set;}
/// <summary>
///流程类别
/// </summary>
public Int32 Type {get;set;}
/// <summary>
///流程名称
/// </summary>
public String Name {get;set;}
/// <summary>
///状态
/// </summary>
public Boolean Enabled {get;set;}
 /// <summary>
///主键Id
/// </summary>
public string Id {get;set;}

    }
}


