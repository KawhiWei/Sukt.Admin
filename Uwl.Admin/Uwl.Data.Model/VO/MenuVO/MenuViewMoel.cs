using System;
using System.Collections.Generic;
using System.Text;

namespace Uwl.Data.Model.VO.MenuVO
{
    public class MenuViewMoel
    {
        /// <summary>
        /// 内置ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父级菜单
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 父级菜单ID
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// API接口
        /// </summary>
        public string APIAddress { get; set; }
        /// <summary>
        /// 前端路由地址
        /// </summary>
        public string UrlAddress { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 创建时间//   类型后面加问号代表可以为空
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// 该菜单的所有父级
        /// </summary>
        public string ParentIdArr { get; set; }
        /// <summary>
        /// 创建时间前端页面展示
        /// </summary>
        public string CreateAts => CreatedDate.HasValue ? CreatedDate.Value.ToString("yyyy-MM-dd") : "";
    }
}
