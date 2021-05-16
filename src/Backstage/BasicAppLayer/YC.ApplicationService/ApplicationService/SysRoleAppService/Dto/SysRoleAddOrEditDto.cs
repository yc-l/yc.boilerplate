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
    /// 角色Dto
    public partial class SysRoleAddOrEditDto
    {
        /// <summary>
        ///名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        ///助记符
        /// </summary>
        public String Memoni { get; set; }
        /// <summary>
        ///排序
        /// </summary>
        public Nullable<Int32> Sort { get; set; }
        /// <summary>
        ///主键Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public string[] PermissionIds { get; set; }

    }
}


