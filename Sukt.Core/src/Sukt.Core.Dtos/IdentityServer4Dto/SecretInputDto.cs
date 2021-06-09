using Sukt.Module.Core.Entity;
using System;

namespace Sukt.Core.Dtos.IdentityServer4Dto
{
    /// <summary>
    /// 客户端授权令牌
    /// </summary>
    public class SecretInputDto : InputDto<Guid>
    {
        public SecretInputDto()
        {
            Type = "SharedSecret";
        }
        public SecretInputDto(string value, DateTimeOffset? expiration) : this()
        {
            Value = value;
            Expiration = expiration;
        }
        public SecretInputDto(string description, string value, DateTimeOffset? expiration)
        {
            Description = description;
            Value = value;
            Expiration = expiration;
        }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTimeOffset? Expiration { get; set; }
        public string Type { get; private set; }
        public override int GetHashCode()
        {
            return (17 * 23 + (Value?.GetHashCode() ?? 0)) * 23 + (Type?.GetHashCode() ?? 0);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            SecretInputDto secret = obj as SecretInputDto;
            if (secret == null)
            {
                return false;
            }

            if (secret == this)
            {
                return true;
            }

            if (string.Equals(secret.Type, Type, StringComparison.Ordinal))
            {
                return string.Equals(secret.Value, Value, StringComparison.Ordinal);
            }

            return false;
        }
    }
}
