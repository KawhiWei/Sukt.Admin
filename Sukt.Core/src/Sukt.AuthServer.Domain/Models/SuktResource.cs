using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.Models
{
    public class SuktResource
    {
        /// <summary>
        /// 资源配置列表
        /// </summary>
        public ICollection<SuktResourceScopeModel> SuktResources = new HashSet<SuktResourceScopeModel>();
    }
}
