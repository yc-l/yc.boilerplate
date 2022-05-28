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
    /// 区块链存证Dto
    public partial class BCEvidenceCreateDto
    {
       /// <summary>
///事务Id
/// </summary>
public String ServiceId {get;set;}
/// <summary>
///存证类别
/// </summary>
public String TypeName {get;set;}
/// <summary>
///存证数据
/// </summary>
public String DataValue {get;set;}

 /// <summary>
///主键Id
/// </summary>
public string Id {get;set;}

    }

    public partial class BCEvidenceAddOrEditDto
    {
        /// <summary>
        ///事务Id
        /// </summary>
        public String ServiceId { get; set; }
        /// <summary>
        ///存证类别
        /// </summary>
        public String TypeName { get; set; }
        /// <summary>
        ///存证数据
        /// </summary>
        public String DataValue { get; set; }
        /// <summary>
        ///存证状态
        /// </summary>
        public Int32 EvidenceState { get; set; }
        /// <summary>
        ///存证提示信息
        /// </summary>
        public String Message { get; set; }
        /// <summary>
        ///交易Hash
        /// </summary>
        public String TranscationHash { get; set; }
        /// <summary>
        ///存证操作时间
        /// </summary>
        public DateTime OperaEvidenceDate { get; set; }
        /// <summary>
        ///主键Id
        /// </summary>
        public string Id { get; set; }

    }
}


