
using Sukt.Core.Domain.Models.Menu;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
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
        /// 菜单集合
        /// </summary>
        public MenuEntity MenuItem { get; private set; }
        /// <summary>
        /// 功能集合
        /// </summary>
        public FunctionEntity FunctionItems { get; set; }
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