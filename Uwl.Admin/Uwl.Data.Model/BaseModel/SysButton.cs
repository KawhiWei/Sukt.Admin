using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Uwl.Data.Model.BaseModel
{
    /// <summary>
    /// 按钮实体类
    /// </summary>
    [Serializable]
    public class SysButton : Entity
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 调用事件
        /// </summary>
        public string KeyCode { get; set; }
        /// <summary>
        /// 调用APIAction方法
        /// </summary>
        public string APIAddress { get; set; }
        /// <summary>
        /// 按钮颜色/样式
        /// </summary>
        [MaxLength(320)]
        public string ButtonStyle { get; set; }
        /// <summary>
        /// 隶属菜单
        /// </summary>
        public Guid MenuId { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; } = false;
        /// <summary>
        /// 按钮排序先后
        /// </summary>
        public int Sort { get; set; } = 0;
    }
}
