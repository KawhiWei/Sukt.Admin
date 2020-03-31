using Sukt.Core.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Attributes.AutoMapper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SuktAutoMapFromAttribute:SuktAutoMapperAttribute
    {
        public override SuktAutoMapDirection MapDirection
        {
            get { return SuktAutoMapDirection.From; }
        }
        public SuktAutoMapFromAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }
}
