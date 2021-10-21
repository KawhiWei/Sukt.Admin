using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.Models
{
    public class SuktSecurityKeyInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public SecurityKey Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SigningAlgorithm { get; set; }
    }
}
