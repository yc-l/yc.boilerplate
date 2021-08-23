using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YC.Model.DbEntity;

namespace YC.Model.SysDbEntity
{

    /// <summary>
    /// 权限
    /// </summary>
	[Table("Sys_Permission")]
    [Display(Name = "权限表")]
    public class SysPermission : CreateOrModifiedEntity<long>
    {
        /// <summary>
        /// 父级节点
        /// </summary>
        [Display(Name = "父级节点")]
        public long ParentId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Display(Name = "权限名称")]
        [StringLength(50)]
        public string Label { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [StringLength(550)]
        [Display(Name = "权限编码")]
        public string Code { get; set; }

        /// <summary>
        /// 权限类型
        /// 分组（ Group = 1）、菜单（ Menu = 2）、接口（ Api = 3）、权限点（ Dot = 4）
        /// </summary>
        [Display(Name = "权限类型")]
        public int Type { get; set; }

        /// <summary>
        /// 视图
        /// </summary>
        [Display(Name = "视图")]
        public string View { get; set; }


        /// <summary>
        /// 接口
        /// </summary>
        [Display(Name = "接口")]
        public string Api { get; set; }


        /// <summary>
        /// 菜单访问地址
        /// </summary>
        [Display(Name = "菜单访问地址")]
        [StringLength(500)]
        public string Path { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Display(Name = " 图标")]
        [StringLength(100)]
        public string Icon { get; set; }

        /// <summary>
        /// 隐藏
        /// </summary>
        [Display(Name = " 是否隐藏")]
        public bool Hidden { get; set; } = false;



        /// <summary>
        /// 可关闭
        /// </summary>
        [Display(Name = " 可关闭")]
        public bool? Closable { get; set; }

        /// <summary>
        /// 打开组
        /// </summary>
        [Display(Name = " 打开组")]
        public bool? Opened { get; set; }

        /// <summary>
        /// 打开新窗口
        /// </summary>
        [Display(Name = " 打开新窗口")]
        public bool? NewWindow { get; set; }

        /// <summary>
        /// 链接外显
        /// </summary>
        [Display(Name = " 链接外显")]
        public bool? External { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = " 排序")]
        public int? Sort { get; set; } = 0;

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = " 描述")]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
