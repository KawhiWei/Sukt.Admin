using Microsoft.EntityFrameworkCore;
using Sukt.AuthServer.Domain.Models;
using Sukt.Core.Domain.Models;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.SuktAuthServer.SuktApplicationStore
{
    /// <summary>
    /// 默认客户端查询实现
    /// </summary>
    public class SuktApplicationStore : ISuktApplicationStore
    {
        private readonly IEFCoreRepository<SuktApplication, Guid> _suktApplicationRepository;

        public SuktApplicationStore(IEFCoreRepository<SuktApplication, Guid> suktApplicationRepository)
        {
            _suktApplicationRepository = suktApplicationRepository;
        }

        /// <summary>
        /// 根据客户端Id查询出客户端应用的一条数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<SuktApplicationModel> FindByClientIdAsync(string clientId)
        {
            var entity =await _suktApplicationRepository.NoTrackEntities.FirstOrDefaultAsync(x => x.ClientId == clientId);
            if (entity != null)
                return entity.MapTo<SuktApplicationModel>();
            return null;
        }
    }
}
