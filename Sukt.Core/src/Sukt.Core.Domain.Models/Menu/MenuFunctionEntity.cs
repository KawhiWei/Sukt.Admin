using SuktCore.Shared.Entity;
using SuktCore.Shared.Extensions;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models
{
    /// <summary>
    /// 菜单功能
    /// </summary>
    [DisplayName("菜单功能")]
    public class MenuFunctionEntity : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public MenuFunctionEntity()
        {
            Id = SuktGuid.NewSuktGuid();
        }

        /// <summary>
        /// 菜单ID
        /// </summary>
        [DisplayName("菜单ID")]
        public Guid MenuId { get; set; }

        /// <summary>
        /// 功能ID
        /// </summary>
        [DisplayName("功能ID")]
        public Guid FunctionId { get; set; }

        #region 公共字段

        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [DisplayName("最后修改人")]
        public Guid? LastModifyId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DisplayName("最后修改时间")]
        public DateTime LastModifedAt { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }

        #endregion 公共字段
    }
}