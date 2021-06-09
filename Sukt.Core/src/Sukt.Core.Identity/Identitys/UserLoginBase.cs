using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Identity
{
    public abstract class UserLoginBase<TUserKey> : EntityBase<Guid>
           where TUserKey : IEquatable<TUserKey>
    {
        [DisplayName("登录的登录提供程序")]
        public string LoginProvider { get; set; }

        [DisplayName("第三方用户的唯一标识")]
        public string ProviderKey { get; set; }

        [DisplayName("第三方用户昵称")]
        public string ProviderDisplayName { get; set; }

        [DisplayName("用户编号")]
        public TUserKey UserId { get; set; }
    }
}