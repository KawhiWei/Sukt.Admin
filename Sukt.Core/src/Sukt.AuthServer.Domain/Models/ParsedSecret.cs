using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.Models
{
    public class ParsedSecret
    {
        public string Id { get; set; }
        public object Credential { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}
