using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YC.Core.Domain
{
    [IsMappedToCreateAttrbute(IsCreate.Yes)]
    [Table("Order")]
    public class Order
    {
        [DisplayName("主键ID")]
        [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("订单类别")]
        [Required]
        [StringLength(100)]
        public String OrderType { get; set; }
        [DisplayName("订单信息")]
        [Required]
        [StringLength(100)]
        public string OrderInfo { get; set; }
        [DisplayName("订单时间")]
        [Required]
        [StringLength(100)]
        public DateTime OrderDate { get; set; }
        
        public Car Car { get; set; }

        

    }
}
