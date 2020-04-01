using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Entity
{
    /// <summary>
    /// 创建人和创建时间
    /// </summary>
    /// <typeparam name="TUserKey"></typeparam>
    public interface ICreatedAudited<TUserKey> where TUserKey:struct
    {
        /// <summary>
        /// 创建人Ｉｄ
        /// </summary>
        TUserKey? CreatedId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedAt { get; set; }
    }
 

}
