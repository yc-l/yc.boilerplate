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
    //获取主键Id的命名

    /// 
    [Table("UserLogins")]
    public partial class UserLogins : FullEntity<long>
    {
        #region Declarations

        [DisplayName("用户ID")]
        public virtual long UserId { get; set; }


        [DisplayName("登录提供者")]
        [StringLength(128)]
        public virtual string LoginProvider { get; set; }


        [DisplayName("提供者秘钥")]
        [StringLength(256)]
        public virtual string ProviderKey { get; set; }

        [DisplayName("租户ID")]
        public virtual int? TenantId { get; set; }


        #endregion
    }
}
