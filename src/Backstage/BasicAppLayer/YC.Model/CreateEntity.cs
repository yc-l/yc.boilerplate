using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Model.DbEntity;

namespace YC.Model
{
   public  class CreateEntity<T>: BaseEntity<T> 
    {
        [DisplayName(" 创建ID")]
        public T CreatorUserId { get; set; }

       

        [DisplayName(" 创建时间")]
        public DateTime? CreationTime { get; set; }
    }
}
