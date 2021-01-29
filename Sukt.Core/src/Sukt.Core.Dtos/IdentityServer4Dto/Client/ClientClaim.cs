using System;

namespace Sukt.Core.Dtos.ClientApplication.ClientDto
{
    public class ClientClaim
    {
        public ClientClaim(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public ClientClaim(string type, string value, string valueType)
        {
            this.Type = type;
            this.Value = value;
            ValueType = valueType;
        }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; } = "http://www.w3.org/2001/XMLSchema#string";
        public override int GetHashCode()
        {
            return ((17 * 23 + Value.GetHashCode()) * 23 + Type.GetHashCode()) * 23 + ValueType.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            ClientClaim clientClaim = obj as ClientClaim;
            if (clientClaim != null)
            {
                if (string.Equals(Type, clientClaim.Type, StringComparison.Ordinal) && string.Equals(Value, clientClaim.Value, StringComparison.Ordinal))
                {
                    return string.Equals(ValueType, clientClaim.ValueType, StringComparison.Ordinal);
                }

                return false;
            }

            return false;
        }
    }
}
