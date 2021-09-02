using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public enum TokenType
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        Jwt,
        /// <summary>
        /// 刷新
        /// </summary>
        Reference
    }
}
