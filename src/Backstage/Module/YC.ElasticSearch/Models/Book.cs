using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.ElasticSearch.Models
{
    public class Book
    {
        public string Id { get; set; }

        /// <summary>
        /// 指定映射为KeyWord 不进行text 映射【不分词】
        /// </summary>
       [Keyword(Name ="bookName")]
        public string BookName { get; set; }

        public string BookContent { get; set; }
        public string Auther { get; set; }
        public DateTime PublishDate { get; set; }

        public double Price { get; set; }
    }
}
