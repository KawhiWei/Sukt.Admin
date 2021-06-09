using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Sukt.Core.Identity
{
    public abstract class UserClaimBase<TUserKey> : EntityBase<Guid>
            where TUserKey : IEquatable<TUserKey>
    {
        [DisplayName("用户编号")]
        public TUserKey UserId { get; set; }

        [Required]
        [DisplayName("声明类型")]
        public string ClaimType { get; set; }

        [DisplayName("声明值")]
        public string ClaimValue { get; set; }

        public virtual Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }

        public virtual void InitializeFromClaim(Claim other)
        {
            ClaimType = other?.Type;
            ClaimValue = other?.Value;
        }
    }
}