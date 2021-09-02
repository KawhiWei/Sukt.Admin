using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Models
{
    /// <summary>
    /// 访问资源实体
    /// </summary>
    [DisplayName("访问资源配置")]
    public class SuktResourceScope :EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        [DisplayName("资源名称")]
        public string Name { get; set; }
        /// <summary>
        /// 资源显示名称
        /// </summary>
        [DisplayName("资源显示名称")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        [DisplayName("属性")]
        public string Properties { get; set; }
        /// <summary>
        /// 资源域
        /// </summary>
        [DisplayName("资源域")]
        public string Resources { get; set; }
        /// <summary>
        /// 获取、设置并发标记
        /// </summary>
        [DisplayName("获取、设置并发标记")]
        public string ConcurrencyToken { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Description { get; set; }
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
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        [DisplayName("修改人ID")]
        public Guid? LastModifyId { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public virtual DateTime LastModifedAt { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }

        #endregion 公共字段
    }
}
