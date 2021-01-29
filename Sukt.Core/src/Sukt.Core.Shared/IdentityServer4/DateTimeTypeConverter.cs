using AutoMapper;
using System;

namespace Sukt.Core.Shared.IdentityServer4
{
    public class DateTimeTypeConverter : ITypeConverter<DateTime, DateTimeOffset>, ITypeConverter<DateTimeOffset, DateTime>
    {
        public DateTime Convert(DateTimeOffset source, DateTime destination, ResolutionContext context)
        {
            return source.LocalDateTime;
        }

        public DateTimeOffset Convert(DateTime source, DateTimeOffset destination, ResolutionContext context)
        {
            return new DateTimeOffset(source);
        }
    }
}
