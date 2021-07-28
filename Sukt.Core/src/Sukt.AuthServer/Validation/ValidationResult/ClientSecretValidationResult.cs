using Sukt.AuthServer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.ValidationResult
{
    /// <summary>
    /// Client 客户端验证返回类
    /// </summary>
    public class ClientSecretValidationResult: ValidationResultBase
    {
        /// <summary>
        /// 客户端应用类
        /// </summary>
        public SuktApplicationModel ClientApplication { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Confirmation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ParsedSecret Secret { get; set; }
    }
}
