using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Model
{
   public class CreateOrModifiedEntity<T>: CreateEntity<T> 
    {
        [DisplayName(" 最后编辑时间")]
        public DateTime LastModificationTime { get; set; }
        [DisplayName(" 最后编辑人ID")]
        public T LastModifierUserId { get; set; }
    }
}
