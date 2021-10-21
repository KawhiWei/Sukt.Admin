using Microsoft.IdentityModel.Tokens;
using Sukt.AuthServer.Extensions;
using Sukt.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Sukt.Module.Core.Constants;

namespace Sukt.AuthServer.Configuration
{
    public static class CryptoHelper
    {
        internal static bool IsValidCurveForAlgorithm(ECDsaSecurityKey key, string algorithm)
        {
            ECParameters eCParameters = key.ECDsa.ExportParameters(includePrivateParameters: false);
            if ((algorithm == "ES256" && eCParameters.Curve.Oid.Value != "1.2.840.10045.3.1.7") || (algorithm == "ES384" && eCParameters.Curve.Oid.Value != "1.3.132.0.34") || (algorithm == "ES512" && eCParameters.Curve.Oid.Value != "1.3.132.0.35"))
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 创建一个RSA安全密钥
        /// </summary>
        /// <param name="keySize"></param>
        /// <returns></returns>
        public static RsaSecurityKey CreateRsaSecurityKey(int keySize = 2048)
        {
            return new RsaSecurityKey(RSA.Create(keySize))
            {
                KeyId = CryptoRandom.CreateUniqueId(16, CryptoRandom.OutputFormat.Hex)
            };
        }
        internal static string GetRsaSigningAlgorithmValue(SuktAuthServerConstants.RsaSigningAlgorithm value)
        {
            return value switch
            {
                SuktAuthServerConstants.RsaSigningAlgorithm.RS256 => SecurityAlgorithms.RsaSha256,
                SuktAuthServerConstants.RsaSigningAlgorithm.RS384 => SecurityAlgorithms.RsaSha384,
                SuktAuthServerConstants.RsaSigningAlgorithm.RS512 => SecurityAlgorithms.RsaSha512,
                SuktAuthServerConstants.RsaSigningAlgorithm.PS256 => SecurityAlgorithms.RsaSsaPssSha256,
                SuktAuthServerConstants.RsaSigningAlgorithm.PS384 => SecurityAlgorithms.RsaSsaPssSha384,
                SuktAuthServerConstants.RsaSigningAlgorithm.PS512 => SecurityAlgorithms.RsaSsaPssSha512,
                _ => throw new ArgumentException("Invalid RSA signing algorithm value", nameof(value)),
            };
        }
        internal static string GetCrvValueFromCurve(ECCurve curve)
        {
            return curve.Oid.Value switch
            {
                CurveOids.P256 => JsonWebKeyECTypes.P256,
                CurveOids.P384 => JsonWebKeyECTypes.P384,
                CurveOids.P521 => JsonWebKeyECTypes.P521,
                _ => throw new InvalidOperationException($"Unsupported curve type of {curve.Oid.Value} - {curve.Oid.FriendlyName}"),
            };
        }
    }
}
