using AutoMapper;
using Sukt.Core.Domain.Models.Menu;
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Dtos
{
    /// <summary>
    /// 树形结构统一输出Dto
    /// </summary>
    public class TreeOutputDto : OutputDtoBase<Guid>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Key => Id.ToString();
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 所有父级
        /// </summary>
        public string ParentNumbers { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public List<TreeOutputDto> Children = new List<TreeOutputDto>();
    }
}