using System.ComponentModel;

namespace Sukt.Core.Domain.Models.Menu
{
    /// <summary>
    /// 菜单类型枚举
    /// </summary>
    public enum MenuEnum
    {
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        MenuType = 0,

        /// <summary>
        /// Tab页
        /// </summary>
        [Description("Tab页")]
        Tab = 5,

        /// <summary>
        /// 按钮
        /// </summary>
        [Description("按钮")]
        Button = 10,
    }
}