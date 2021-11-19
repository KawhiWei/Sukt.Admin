using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.Menu
{
    [DisplayName("功能模块")]
    public class FunctionEntity : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public FunctionEntity()
        {
            Id = SuktGuid.NewSuktGuid();
        }

        public FunctionEntity(string name, string description, bool isEnabled, string linkUrl):this()
        {
            Name = name;
            Description = description;
            IsEnabled = isEnabled;
            LinkUrl = linkUrl;
        }
        public void SetFiled(string name, string description, bool isEnabled, string linkUrl)
        {
            Name = name;
            Description = description;
            IsEnabled = isEnabled;
            LinkUrl = linkUrl;
        }

        [DisplayName("功能名称")]
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; private set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [DisplayName("是否可用")]
        public bool IsEnabled { get; private set; }

        /// <summary>
        /// 链接Url
        /// </summary>
        [DisplayName("链接Url")]
        public string LinkUrl { get; private set; }
        #region 公共字段

        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [DisplayName("最后修改人")]
        public Guid? LastModifyId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DisplayName("最后修改时间")]
        public DateTimeOffset? LastModifedAt { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }

        #endregion 公共字段
    }
}