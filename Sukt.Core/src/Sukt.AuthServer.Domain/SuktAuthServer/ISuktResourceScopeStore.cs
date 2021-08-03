using Sukt.AuthServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.SuktAuthServer
{
    public interface ISuktResourceScopeStore
    {
        /// <summary>
        /// 根据传入的Scope查询对应的资源
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        Task<SuktResource> FindResourcesByScopeAsync(IEnumerable<string> scopeNames);

    }
}
