using Sukt.AuthServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.ValidationResult
{
    public class ResourceValidationRequest
    {
        /// <summary>
        /// 客户端
        /// </summary>
        public SuktApplicationModel ClientApplication { get; set; }
        /// <summary>
        /// 请求作用域
        /// </summary>
        public IEnumerable<string> Scopes { get; set; }
    }
}
