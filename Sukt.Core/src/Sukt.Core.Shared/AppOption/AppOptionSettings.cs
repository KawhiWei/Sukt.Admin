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
}
