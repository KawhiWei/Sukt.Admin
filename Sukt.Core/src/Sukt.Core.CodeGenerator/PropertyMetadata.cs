namespace Sukt.Core.CodeGenerator
{
    /// <summary>
    /// 属性元数据
    /// </summary>
    public class PropertyMetadata
    {
        /// <summary>
        /// 是否主键
        /// </summary>

        public bool IsPrimaryKey { get; set; } = false;

        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool IsNullable { get; set; } = false;

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// C#数据类型
        /// </summary>
        public string CSharpType { get; set; }

        /// <summary>
        /// 该字段是否生成输入DTO
        /// </summary>
        public bool IsInputDto { get; set; } = true;

        /// <summary>
        /// 该字段是否生成输出DTO
        /// </summary>
        public bool IsOutputDto { get; set; } = true;

        /// <summary>
        /// 该字段是否生成分页DTO
        /// </summary>
        public bool IsPageDto { get; set; } = true;
    }
}