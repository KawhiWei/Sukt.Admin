using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public class TokenRequest
    {
        public TokenRequest(string type)
        {
            Type = type;
        }

        /// <summary>
        /// 用户信息主体
        /// </summary>
        public ICollection<Claim> Claims { get; set; }
        /// <summary>
        /// 创建时间 
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 时长  
        /// </summary>
        public int Lifetime { get; set; } = 300;
        /// <summary>
        /// 颁发人
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 客户端Id
        /// </summary>
        public string SuktApplicationClientId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public TokenType TokenType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<string> Audiences { get; set; } = new HashSet<string>();
        /// <summary>
        /// 类型
        /// </summary>
        public string Type{ get; set; } = "access_token";


    }
}
