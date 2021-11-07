using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Core;

namespace YC.ElasticSearch.Models
{
    [Table("books")]//这个只是为了代码生成器的标识
    public class Book
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("主键Id")]
        public string Id { get; set; }

        /// <summary>
        /// 指定映射为KeyWord 不进行text 映射【不分词】
        /// </summary>
        [Keyword(Name ="bookName")]
        [DisplayName("书名")]
        public string BookName { get; set; }
        [DisplayName("内容")]
        public string BookContent { get; set; }
        [DisplayName("作者")]
        public string Auther { get; set; }
        [DisplayName("出版时间")]
        public DateTime PublishDate { get; set; }
        [DisplayName("价格")]
        public double Price { get; set; }
        [DisplayName("创建时间")]
        public DateTime CreateDate { get; set; }
    }
}
