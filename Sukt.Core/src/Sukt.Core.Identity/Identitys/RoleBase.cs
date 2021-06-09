using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Identity
{
    public abstract class RoleBase<TRoleKey> : EntityBase<TRoleKey>
          where TRoleKey : IEquatable<TRoleKey>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [DisplayName("角色名称")]
        public string Name { get; set; }

        /// <summary>
        /// 标准化角色名称
        /// </summary>
        [DisplayName("标准化角色名称")]
        public string NormalizedName { get; set; }

        /// <summary>
        /// 版本标识
        /// </summary>
        [DisplayName("版本标识")]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

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