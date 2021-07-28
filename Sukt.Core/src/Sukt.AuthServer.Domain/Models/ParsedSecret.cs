using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.Models
{
    public class ParsedSecret
    {
        /// <summary>
        /// 客户端id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 传入的Secret
        /// </summary>
        public object Credential { get; set; }
        /// <summary>
        /// 密钥类型
        /// </summary>
        public string Type { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}
