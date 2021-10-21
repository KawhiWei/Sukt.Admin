using Sukt.AuthServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public class SuktValidationKeysStore: ISuktValidationKeysStore
    {
        private readonly IEnumerable<SuktSecurityKeyInfo> _keys;

        public SuktValidationKeysStore(IEnumerable<SuktSecurityKeyInfo> keys)
        {
            _keys = keys ?? throw new ArgumentNullException(nameof(keys));
        }
        public Task<IEnumerable<SuktSecurityKeyInfo>> GetValidationKeysAsync()
        {
            return Task.FromResult(_keys);
        }
    }
}
