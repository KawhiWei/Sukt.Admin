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
        protected UserBase(string userName, string normalizedUserName, string nickName, string email, 
            string normalizeEmail, bool emailConfirmed, string passwordHash, string headImg, string securityStamp, 
            string concurrencyStamp, string phoneNumber, bool phoneNumberConfirmed, bool twoFactorEnabled, 
            DateTimeOffset? lockoutEnd, bool lockoutEnabled, int accessFailedCount, bool isSystem, string sex)
        {

            UserName = userName;
            NormalizedUserName = normalizedUserName;
            NickName = nickName;
            Email = email;
            NormalizeEmail = normalizeEmail;
            EmailConfirmed = emailConfirmed;
            PasswordHash = passwordHash;
            HeadImg = headImg;
            SecurityStamp = securityStamp;
            ConcurrencyStamp = concurrencyStamp;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            TwoFactorEnabled = twoFactorEnabled;
            LockoutEnd = lockoutEnd;
            LockoutEnabled = lockoutEnabled;
            AccessFailedCount = accessFailedCount;
            IsSystem = isSystem;
            Sex = sex;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string UserName { get; private set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [DisplayName("登录账号")]
        public string NormalizedUserName { get; private set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [DisplayName("用户昵称")]
        public string NickName { get; private set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [DisplayName("电子邮箱"), DataType(DataType.EmailAddress)]
        public string Email { get; private set; }

        /// <summary>
        /// 标准化的电子邮箱
        /// </summary>
        [DisplayName("标准化的电子邮箱"), DataType(DataType.EmailAddress)]
        public string NormalizeEmail { get; private set; }

        /// <summary>
        /// 电子邮箱确认
        /// </summary>
        [DisplayName("电子邮箱确认")]
        public bool EmailConfirmed { get; private set; }

        /// <summary>
        /// 密码哈希值
        /// </summary>
        [DisplayName("密码哈希值")]
        public string PasswordHash { get; private set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [DisplayName("用户头像")]
        public string HeadImg { get; private set; }

        /// <summary>
        /// 安全标识
        /// </summary>
        [DisplayName("安全标识")]
        public string SecurityStamp { get; private set; }

        /// <summary>
        /// 安全标识
        /// </summary>
        [DisplayName("安全标识")]
        public string ConcurrencyStamp { get; private set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// 手机号码确定
        /// </summary>
        [DisplayName("手机号码确定")]
        public bool PhoneNumberConfirmed { get; private set; }

        /// <summary>
        /// 双因子身份验证
        /// </summary>
        [DisplayName("双因子身份验证")]
        public bool TwoFactorEnabled { get; private set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
        [DisplayName("锁定时间")]
        public DateTimeOffset? LockoutEnd { get; private set; }

        /// <summary>
        /// 是否登录锁
        /// </summary>
        [DisplayName("是否登录锁")]
        public bool LockoutEnabled { get; private set; }

        /// <summary>
        /// 登录失败次数
        /// </summary>
        [DisplayName("登录失败次数")]
        public int AccessFailedCount { get; private set; }

        /// <summary>
        /// 是否系统账号
        /// </summary>
        [DisplayName("是否系统账号")]
        public bool IsSystem { get; private set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public string Sex { get; private set; }
        public void SetPasswordHash(string passwordHash)
        {
            this.PasswordHash = passwordHash;
        }
        public void SetSecurityStamp(string securityStamp)
        {
            this.SecurityStamp = securityStamp;
        }
        public void SetEmailConfirmed(bool emailConfirmed)
        {
            this.EmailConfirmed = emailConfirmed;
        }
        public void SetNormalizeEmail(string normalizeEmail)
        {
            this.NormalizeEmail = normalizeEmail;
        }
        
        public void SetPhoneNumberConfirmed(bool phoneNumberConfirmed)
        {
            this.PhoneNumberConfirmed = phoneNumberConfirmed;
        }
        public void SetNormalizedUserName(string normalizedUserName)
        {
            this.NormalizedUserName = normalizedUserName;
        }
        public void SetUserName(string userName)
        {
            this.UserName = userName;
        }
        public void SetEmail(string email)
        {
            this.Email = email;
        }
        public void SetLockoutEnd(DateTimeOffset? lockoutEnd)
        {
            this.LockoutEnd = lockoutEnd;
        }
        public void SetLockoutEnabled(bool lockoutEnabled)
        {
            this.LockoutEnabled = lockoutEnabled;
        }
        public void SetAccessFailedCount()
        {
            this.AccessFailedCount ++;
        }
        public void ResetAccessFailedCount(int accessFailedCount)
        {
            this.AccessFailedCount= accessFailedCount;
        }
        public void SetPhoneNumber(string phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }
        public void SetTwoFactorEnabled(bool twoFactorEnabled)
        {
            this.TwoFactorEnabled = twoFactorEnabled;
        }
        




    }
}