using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Sukt.AuthServer.Configuration;
using Sukt.AuthServer.Domain.Models;
using Sukt.AuthServer.Generator;
using Sukt.Module.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Extensions
{
    public static class SigningCredentialExtensionsCrypto
    {
        /// <summary>
        /// 使用X509生成私钥
        /// </summary>
        /// <param name="service"></param>
        /// <param name="certificate"></param>
        /// <param name="signingAlgorithm"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static IServiceCollection AddSigningCredential(this IServiceCollection service, X509Certificate2 certificate, string signingAlgorithm = "RS256")
        {
            if(certificate==null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }
            if (!certificate.HasPrivateKey)
            {
                throw new InvalidOperationException($"X509证书没有私钥!");
            }
            SigningCredentials credentials = new SigningCredentials(new X509SecurityKey(certificate), signingAlgorithm);
            service.AddSigningCredential(credentials);
            return service;
        }
        public static IServiceCollection AddSigningCredential(this IServiceCollection service, SigningCredentials credential)
        {
            if (!(credential.Key is AsymmetricSecurityKey) && (!(credential.Key is Microsoft.IdentityModel.Tokens.JsonWebKey) || !((Microsoft.IdentityModel.Tokens.JsonWebKey)credential.Key).HasPrivateKey))
            {
                throw new InvalidOperationException("签名密钥不能是对称的!");
            }
            if (!SuktAuthServerConstants.SupportedSigningAlgorithms.Contains<string>(credential.Algorithm, StringComparer.Ordinal))
            {
                throw new InvalidOperationException("签名算法 " + credential.Algorithm + " is not supported.");
            }
            ECDsaSecurityKey eCDsaSecurityKey = credential.Key as ECDsaSecurityKey;
            if (eCDsaSecurityKey != null && !CryptoHelper.IsValidCurveForAlgorithm(eCDsaSecurityKey, credential.Algorithm))
            {
                throw new InvalidOperationException("Invalid curve for signing algorithm");
            }
            service.AddSingleton<ISuktSigningCredentialStore>(new SuktDefaultSigningCredentialStore(credential));
            SuktSecurityKeyInfo securityKeyInfo = new SuktSecurityKeyInfo
            {
                Key = credential.Key,
                SigningAlgorithm = credential.Algorithm
            };
            service.AddSingleton((ISuktValidationKeysStore)new SuktValidationKeysStore(new SuktSecurityKeyInfo[1]
            {
                securityKeyInfo
            }));
            return service;
        }
        /// <summary>
        /// 开发环境中使用的证书<在开发中使用自己生成的随机的jwt.key>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDeveloperSigningCredential(this IServiceCollection services,
            bool persistKey = true,
            string filename = null,
            SuktAuthServerConstants.RsaSigningAlgorithm signingAlgorithm = SuktAuthServerConstants.RsaSigningAlgorithm.RS256)
        {
            if (filename == null)
            {
                filename = Path.Combine(Directory.GetCurrentDirectory(), "securitykey.jwk");
            }
            if (File.Exists(filename))
            {
                var json = File.ReadAllText(filename);
                var jwk = new JsonWebKey(json);
                return services.AddSigningCredential(jwk, jwk.Alg);
            }
            else
            {
                var key = CryptoHelper.CreateRsaSecurityKey();
                var jwk = JsonWebKeyConverter.ConvertFromRSASecurityKey(key);
                jwk.Alg = signingAlgorithm.ToString();
                if (persistKey)
                {
                    File.WriteAllText(filename, JsonConvert.SerializeObject(jwk));
                }
                return services.AddSigningCredential(key, signingAlgorithm);
            }
        }
        public static IServiceCollection AddSigningCredential(this IServiceCollection services, RsaSecurityKey key, SuktAuthServerConstants.RsaSigningAlgorithm signingAlgorithm)
        {
            var credential = new SigningCredentials(key, CryptoHelper.GetRsaSigningAlgorithmValue(signingAlgorithm));
            return services.AddSigningCredential(credential);
        }
        public static IServiceCollection AddSigningCredential(this IServiceCollection services, SecurityKey key, string signingAlgorithm)
        {
            var credential = new SigningCredentials(key, signingAlgorithm);
            return services.AddSigningCredential(credential);
        }
    }
}
