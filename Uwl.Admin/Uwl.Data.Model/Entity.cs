using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Uwl.Attribute.ExcelAttribute;

namespace Uwl.Data.Model
{
    /// <summary>
    /// 对所有实体建立一个泛型基类Entity<TPrimaryKey>,默认的主键类型为Guid的实体基类Entity，权限管理系统的所有实体都从Entity基类继承，
    /// 如果想要实现其他类型主键，新建的实体从Entity<TPrimaryKey>泛型基类继承即可。
    /// </summary>
    /// <typeparam name="TPriMaryKey">继承类传进来的主键类型</typeparam>
    public abstract class Entity<TPriMaryKey>
    {
        [Key]
        public virtual  TPriMaryKey Id { get; set; }
    }
    public abstract class Entity : Entity<Guid>
    {
        /// <summary>
        /// 创建时间//   类型后面加问号代表可以为空
        /// </summary>
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid? CreatedId { get; set; }
        [ExcelColumnName("创建人", ColumnWith = 30, Sort = 8)]
        /// <summary>
        /// 创建人姓名
        /// </summary>
        [MaxLength(50)]
        public string CreatedName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        [MaxLength(50)]
        public string UpdateName { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public Guid? UpdateId { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDrop { get; set; } = false;
    }
}
