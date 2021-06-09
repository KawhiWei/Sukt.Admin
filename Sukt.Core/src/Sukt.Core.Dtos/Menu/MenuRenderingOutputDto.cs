using AutoMapper;
using Sukt.Core.Domain.Models.Menu;
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Dtos.Menu
{
    /// <summary>
    /// 菜单动态路由输出Dto
    /// </summary>
    [DisplayName("菜单动态路由")]
    [AutoMap(typeof(MenuEntity))]
    public class MenuRenderingOutputDto : OutputDtoBase<Guid>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [DisplayName("菜单名称")]
        public string Name { get; set; }

        /// <summary>
        /// 路由地址(前端)
        /// </summary>
        [DisplayName("路由")]
        public string Path { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        [DisplayName("父级菜单ID")]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [DisplayName("菜单图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 当前菜单以上所有的父级
        /// </summary>
        [DisplayName("当前菜单以上所有的父级")]
        public string ParentNumber { get; set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        [DisplayName("组件地址")]
        public string Component { get; set; }

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
        /// 当前菜单对应的子应用
        /// </summary>
        [DisplayName("菜单对应子应用")]
        public string MicroName { get; set; }

        /// <summary>
        /// 菜单类型（菜单/按钮）
        /// </summary>
        [DisplayName("菜单类型")]
        public MenuEnum Type { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public List<MenuRenderingOutputDto> Children = new List<MenuRenderingOutputDto>();

        /// <summary>
        /// tab页
        /// </summary>
        public List<MenuRenderingOutputDto> Tabs = new List<MenuRenderingOutputDto>();

        /// <summary>
        /// 按钮列表
        /// </summary>
        public List<MenuRenderingOutputDto> Buttons = new List<MenuRenderingOutputDto>();
    }
}