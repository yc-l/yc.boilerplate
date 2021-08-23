using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using YC.Model.DbEntity;

namespace YC.Model.SysDbEntity
{
   
    public partial class SysDataDictionary
    {
        #region 自定义属性，即由数据实体扩展的实体                
        #endregion
    }
    [Display(Name = "数据字典")]
    [Table("Sys_DataDictionary")]
    public partial class SysDataDictionary : CreateEntity<long>
    {
        #region Declarations
        [Display(Name = "编码")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]
    
        public string Key { get; set; }

        [Display(Name = "名称")]
        [StringLength(50, ErrorMessage = "{0}不能超过32个字符！")]
        public string Label { get; set; }
        

        [Display(Name = "父节点")]
        public long? ParentId { get; set; }

        [Display(Name = "类型")]
        public int Type { get; set; }

        [Display(Name = "助记符")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]
        public string Memoni { get; set; }


        [Display(Name = "排序")]
        public int? Sort { get; set; }


        [Display(Name = "备注")]
        [StringLength(128, ErrorMessage = "{0}不能超过128个字符！")]
        public string Value { get; set; }


        #endregion
    }
}
