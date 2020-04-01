using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Entity
{
    /// <summary>
    /// 逻辑删除
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
