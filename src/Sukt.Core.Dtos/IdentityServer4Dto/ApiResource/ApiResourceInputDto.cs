using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Dtos.IdentityServer4Dto.ApiResource
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("Api资源输入Dto")]
    public class ApiResourceInputDto
    {
        public string Name { get; set; }
        public List<string> UserClaims { get; set; }
        public string DisplayName { get; set; }
    }
}
