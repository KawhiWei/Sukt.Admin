using Sukt.AuthServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.ValidationResult
{
    public class ResourceValidationResult
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public SuktResource SuktResources { get; set; } = new SuktResource();
        /// <summary>
        /// 作用域
        /// </summary>
        public ICollection<string> ParsedScopes = new HashSet<string>();
        /// <summary>
        /// 请求的范围无效。
        /// </summary>
        public ICollection<string> InvalidScopes { get; set; } = new HashSet<string>();
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Succeeded => ParsedScopes.Any() && !InvalidScopes.Any();
    }
}
