using AutoMapper;
using Sukt.Core.Domain.Models.Menu;
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Dtos.Menu
{
    /// <summary>
    /// 加载菜单路由模型
    /// </summary>
    [AutoMap(typeof(MenuEntity))]
    [DisplayName("加载菜单路由模型")]
    public class RouterMenuOutput : OutputDtoBase<Guid>
    {
        /// <summary>
        /// 菜单图标
        /// </summary>
        [DisplayName("菜单图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 菜单对应子应用
        /// </summary>
        [DisplayName("菜单对应子应用")]
        public string MicroName { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        [DisplayName("父级菜单ID")]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [DisplayName("菜单名称")]
        public string Name { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        [DisplayName("路由")]
        public string Path { get; set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        [DisplayName("组件地址")]
        public string Component { get; set; }

        /// <summary>
        /// 菜单类型（菜单/按钮）
        /// </summary>
        [DisplayName("菜单类型")]
        public MenuEnum Type { get; set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        [DisplayName("组件名称")]
        public string ComponentName { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [DisplayName("是否显示")]
        public bool IsShow { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 按钮事件
        /// </summary>
        [DisplayName("按钮事件")]
        public string ButtonClick { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        [DisplayName("子级")]
        public List<RouterMenuOutput> Children { get; set; } = new List<RouterMenuOutput>();

        /// <summary>
        /// tab页
        /// </summary>
        [DisplayName("tab页")]
        public List<RouterMenuOutput> Tabs { get; set; } = new List<RouterMenuOutput>();

        /// <summary>
        /// 按钮列表
        /// </summary>
        [DisplayName("按钮列表")]
        public List<RouterMenuOutput> Buttons { get; set; } = new List<RouterMenuOutput>();
    }
}