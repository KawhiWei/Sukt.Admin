using System;
using System.Collections.Generic;

namespace Sukt.Core.Shared.AppOption
{
    public class AppOptionSettings
    {
        public CorsOptions Cors { get; set; }

        public JwtOptions Jwt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AuthOptions Auth { get; set; }
        public Dictionary<string, DestinyContextOptions> DbContexts { get; set; }
    }

    /// <summary>
    /// Cors操作
    /// </summary>
    public class CorsOptions
    {
        /// <summary>
        /// 策略名
        /// </summary>
        public string PolicyName { get; set; }

        /// <summary>
        /// Cors地址
        /// </summary>
        public string Url { get; set; }
    }
    public class AuthOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string Authority { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }
    }
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DestinyContextOptions
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public DatabaseType DatabaseType { get; set; }

        /// <summary>
        /// 数据库链接
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 下上文类型名字
        /// </summary>
        public string DbContextTypeName { get; set; }

        /// <summary>
        /// 上下文类型
        /// </summary>
        public Type DbContextType => Type.GetType(DbContextTypeName);

        /// <summary>
        /// 迁移Assembly名字
        /// </summary>
        public string MigrationsAssemblyName { get; set; }
    }
}
