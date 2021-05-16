using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//----------------------------------------------
//简介：YC.ApplicationService  Dto 数据传输对象
//
//
//
//auther：林宣名
//----------------------------------------------
namespace YC.ApplicationService.Dto
{
    /// 权限Dto
    public partial class SysPermissionAddOrEditDto
    {
        /// <summary>
        ///父级节点
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        ///权限名称
        /// </summary>
        public String Label { get; set; }
        /// <summary>
        ///权限编码
        /// </summary>
        public String Code { get; set; }
        /// <summary>
        ///权限类型
        /// </summary>
        public Int32 Type { get; set; }
        /// <summary>
        ///视图
        /// </summary>
        public string View { get; set; }
        /// <summary>
        ///接口
        /// </summary>
        public string Api{ get; set; }
        /// <summary>
        ///菜单访问地址
        /// </summary>
        public String Path { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public String Icon { get; set; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public Boolean Hidden { get; set; }
        /// <summary>
        /// 可关闭
        /// </summary>
        public Nullable<Boolean> Closable { get; set; }
        /// <summary>
        /// 打开组
        /// </summary>
        public Nullable<Boolean> Opened { get; set; }
        /// <summary>
        /// 打开新窗口
        /// </summary>
        public Nullable<Boolean> NewWindow { get; set; }
        /// <summary>
        /// 链接外显
        /// </summary>
        public Nullable<Boolean> External { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Nullable<Int32> Sort { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        ///主键Id
        /// </summary>
        public string Id { get; set; }

    }
}


