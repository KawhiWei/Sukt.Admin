using Sukt.Core.Domain.Models.Menu;
using Sukt.Module.Core.Attributes.AutoMapper;
using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Dtos.Function
{
    [SuktAutoMapper(typeof(FunctionEntity))]
    public class FunctionInputDto : InputDto<Guid>
    {
        [DisplayName("功能名称")]
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [DisplayName("是否可用")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 链接Url
        /// </summary>
        [DisplayName("链接Url")]
        public string LinkUrl { get; set; }
    }
}