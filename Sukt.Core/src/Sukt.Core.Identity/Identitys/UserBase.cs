using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sukt.Core.Identity
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public abstract class UserBase<TUserKey> : EntityBase<TUserKey>
         where TUserKey : IEquatable<TUserKey>
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [DisplayName("用户名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [DisplayName("登录账号")]
        public string NormalizedUserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [DisplayName("用户昵称")]
        public string NickName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [DisplayName("电子邮箱"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// 标准化的电子邮箱
        /// </summary>
        [DisplayName("标准化的电子邮箱"), DataType(DataType.EmailAddress)]
        public string NormalizeEmail { get; set; }

        /// <summary>
        /// 电子邮箱确认
        /// </summary>
        [DisplayName("电子邮箱确认")]
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 密码哈希值
        /// </summary>
        [DisplayName("密码哈希值")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [DisplayName("用户头像")]
        public string HeadImg { get; set; }

        /// <summary>
        /// 安全标识
        /// </summary>
        [DisplayName("安全标识")]
        public string SecurityStamp { get; set; }

        /// <summary>
        /// 安全标识
        /// </summary>
        [DisplayName("安全标识")]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 手机号码确定
        /// </summary>
        [DisplayName("手机号码确定")]
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 双因子身份验证
        /// </summary>
        [DisplayName("双因子身份验证")]
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
        [DisplayName("锁定时间")]
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// 是否登录锁
        /// </summary>
        [DisplayName("是否登录锁")]
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// 登录失败次数
        /// </summary>
        [DisplayName("登录失败次数")]
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 是否系统账号
        /// </summary>
        [DisplayName("是否系统账号")]
        public bool IsSystem { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public string Sex { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return UserName;
        }
    }
}