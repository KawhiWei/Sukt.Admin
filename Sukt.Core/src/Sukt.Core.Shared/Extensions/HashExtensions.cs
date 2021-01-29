using System;
using System.Security.Cryptography;
using System.Text;

namespace Sukt.Core.Shared.Extensions
{
    public static partial class Extensions
    {
        public static string Sha256(this string input)
        {
            if (input.IsMissing())
            {
                return string.Empty;
            }

            using (SHA256 sHA = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                return Convert.ToBase64String(sHA.ComputeHash(bytes));
            }
        }
        public static byte[] Sha256(this byte[] input)
        {
            if (input == null)
            {
                return null;
            }

            using (SHA256 sHA = SHA256.Create())
            {
                return sHA.ComputeHash(input);
            }
        }
        public static string Sha512(this string input)
        {
            if (input.IsMissing())
            {
                return string.Empty;
            }

            using (SHA512 sHA = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                return Convert.ToBase64String(sHA.ComputeHash(bytes));
            }
        }
        public static bool IsMissing(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        public static bool IsPresent(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
