using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Model.DbEntity;

namespace YC.Model
{
  public abstract class ModifiedEntity<T> : BaseEntity<T> 
    {
        [DisplayName(" 最后编辑时间")]
        public DateTime LastModificationTime { get; set; }
        [DisplayName(" 最后编辑人ID")]
        public T LastModifierUserId { get; set; }
    }
}
