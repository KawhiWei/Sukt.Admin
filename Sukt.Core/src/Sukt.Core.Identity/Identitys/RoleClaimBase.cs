using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;
using System.Security.Claims;

namespace Sukt.Core.Identity
{
    /// <summary>
    /// 角色声明
    /// </summary>
    /// <typeparam name="TRoleKey"></typeparam>
    public abstract class RoleClaimBase<TRoleKey> : EntityBase<Guid>
            where TRoleKey : IEquatable<TRoleKey>
    {
        [DisplayName("角色编号")]
        public TRoleKey RoleId { get; set; }

        [DisplayName("声明类型")]
        public string ClaimType { get; set; }

        [DisplayName("声明值")]
        public string ClaimValue { get; set; }

        public virtual Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }

        /// <summary>
        /// 使用一个声明对象初始化
        /// </summary>
        /// <param name="other">声明对象</param>
        public virtual void InitializeFromClaim(Claim other)
        {
            ClaimType = other?.Type;
            ClaimValue = other?.Value;
        }
    }
}