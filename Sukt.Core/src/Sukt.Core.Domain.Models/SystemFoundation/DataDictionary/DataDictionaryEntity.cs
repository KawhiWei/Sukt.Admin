using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.SystemFoundation.DataDictionary
{
    [DisplayName("数据字典")]
    public class DataDictionaryEntity : EntityBase<Guid>, IFullAuditedEntity<Guid>
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

        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        [DisplayName("修改人ID")]
        public Guid? LastModifyId { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public virtual DateTime LastModifedAt { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
    }
}