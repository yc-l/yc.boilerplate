using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YC.Model.DbEntity;

namespace YC.Model.SysDbEntity
{
    [Table("Sys_Organization")]
    [Display(Name = "组织机构")]
    public partial class SysOrganization : CreateEntity<long>
    {
        #region Declarations
        [Display(Name = "名称")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]
       
        //[Default(DefaultType.Method, "GetName")]
        public string Label { get; set; }

        [Display(Name = "所属上级")]
        public long? ParentId { get; set; }

        [Display(Name = "节点类型")]
        public int? OrganType { get; set; }

        [Display(Name = "序号")]
        public int? Sort { get; set; }

        [Display(Name = "岗位编号")]
        public int? PostId { get; set; }

        [Display(Name = "传真")]
        [StringLength(16, ErrorMessage = "{0}不能超过16个字符！")]
        public string Fax { get; set; }

        [Display(Name = "联系电话")]
        [StringLength(16, ErrorMessage = "{0}不能超过16个字符！")]
        [RegularExpression(@"\d{3}-\d{7,8}|\d{4}-\d{7,8}|1[3|4|5|6|7|8|9][0-9]{9}", ErrorMessage = "{0}格式错误！")]
        public string Telephone { get; set; }

        [Display(Name = "通讯地址")]
        [StringLength(64, ErrorMessage = "{0}不能超过64个字符！")]
        public string Address { get; set; }


        [Display(Name = "助记符")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]
        public string Memoni { get; set; }

        [Display(Name = "备注")]
        [StringLength(128, ErrorMessage = "{0}不能超过128个字符！")]
        public string Remark { get; set; }

        [Display(Name = "权限")]
       
        public int? RangeType { get; set; }

        [Display(Name = "权限范围")]
        [StringLength(128, ErrorMessage = "{0}不能超过128个字符！")]
        public string Range { get; set; }


        [Display(Name = "联系人")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]
        public string Linkman { get; set; }


        #endregion
    }
    public partial class SysOrganization
    {
        #region 自定义属性，即由数据实体扩展的实体


        #endregion
    }
}
