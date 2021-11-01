using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Identity
{
    public abstract class RoleBase<TRoleKey> : EntityBase<TRoleKey>
          where TRoleKey : IEquatable<TRoleKey>
    {
        protected RoleBase(string name, string normalizedName, bool isAdmin)
        {
            Name = name;
            NormalizedName = normalizedName;
            IsAdmin = isAdmin;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [DisplayName("角色名称")]
        public string Name { get; private set; }

        /// <summary>
        /// 标准化角色名称
        /// </summary>
        [DisplayName("标准化角色名称")]
        public string NormalizedName { get; private set; }

        /// <summary>
        /// 版本标识
        /// </summary>
        [DisplayName("版本标识")]
        public string ConcurrencyStamp { get; private set; } = Guid.NewGuid().ToString();
        public void SetNormalizedName(string normalizedName)
        {
            NormalizedName = normalizedName;
        }
        public void SetName(string name)
        {
            Name = name;
        }
        public void SetFunc(string name, string normalizedName, bool isAdmin)
        {
            Name = name;
            NormalizedName = normalizedName;
            IsAdmin = isAdmin;
        }

        /// <summary>
        /// 是否管理员
        /// </summary>
        [DisplayName("是否管理员")]
        public bool IsAdmin { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}