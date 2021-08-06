using Sukt.AuthServer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation
{
    /// <summary>
    /// 密码认证模式认证服务接口
    /// </summary>
    public interface IResourceOwnerPasswordValidator
    {
        Task ValidateAsync(ResourceOwnerPasswordValidationContext context);
    }
}
