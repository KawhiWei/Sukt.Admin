using AutoMapper;
using Sukt.Core.Domain.Models.SystemFoundation.DataDictionary;
using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Dtos.DataDictionaryDto
{
    [AutoMap(typeof(DataDictionaryEntity))]
    public class DataDictionaryOutDto : OutputDtoBase<Guid>
    {
        /// <summary>
        /// 数据字典标题
        /// </summary>
        [DisplayName("数据字典标题")]
        public string Title { get; set; }

        /// <summary>
        /// 数据字典值
        /// </summary>
        [DisplayName("数据字典值")]
        public string Value { get; set; }

        /// <summary>
        /// 数据字典备注
        /// </summary>
        [DisplayName("数据字典备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 数据字典父级
        /// </summary>
        [DisplayName("数据字典父级")]
        public Guid ParentId { get; set; } = Guid.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        ///获取或设置 编码
        /// </summary>
        [DisplayName("唯一编码")]
        public string Code { get; set; }
    }
}