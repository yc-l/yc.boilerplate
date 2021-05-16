using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YC.Model.DbEntity;

//----------------------------------------------
//简介：YC.Model.DbEntity  Entity 数据库对应实体
//
//
//
//auther：林宣名
//----------------------------------------------
namespace YC.Model.DbEntity
{

    [Table("UserAccounts")]
    public partial class UserAccounts : FullEntity<long>
    {
        #region Declarations

        [DisplayName("租户ID")]
        public virtual int? TenantId { get; set; }

        [DisplayName("用户ID")]
        public virtual long UserId { get; set; }

        [DisplayName("用户连接ID")]
        public virtual long? UserLinkId { get; set; }


        [DisplayName("用户名称")]
        [StringLength(100)]
        public virtual string UserName { get; set; }


        [DisplayName("邮箱地址")]
        [StringLength(128)]
        public virtual string EmailAddress { get; set; }
        [DisplayName("最后登录时间")]
        public virtual DateTime? LastLoginTime { get; set; }

        #endregion
    }
}
