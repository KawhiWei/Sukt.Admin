using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Attributes
{
    public abstract class AttributeBase: Attribute
    {
        public abstract string Description();
    }
}
