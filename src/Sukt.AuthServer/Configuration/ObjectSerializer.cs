using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Configuration
{
    internal static class ObjectSerializer
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            IgnoreNullValues = true
        };

        public static string ToString(object o)
        {
            return JsonSerializer.Serialize(o, Options);
        }

        public static T FromString<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value, Options);
        }
    }
}
