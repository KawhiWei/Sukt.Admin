using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Extensions
{
    public class CryptoRandom : Random
    {
        public enum OutputFormat
        {
            Base64Url,
            Base64,
            Hex
        }
        private readonly byte[] _uint32Buffer = new byte[4];
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();
        public CryptoRandom()
        {
        }
        public CryptoRandom(int ignoredSeed)
        {
        }
        public static byte[] CreateRandomKey(int length)
        {
            byte[] array = new byte[length];
            Rng.GetBytes(array);
            return array;
        }
        public static string CreateUniqueId(int length = 32, OutputFormat format = OutputFormat.Base64Url)
        {
            byte[] array = CreateRandomKey(length);
            switch (format)
            {
                case OutputFormat.Base64Url:
                    return Base64Url.Encode(array);
                case OutputFormat.Base64:
                    return Convert.ToBase64String(array);
                case OutputFormat.Hex:
                    return BitConverter.ToString(array).Replace("-", "");
                default:
                    throw new ArgumentException("Invalid output format", "format");
            }
        }
        public override int Next()
        {
            Rng.GetBytes(_uint32Buffer);
            return BitConverter.ToInt32(_uint32Buffer, 0) & int.MaxValue;
        }
        public override int Next(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException("maxValue");
            }

            return Next(0, maxValue);
        }
        public override int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue");
            }

            if (minValue == maxValue)
            {
                return minValue;
            }

            long num = maxValue - minValue;
            uint num2;
            long num3;
            long num4;
            do
            {
                Rng.GetBytes(_uint32Buffer);
                num2 = BitConverter.ToUInt32(_uint32Buffer, 0);
                num3 = 4294967296L;
                num4 = num3 % num;
            }
            while (num2 >= num3 - num4);
            return (int)(minValue + (long)num2 % num);
        }
        public override double NextDouble()
        {
            Rng.GetBytes(_uint32Buffer);
            return (double)BitConverter.ToUInt32(_uint32Buffer, 0) / 4294967296.0;
        }
        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            Rng.GetBytes(buffer);
        }
    }
}
