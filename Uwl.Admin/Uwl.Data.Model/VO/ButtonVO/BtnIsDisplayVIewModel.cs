using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.VO.ButtonVO
{
    /// <summary>
    /// 菜单按钮渲染VO
    /// </summary>
    public class BtnIsDisplayViewModel
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 按钮样式
        /// </summary>
        public string BtnStyle { get; set; }
        /// <summary>
        /// 按钮是否显示属性
        /// </summary>
        public string KeyCode { get; set; }
    }
}
