using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.EntityBase
{
    /// <summary>
    /// 逻辑删除
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
