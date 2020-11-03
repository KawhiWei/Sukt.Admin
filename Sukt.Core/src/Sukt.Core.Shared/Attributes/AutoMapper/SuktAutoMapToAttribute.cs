using Sukt.Core.Shared.Enums;
using System;

namespace Sukt.Core.Shared.Attributes.AutoMapper
{
    public class SuktAutoMapToAttribute : SuktAutoMapperAttribute
    {
        public override SuktAutoMapDirection MapDirection
        {
            get { return SuktAutoMapDirection.From; }
        }

        public SuktAutoMapToAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {
        }
    }
}