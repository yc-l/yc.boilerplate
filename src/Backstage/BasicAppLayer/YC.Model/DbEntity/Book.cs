using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Model.DbEntity
{
    public class Book
    {
        public string Id { get; set; }
        
        public string BookName { get; set; }

        public string BookContent { get; set; }
        public string Auther { get; set; }
        public DateTime PublishDate { get; set; }

        public double Price { get; set; }
    }
}
