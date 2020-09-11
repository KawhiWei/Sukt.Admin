using Sukt.Core.Shared.Attributes.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared
{
    /// <summary>
    /// 实现此接口的类型将自动注册为<see cref="ServiceLifetime.Transient"/>模式
    /// </summary>
    [IgnoreDependency]
    public interface ITransientDependency
    {

    }
}
