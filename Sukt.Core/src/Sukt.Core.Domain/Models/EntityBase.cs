using Sukt.Core.Shared.EntityBase;
using Sukt.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Domain.Models
{
    public class EntityBase<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 所有实体主键
        /// </summary>
        public TKey Id { get; set; }
        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if(obj==null)
            {
                return false;
            }
            if(!(obj is EntityBase<TKey> entity))//判断obj是否是派生自EntityBase
            {
                return false;
            }

            return base.Equals(obj);
        }
        /// <summary>
        /// 重写HashCode方法
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #region 私有帮助方法
        // <summary>
        /// 实体ID是否相等
        /// </summary>
        public static bool IsKeyEqual(TKey id1, TKey id2)
        {
            if (id1 == null && id2 == null)
            {
                return true;
            }
            if (id1 == null || id2 == null)
            {
                return false;
            }

            Type type = typeof(TKey);
            if (type.IsDeriveClassFrom(typeof(IEquatable<TKey>)))
            {
                return id1.Equals(id2);
            }
            return Equals(id1, id2);
        }
        #endregion
    }
}
