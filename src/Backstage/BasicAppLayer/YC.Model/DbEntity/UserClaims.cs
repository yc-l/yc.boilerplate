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

    [Table("UserClaims")]
    public partial class UserClaims : FullEntity<long>
    {
        #region Declarations
        [DisplayName("租户ID")]
        public virtual int? TenantId { get; set; }
        [DisplayName("用户ID")]

        public virtual long UserId { get; set; }


        [DisplayName("凭证类别")]
        [StringLength(518)]
        public virtual string ClaimType { get; set; }


        [DisplayName("凭证内容")]
        [StringLength(518)]
        public virtual string ClaimValue { get; set; }

        [DisplayName("创建时间")]
        public virtual DateTime CreationTime { get; set; }

        [DisplayName("创建者ID")]
        public virtual long? CreatorUserId { get; set; }


        #endregion
    }
}
