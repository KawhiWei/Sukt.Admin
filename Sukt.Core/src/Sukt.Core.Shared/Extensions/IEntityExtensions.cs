using Microsoft.AspNetCore.Http;
using Sukt.Core.Shared.Entity;
using System;

namespace Sukt.Core.Shared.Extensions
{
    public static class IEntityExtensions
    {
        #region 创建时间扩展
        public static TEntity[] CheckInsert<TEntity, TPrimaryKey>(this TEntity[] entitys, IHttpContextAccessor httpContextAccessor)
            where TEntity : class, IEntity<TPrimaryKey>
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {

            for (int i = 0; i < entitys.Length; i++)
            {
                var entity = entitys[i];
                entitys[i] = entity.CheckInsert<TEntity, TPrimaryKey>(httpContextAccessor);
            }
            return entitys;
        }
        public static TEntity CheckInsert<TEntity, TPrimaryKey>(this TEntity entity, IHttpContextAccessor httpContextAccessor)
           where TEntity : class, IEntity<TPrimaryKey>
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {

            var creationAudited = entity.GetType().GetInterface(/*$"ICreationAudited`1"*/typeof(ICreatedAudited<>).Name);
            if (creationAudited == null)
            {
                return entity;
            }
            var typeArguments = creationAudited?.GenericTypeArguments[0];
            var fullName = typeArguments?.FullName;
            if (fullName == typeof(Guid).FullName)
            {
                entity = CheckICreationAudited<TEntity, Guid>(entity, httpContextAccessor);
            }
            return entity;
        }
        private static TEntity CheckICreationAudited<TEntity, TUserKey>(TEntity entity, IHttpContextAccessor httpContextAccessor)
           where TUserKey : struct, IEquatable<TUserKey>
        {
            if (!entity.GetType().IsBaseOn(typeof(ICreatedAudited<>)))
            {
                return entity;
            }
            ICreatedAudited<TUserKey> entity1 = (ICreatedAudited<TUserKey>)entity;
            entity1.CreatedId = httpContextAccessor.HttpContext?.User?.Identity.GetUesrId<TUserKey>();
            entity1.CreatedAt = DateTime.Now;
            return (TEntity)entity1;
        }
        #endregion

        #region 检查修改时间
        public static TEntity[] CheckModification<TEntity, TPrimaryKey>(this TEntity[] entitys, IHttpContextAccessor httpContextAccessor)
            where TEntity : class, IEntity<TPrimaryKey>
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {

            for (int i = 0; i < entitys.Length; i++)
            {
                var entity = entitys[i];
                entitys[i] = entity.CheckInsert<TEntity, TPrimaryKey>(httpContextAccessor);
            }
            return entitys;
        }
        public static TEntity CheckModification<TEntity, TPrimaryKey>(this TEntity entity, IHttpContextAccessor httpContextAccessor)
           where TEntity : class, IEntity<TPrimaryKey>
            where TPrimaryKey : IEquatable<TPrimaryKey>
        {

            var creationAudited = entity.GetType().GetInterface(/*$"ICreationAudited`1"*/typeof(IModifyAudited<>).Name);
            if (creationAudited == null)
            {
                return entity;
            }
            var typeArguments = creationAudited?.GenericTypeArguments[0];
            var fullName = typeArguments?.FullName;
            if (fullName == typeof(Guid).FullName)
            {
                entity = CheckIModificationAudited<TEntity, Guid>(entity, httpContextAccessor);
            }
            return entity;
        }
        private static TEntity CheckIModificationAudited<TEntity, TUserKey>(TEntity entity, IHttpContextAccessor httpContextAccessor)
           where TUserKey : struct, IEquatable<TUserKey>
        {
            if (!entity.GetType().IsBaseOn(typeof(IModifyAudited<>)))
            {
                return entity;
            }
            IModifyAudited<TUserKey> entity1 = (IModifyAudited<TUserKey>)entity;
            entity1.LastModifyId = httpContextAccessor.HttpContext?.User?.Identity.GetUesrId<TUserKey>();
            entity1.LastModifedAt = DateTime.Now;
            return (TEntity)entity1;
        }
        #endregion
    }
}
