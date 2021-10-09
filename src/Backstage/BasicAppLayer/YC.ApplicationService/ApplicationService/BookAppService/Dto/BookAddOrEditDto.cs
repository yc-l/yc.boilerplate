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
    public partial class BookAddOrEditDto
    {
       public String Id {get;set;}
public String BookName {get;set;}
public String BookContent {get;set;}
public String Auther {get;set;}
public DateTime PublishDate {get;set;}
public Double Price {get;set;}
public DateTime CreateDate {get;set;}

    }
}


