using System.ComponentModel;

namespace Sukt.Core.Domain.Models.Identity.Enum
{
    /// <summary>
    /// 用户类型
    /// </summary>
    [Description("用户类型")]
    public enum UserTypeEnum
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        SuperAdmin = 0,
        /// <summary>
        /// 普通管理员
        /// </summary>
        [Description("普通管理员")]
        GeneralAdmin = 5,
        /// <summary>
        /// 普通用户
        /// </summary>
        [Description("普通用户")]
        OrdinaryUser = 10,
    }
}
