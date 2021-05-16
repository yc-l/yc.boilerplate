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
    /// 数据字典Dto
    public partial class SysDataDictionaryDto
    {
         public string TypeName { get; set; }
       /// <summary>
///编码
/// </summary>
public String Key {get;set;}
/// <summary>
///名称
/// </summary>
public String Label {get;set;}
/// <summary>
///父节点
/// </summary>
public string ParentId {get;set;}
/// <summary>
///类型
/// </summary>
public Int32 Type {get;set;}
/// <summary>
///助记符
/// </summary>
public String Memoni {get;set;}
/// <summary>
///排序
/// </summary>
public Nullable<Int32> Sort {get;set;}
/// <summary>
///备注
/// </summary>
public String Value {get;set;}
 /// <summary>
///主键Id
/// </summary>
public string Id {get;set;}

    }
}


