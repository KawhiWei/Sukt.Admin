namespace Sukt.Core.CodeGenerator
{
    /// <summary>
    /// 项目元数据
    /// </summary>
    public class ProjectMetadata
    {
        /// <summary>
        /// 获取或设置 公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 获取或设置 站点地址
        /// </summary>
        public string SiteUrl { get; set; }

        /// <summary>
        /// 获取或设置 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 获取或设置 版权
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// 实体元数据
        /// </summary>

        public EntityMetadata EntityMetadata { get; set; }
    }
}