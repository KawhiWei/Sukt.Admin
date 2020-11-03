using System;

namespace Sukt.Core.Shared.Attributes
{
    public abstract class AttributeBase : Attribute
    {
        public abstract string Description();
    }
}