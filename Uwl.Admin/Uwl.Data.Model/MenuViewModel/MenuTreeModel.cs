using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uwl.Data.Model.VO.ButtonVO;

namespace Uwl.Data.Model.MenuViewModel
{
    /// <summary>
    /// 定义一个Vue前端路由实体类
    /// </summary>
    public class RouterBar
    {
        public RouterBar()
        {
            children = new List<RouterBar>();
        }
        public Guid Id { get; set; }
        public string name { get; set; }
        public Guid? ParentId { get; set; }
        //图标名称
        public string iconCls { get; set; }
        /// <summary>
        /// 前端路由地址
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// API路由地址
        /// </summary>
        public string APIAddress { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int? order { get; set; }
        public NavigationBarMeta meta { get; set; }
        //加载子级菜单
        public List<RouterBar> children { get; set; }
        /// <summary>
        /// 菜单下的按钮
        /// </summary>
        public List<BtnIsDisplayViewModel> btnIsDisplayViews { get; set; } = new List<BtnIsDisplayViewModel>();
    }
    /// <summary>
    /// 定义一个Vue路由属性类
    /// </summary>
    public class NavigationBarMeta
    {
        public string title { get; set; }
        public bool requireAuth { get; set; } = true;
    }
}
