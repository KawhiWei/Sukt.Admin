using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sukt.Core.Shared.Entity
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
