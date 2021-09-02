using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Extensions
{
    public static class Base64Url
    {
        public static string Encode(byte[] arg)
        {
            return Convert.ToBase64String(arg).Split(new char[1]
            {
                '='
            })[0].Replace('+', '-').Replace('/', '_');
        }
        public static byte[] Decode(string arg)
        {
            string text = arg;
            text = text.Replace('-', '+');
            text = text.Replace('_', '/');
            switch (text.Length % 4)
            {
                case 2:
                    text += "==";
                    break;
                case 3:
                    text += "=";
                    break;
                default:
                    throw new Exception("Illegal base64url string!");
                case 0:
                    break;
            }

            return Convert.FromBase64String(text);
        }
    }
}
