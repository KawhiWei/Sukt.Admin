using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sukt.Core.Domain.Models.Function
{
    [DisplayName("功能模块")]
    public class FunctionEntity : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public FunctionEntity()
        {
            Id = SuktGuid.NewSuktGuid();
        }
        [DisplayName("功能名称")]
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("描述")]
        public string Description { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        [DisplayName("是否可用")]
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 链接Url
        /// </summary>
        [DisplayName("链接Url")]
        public string LinkUrl { get; set; }
        #region 公共字段
        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid? CreatedId { get; set; }
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
        #endregion
    }
}
