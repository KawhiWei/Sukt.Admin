using Sukt.AuthServer.Domain.Models;
using Sukt.Core.Domain.Models.IdentityServerFour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Contexts
{
    /// <summary>
    /// 是否是活跃上下文
    /// </summary>
    public class IsActiveContext
    {
        public IsActiveContext(ClaimsPrincipal subject, SuktApplicationModel suktApplication, string caller)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            SuktApplication = suktApplication ?? throw new ArgumentNullException(nameof(suktApplication));
            Caller = caller ?? throw new ArgumentNullException(nameof(caller));
        }

        /// <summary>
        /// 上下文主题
        /// </summary>
        public ClaimsPrincipal Subject { get; set; }
        /// <summary>
        /// 客户端信息
        /// </summary>
        public SuktApplicationModel SuktApplication { get; set; }
        /// <summary>
        /// 调用者
        /// </summary>
        public string Caller { get; set; }
        /// <summary>
        /// 是否活跃
        /// </summary>
        public bool IsActive { get; set; }
    }
}
