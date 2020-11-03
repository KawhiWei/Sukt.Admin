using System;

namespace Sukt.Core.Shared.Entity
{
    /// <summary>
    /// 修改人和修改时间接口
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public interface IModifyAudited<TUserKey> where TUserKey : struct
    {
        /// <summary>
        /// 最后修改人Id
        /// </summary>
        TUserKey? LastModifyId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime LastModifedAt { get; set; }
    }
}