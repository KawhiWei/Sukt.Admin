using Sukt.Core.IdentityServerFour.Resources;
using Sukt.Core.Shared;
using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// api资源
    /// </summary>
    [DisplayName("api资源")]
    public abstract class ApiResourceBase : ResourceBase, IAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 允许的访问令牌登录算法
        /// </summary>
        [DisplayName("允许的访问令牌登录算法")]
        public string AllowedAccessTokenSigningAlgorithms { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        [DisplayName("最后访问时间")]
        public DateTime? LastAccessed { get; set; }

        /// <summary>
        /// 是否不可编辑
        /// </summary>
        [DisplayName("是否不可编辑")]
        public bool NonEditable { get; set; }

        
    }
}