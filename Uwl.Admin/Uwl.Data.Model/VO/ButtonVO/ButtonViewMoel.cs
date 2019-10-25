using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.VO.ButtonVO
{
    /// <summary>
    /// 按钮ViewModel
    /// </summary>
    public class ButtonViewMoel
    {
        /// <summary>
        /// 内置ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuId { get; set; }
        /// <summary>
        /// API接口
        /// </summary>
        public string APIAddress { get; set; }
        /// <summary>
        /// JS事件
        /// </summary>
        public string KeyCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 按钮颜色/样式
        /// </summary>
        public string ButtonStyle { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; } = false;
        /// <summary>
        /// 创建时间//   类型后面加问号代表可以为空
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// 创建时间前端页面展示
        /// </summary>
        public string CreateAts => CreatedDate.HasValue ? CreatedDate.Value.ToString("yyyy-MM-dd") : "";
    }
}
