using System.Collections.Generic;

namespace Sukt.Core.CodeGenerator
{
    public class EntityMetadata
    {
        /// <summary>
        /// 实体名
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 主键类型
        /// </summary>
        public string PrimaryKeyType { get; set; }

        /// <summary>
        /// 主键名
        /// </summary>

        public string PrimaryKeyName { get; set; }

        /// <summary>
        /// 是否生成Dto
        /// </summary>
        public bool IsGeneratorDto { get; set; }

        /// <summary>
        /// 属性集合
        /// </summary>
        public List<PropertyMetadata> Properties = new List<PropertyMetadata>();

        /// <summary>
        /// 就否自动映射（AutoMappper）
        /// </summary>
        public bool IsAutoMap { get; set; }

        /// <summary>
        // 是否创建
        /// </summary>
        public bool IsCreation { get; set; } = true;

        /// <summary>
        /// 是否修改
        /// </summary>
        public bool IsModification { get; set; } = true;

        /// <summary>
        /// 是否软删除
        /// </summary>
        public bool IsSoftDelete { get; set; } = true;

        /// <summary>
        /// 是否全部审核
        /// </summary>
        /// <returns></returns>
        public bool IsFullAudited()
        {
            return IsCreation && IsModification && IsSoftDelete;
        }

        /// <summary>
        /// 审核用户键类型
        /// </summary>
        public string AuditedUserKeyType { get; set; }
    }
}