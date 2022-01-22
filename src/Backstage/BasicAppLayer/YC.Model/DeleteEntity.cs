using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Model.DbEntity;

namespace YC.Model
{
   public abstract class DeleteEntity<T>: BaseEntity<T> 
    {
        [DisplayName(" 删除时间")]
        public DateTime? DeletionTime { get; set; }
        [DisplayName(" 最后删除人ID")]
        public T DeleterUserId { get; set; }
    }
}
