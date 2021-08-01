using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Identity
{
    public abstract class UserRoleBase<TUserKey, TRoleKey> : EntityBase<Guid>
          where TUserKey : IEquatable<TUserKey>
          where TRoleKey : IEquatable<TRoleKey>
    {
    }
}