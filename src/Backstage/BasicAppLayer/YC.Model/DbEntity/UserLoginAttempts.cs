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

    [Table("UserLoginAttempts")]
    public partial class UserLoginAttempts : FullEntity<long>
    {
        #region Declarations
     
        [DisplayName("租户ID")]
        public virtual int? TenantId { get; set; }


        [DisplayName("租户名")]
        [StringLength(64)]
        public virtual string TenancyName { get; set; }
        [DisplayName("用户ID")]

        public virtual long? UserId { get; set; }


        [DisplayName("用户名或者邮箱地址")]
        [StringLength(255)]
        public virtual string UserNameOrEmailAddress { get; set; }


        [DisplayName("客户端IP地址")]
        [StringLength(64)]
        public virtual string ClientIpAddress { get; set; }


        [DisplayName("客户端名称")]
        [StringLength(128)]
        public virtual string ClientName { get; set; }


        [DisplayName("浏览器信息")]
        [StringLength(256)]
        public virtual string BrowserInfo { get; set; }

        [DisplayName("结果")]
        public virtual byte Result { get; set; }

        [DisplayName("创建时间")]
        public virtual DateTime CreationTime { get; set; }


        #endregion
    }
}
