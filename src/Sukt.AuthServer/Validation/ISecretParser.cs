using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation
{
    public interface ISecretParser
    {
        string AuthenticationMethod { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<ParsedSecret> ParseAsync(HttpContext context);
    }
}
