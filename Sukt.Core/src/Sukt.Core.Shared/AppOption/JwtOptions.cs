namespace Sukt.Core.Shared.AppOption
{
    public class JwtOptions
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 订阅人
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 过期分种数
        /// </summary>
        public double ExpireMins { get; set; }
    }
}