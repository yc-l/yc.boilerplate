using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace YC.Core.Domain.Entity
{
   public class BaseEntity
    {
        [DisplayName("是否活动状态")]
        public bool IsActive { get; set; }
        [DisplayName(" 是否删除状态")]
        public bool IsDeleted { get; set; }

        [DisplayName(" 最后编辑时间戳")]
        public long LastModificationTimeStamp { get; set; }
        [DisplayName(" 最后编辑人ID")]
        public string LastModifierUserId { get; set; }
        [DisplayName(" 删除时间")]
        public long DeletionTimeStamp { get; set; }
        [DisplayName(" 最后删除人ID")]
        public string DeleterUserId { get; set; }
        [DisplayName(" 创建时间")]
        public long CreationTimeStamp { get; set; }

        [DisplayName(" 最后创建ID")]
        public string CreatorUserId { get; set; }


    }
}
