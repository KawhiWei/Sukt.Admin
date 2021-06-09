using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;

namespace Sukt.Core.Dtos.DataDictionaryDto
{
    public class TreeDictionaryOutDto : OutputDtoBase<Guid>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 是否展开直子节点
        /// </summary>
        public bool expand { get; set; }

        /// <summary>
        /// 禁掉响应
        /// </summary>
        public string disabled { get; set; }

        /// <summary>
        /// 组织架构深度
        /// </summary>
        public string Depth { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 当前节点以上所有组织架构
        /// </summary>
        public string ParenNumber { get; set; }

        /// <summary>
        /// 组织架构标题
        /// </summary>
        public List<TreeDictionaryOutDto> Children { get; set; } = new List<TreeDictionaryOutDto>();
    }
}