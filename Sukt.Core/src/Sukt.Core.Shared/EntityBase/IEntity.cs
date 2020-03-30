using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sukt.Core.Shared.EntityBase
{
    public interface IEntity
    {
    }
    public interface IEntity<out TKey>: IEntity
    {
        [Description("主键")]
        TKey Id { get; }
    }
    
}
