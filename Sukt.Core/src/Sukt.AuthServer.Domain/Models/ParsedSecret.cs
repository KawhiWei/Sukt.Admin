using System.Collections.Generic;

namespace Sukt.AuthServer.Domain.Models
{
    public class ParsedSecret
    {
        /// <summary>
        /// 客户端id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 凭据  即传入的Secret
        /// </summary>
        public object Credential { get; set; }
        /// <summary>
        /// 密钥类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 额外属性
        /// </summary>
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}
