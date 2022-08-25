using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Core;

namespace YC.Model
{


    public partial class ProcessFlow
    {
        #region 自定义属性，即由数据实体扩展的实体                
        #endregion
    }
    [Display(Name = "流程管理")]
    [Table("WF_ProcessFlow")]
    public partial class ProcessFlow : CreateEntity<string>
    {
        #region Declarations
        [Display(Name = "流程内容")]
        [StringLength(32, ErrorMessage = "{0}不能超过32个字符！")]
        [StringToTextAttrbute]
        public string FlowContent { get; set; }

        [Display(Name = "流程类别")]
        [StringLength(50, ErrorMessage = "{0}不能超过50个字符！")]
        public int Type { get; set; }

        [Display(Name = "流程名称")]
        [StringLength(50, ErrorMessage = "{0}不能超过50个字符！")]
        public string Name { get; set; }

        [Display(Name = "启用状态")]
        public bool Enabled { get; set; }

        #endregion
    }
}
