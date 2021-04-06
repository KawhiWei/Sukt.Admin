using System;
using System.Collections.Generic;

namespace Sukt.Core.Dtos.TreeDto
{
    /// <summary>
    /// 通用树形Dto
    /// </summary>
    public class CurrencyTreeDto
    {
        /// <summary>
        /// 唯一值
        /// </summary>
        public Guid Key { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 将树禁用
        /// </summary>
        public bool Disabled { get; set; } = false;
        /// <summary>
        /// 子级
        /// </summary>
        public List<CurrencyTreeDto> Children { get; set; }
    }
}
