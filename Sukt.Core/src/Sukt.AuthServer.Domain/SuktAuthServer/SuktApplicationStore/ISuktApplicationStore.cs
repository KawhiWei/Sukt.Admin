using Sukt.AuthServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.SuktAuthServer.SuktApplicationStore
{
    /// <summary>
    /// 应用接口仓库定义
    /// </summary>
    public interface ISuktApplicationStore
    {
        Task<SuktApplicationModel> FindByClientIdAsync(string clientId);
    }
}
