using Microsoft.AspNetCore.Identity;
using Sukt.Module.Core.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.Identity
{
    public abstract class UserNickNameValidator<TUser, TUserKey> : IUserValidator<TUser>
         where TUser : UserBase<TUserKey>
         where TUserKey : IEquatable<TUserKey>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            IdentityResult result = IdentityResult.Success;
            TUser existUser = manager.Users.FirstOrDefault(m => m.NickName == user.NickName);
            if (existUser != null
             && (Equals(user.Id, default(TUserKey))
             || !Equals(user.Id, existUser.Id)))
            {
                result = new IdentityResult().Failed($"昵称为“{user.NickName}”的用户已存在，请更换昵称重试");
            }
            return Task.FromResult(result);
        }
    }
}