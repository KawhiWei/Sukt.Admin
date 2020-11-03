using Sukt.Core.IdentityServerFour.Resources;
using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// api资源
    /// </summary>
    [DisplayName("api资源")]
    public abstract class ApiResourceBase : ResourceBase, IEntity<Guid>
    {
        /// <summary>
        /// 允许的访问令牌登录算法
        /// </summary>
        [DisplayName("允许的访问令牌登录算法")]
        public string AllowedAccessTokenSigningAlgorithms { get; set; }

        ///// <summary>
        ///// 密钥
        ///// </summary>
        //[DisplayName("密钥")]
        //public List<ApiResourceSecret> Secrets { get; set; }

        ///// <summary>
        ///// 授权范围
        ///// </summary>
        //[DisplayName("授权范围")]
        //public List<ApiResourceScope> Scopes { get; set; }

        ///// <summary>
        ///// 用户声明
        ///// </summary>
        //[DisplayName("用户声明")]
        //public List<ApiResourceClaim> UserClaims { get; set; }

        ///// <summary>
        ///// 属性
        ///// </summary>
        //[DisplayName("属性")]
        //public List<ApiResourceProperty> Properties { get; set; }
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