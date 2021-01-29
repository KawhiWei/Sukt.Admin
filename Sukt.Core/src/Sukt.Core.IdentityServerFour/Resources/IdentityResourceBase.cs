using Sukt.Core.IdentityServerFour.Resources;
using Sukt.Core.Shared;
using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 身份资源
    /// </summary>
    [DisplayName("身份资源")]
    public abstract class IdentityResourceBase : ResourceBase, IAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
    
        /// <summary>
        /// 是否必须
        /// </summary>
        [Description("是否必须")]
        public bool Required { get; set; }

        /// <summary>
        /// 是否强调显示
        /// </summary>
        [Description("是否强调显示")]
        public bool Emphasize { get; set; }
        /// <summary>
        /// 是否不可编辑
        /// </summary>
        [Description("是否不可编辑")]
        public bool NonEditable { get; set; }
    }
}