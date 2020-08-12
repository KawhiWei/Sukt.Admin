using Sukt.Core.Shared.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Shared.Permission
{
    public interface IAuthorityVerification: IScopedDependency
    {
        /// <summary>
        /// 判断用户是否有权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsPermission(string url);
    }
}
