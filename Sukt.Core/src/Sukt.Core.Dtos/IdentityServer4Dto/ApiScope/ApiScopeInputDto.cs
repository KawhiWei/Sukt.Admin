using System.ComponentModel;

namespace Sukt.Core.Dtos.IdentityServer4Dto.ApiScope
{
    public class ApiScopeInputDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; private set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [DisplayName("显示名称")]
        public string DisplayName { get; private set; }
    }
}
