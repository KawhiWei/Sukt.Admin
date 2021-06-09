using Sukt.Module.Core;
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Sukt.Core.Domain.Models.IdentityServerFour
{
    /// <summary>
    /// 身份资源
    /// </summary>
    [DisplayName("身份资源")]
    public class IdentityResource : AggregateRootBase<Guid>/* IdentityResourceBase*/, IFullAuditedEntity<Guid>
    {
        public IdentityResource(string name, string displayName, bool required)
        {
            Name = name;
            DisplayName = displayName;
            Required = required;
        }
        public void AddUserClaims(List<string> userclaims)
        {
            if (UserClaims == null)
                UserClaims = new List<IdentityResourceClaim>();
            UserClaims.AddRange(userclaims.Select(x => new IdentityResourceClaim(x)));
        }
        public void SetEmphasize(bool emphasize)
        {
            Emphasize = emphasize;
        }

        /// <summary>
        /// 是否必须
        /// </summary>
        [Description("是否必须")]
        public bool Required { get; private set; }

        /// <summary>
        /// 是否强调显示
        /// </summary>
        [Description("是否强调显示")]
        public bool Emphasize { get; private set; }
        /// <summary>
        /// 是否不可编辑
        /// </summary>
        [Description("是否不可编辑")]
        public bool NonEditable { get; private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool Enabled { get; private set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; private set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [DisplayName("显示名称")]
        public string DisplayName { get; private set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; private set; }
        /// <summary>
        /// 是否显示在发现文档中
        /// </summary>
        [DisplayName("是否显示在发现文档中")]
        public bool ShowInDiscoveryDocument { get; private set; }
        /// <summary>
        /// 用户声明
        /// </summary>
        [Description("用户声明")]
        public List<IdentityResourceClaim> UserClaims { get; private set; }
        /// <summary>
        /// 属性
        /// </summary>
        [Description("属性")]
        public List<IdentityResourceProperty> Properties { get; private set; }
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

        #endregion 公共字段
    }
}
