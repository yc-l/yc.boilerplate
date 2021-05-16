using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YC.Core.Domain
{
    [Table("Car")]
    public class Car
    {
        #region DatabaseFields
        //System.ComponentModel.DataAnnotations.Key
        [Key]
       [Column("Id")]
       [StringKeyToGuidAttrbute]
        [DisplayName("序列ID")]
        public string CarId { get; set; }

        [DisplayName("标注")]
        [Required(ErrorMessage = "{0}是必填项")]
        public string Make { get; set; }
        [DisplayName("模型")]
        public string Model { get; set; }

        
        [DisplayName("最后操作时间")]
        public DateTime LastDate { get; set; }
        //[DisplayName("金额")]
        //[Range(typeof(decimal), "1000.00", "2000.99")]
       
        //[DecimalPrecision(18,2)]
        //public decimal AmountInfo { get; set; }


        //[DisplayName("字符信息")]
        //public char CharInfo { get; set; }
        #endregion

        #region RelatedTables
       
        #endregion

        #region AdditionalFields
        [NotMapped]
        [Editable(false)]
        public string MakeWithModel { get { return Make + " (" + Model + ")"; } }
        #endregion

    }
}
