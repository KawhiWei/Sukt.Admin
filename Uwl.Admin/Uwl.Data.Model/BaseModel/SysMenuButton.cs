using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.BaseModel
{
    /// <summary>
    /// 菜单按钮类,用于保存这个按钮依附于那个菜单
    /// </summary>
    [Serializable]
    public class SysMenuButton: Entity
    {
        public Guid MenuId { get; set; }
        /// <summary>
        /// 按钮ID
        /// </summary>
        public Guid ButtonId { get; set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 调用事件
        /// </summary>
        public string KeyCode { get; set; }
        /// <summary>
        /// 调用API接口
        /// </summary>
        public string APIAddress { get; set; }
        /// <summary>
        /// 按钮颜色/样式
        /// </summary>
        public string ButtonStyle { get; set; }

    }
}
