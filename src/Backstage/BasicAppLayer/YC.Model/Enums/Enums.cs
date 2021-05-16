using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YC.Model
{
    public enum Sex : byte
    {
        [Display(Name = "男")]
        Male = 0,
        [Display(Name = "女")]
        Female = 1
    }

    public enum UserType : byte
    {
        [Display(Name = "机构用户")]
        SysUser = 0,
        [Display(Name = "企业用户")]
        BasUser = 1
    }

    public enum OrganRangeType : byte
    {
        [Display(Name = "本组织")]
        Oneself = 0,
        [Display(Name = "本组织及以下")]
        Branch = 1,
        [Display(Name = "任意组织")]
        Any = 2,
        [Display(Name = "可选组织")]
        Multiple = 3

    }

    public enum Industry : byte
    {
        [Display(Name = "商场")]
        Mall = 0,
        [Display(Name = "酒店")]
        Hotel = 1,
        [Display(Name = "学校")]
        School = 2,
        [Display(Name = "医院")]
        Hospital = 3,
        [Display(Name = "办公楼")]
        Office = 4,
        [Display(Name = "酒吧KTV")]
        Bar = 5,
        [Display(Name = "部队")]
        Army = 6,
        [Display(Name = "其他")]
        Other = 255


    }

    public enum PeriodKind : byte
    {
        [Display(Name = "每天")]
        Day = 0,
        [Display(Name = "每周")]
        Week = 1,
        [Display(Name = "每月")]
        Month = 2,
        [Display(Name = "每年")]
        Year = 3
    }

    
    public enum ItemType : byte
    {
        [Display(Name = "简单判别")]
        Logic = 0,
        [Display(Name = "数值类型")]
        Digital = 1
    }

    public enum TrueFalse : byte
    {
        [Display(Name = "是")]
        True = 1,
        [Display(Name = "否")]
        False = 0
    }

    public enum NormalState : byte
    {
        [Display(Name = "正常")]
        True = 1,
        [Display(Name = "异常")]
        False = 0
    }

    public enum ExecutorType : byte
    {
        [Display(Name = "部门")]
        DEPT = 0,
        [Display(Name = "个人")]
        POST = 1,
    }

    public enum StatueType : byte
    {

        [Display(Name = "已处理")]
        AlreadyDeal = 1,
        [Display(Name = "未处理")]
        UnDeal = 0,
    }

    public enum LoginType : byte
    {
        [Display(Name = "登录")]
        Login = 0,
        [Display(Name = "登出")]
        Logout = 1,
    }

    /// <summary>
    /// 巡检任务状态
    /// 应检:100以内的
    /// 已完成:大于2小于100以内
    /// 免检:100及以上，如节假日、停业整顿，不算在应检范围
    /// </summary>
    public enum TaskType : byte
    {
        [Display(Name = "未开始")]
        UnStart = 0,
        [Display(Name = "进行中")]
        OnGoing = 1,
        [Display(Name = "进行中（异常）")]
        OnGoingHaveBug = 2,
        [Display(Name = "已完成")]
        Finish = 3,
        [Display(Name = "已完成（异常）")]
        FinishHaveBug = 4,
        [Display(Name = "系统异常")]
        SystemFailure = 5,
        [Display(Name = "节假日")]
        Holiday = 100
    }

    public enum RoleType : byte
    {
        [Display(Name = "所属角色")]
        OwnRole=0,
        [Display(Name = "可分配角色")]
        AssignableRole=1
    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// 分组
        /// </summary>
        [Display(Name = "分组")]
        Group = 1,
        /// <summary>
        /// 菜单
        /// </summary>
        [Display(Name = "菜单")]
        Menu = 2,
        /// <summary>
        /// 操作点
        /// </summary>
        [Display(Name = "操作点")]
        Operation = 3,

    }
}
