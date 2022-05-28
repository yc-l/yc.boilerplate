using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Model.BlockChainEntity
{
    public partial class BCEvidence
    {
        #region 自定义属性，即由数据实体扩展的实体                
        #endregion
    }
    [Display(Name = "区块链存证")]
    [Table("BC_Evidence")]
    public partial class BCEvidence : CreateEntity<long>
    {
        #region Declarations
        [Display(Name = "事务Id")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]

        public string ServiceId { get; set; }

        [Display(Name = "存证类别")]
        [StringLength(50, ErrorMessage = "{0}不能超过32个字符！")]
        public string TypeName { get; set; }

        [Display(Name = "存证数据")]
        [StringLength(2000, ErrorMessage = "{0}不能超过2000个字符！")]
        public string DataValue { get; set; }

        [Display(Name = "存证状态")]
       
        public int EvidenceState { get; set; }
        [Display(Name = "存证提示信息")]
        [StringLength(50, ErrorMessage = "{0}不能超过50个字符！")]
        public string Message { get; set; }

        [Display(Name = "交易Hash")]
        [StringLength(300, ErrorMessage = "{0}不能超过300个字符！")]
        public string TranscationHash { get; set; }

        [Display(Name = "存证操作时间")]
        public DateTime OperaEvidenceDate { get; set; }
        #endregion
    }
}
