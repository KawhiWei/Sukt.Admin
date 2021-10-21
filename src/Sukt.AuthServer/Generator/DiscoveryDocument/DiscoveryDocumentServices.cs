using Microsoft.IdentityModel.Tokens;
using Sukt.AuthServer.Configuration;
using Sukt.AuthServer.EndpointHandler;
using Sukt.AuthServer.Extensions;
using Sukt.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Sukt.Module.Core.Constants;

namespace Sukt.AuthServer.Generator.DiscoveryDocument
{
    public class DiscoveryDocumentServices : IDiscoveryDocument
    {
        private readonly IEnumerable<ISuktValidationKeysStore>  _suktValidationKeysStores;
        private readonly IEnumerable<ISuktSigningCredentialStore> _suktSigningCredentialStores;
        public DiscoveryDocumentServices(IEnumerable<ISuktValidationKeysStore> suktValidationKeysStores,IEnumerable<ISuktSigningCredentialStore>  suktSigningCredentialStores)
        {
            _suktValidationKeysStores = suktValidationKeysStores;
            _suktSigningCredentialStores = suktSigningCredentialStores;
        }
        public async Task<Dictionary<string, object>> GetDocumentsAsync(string baseUrl, string issureUrl)
        {
            var entities = new Dictionary<string, object>{
                {OidcConstants.Discovery.Issuer,issureUrl},
                { OidcConstants.Discovery.AuthorizationEndpoint,$"{baseUrl}{ProtocolRoutePaths.Authorize}"},
                { OidcConstants.Discovery.TokenEndpoint,$"{baseUrl}{ProtocolRoutePaths.Token}"},
                { OidcConstants.Discovery.UserInfoEndpoint, baseUrl + ProtocolRoutePaths.UserInfo },
                { OidcConstants.Discovery.JwksUri, baseUrl + ProtocolRoutePaths.DiscoveryWebKeys },
            };
            //var standardGrantTypes = new List<string>
            //    {
            //        OidcConstants.GrantTypes.AuthorizationCode,
            //        OidcConstants.GrantTypes.ClientCredentials,
            //        OidcConstants.GrantTypes.RefreshToken,
            //        OidcConstants.GrantTypes.Implicit,
            //        OidcConstants.GrantTypes.Password,
            //        OidcConstants.GrantTypes.DeviceCode
            //    };
            //entities.Add(OidcConstants.Discovery.GrantTypesSupported, standardGrantTypes);
            //entities.Add(OidcConstants.Discovery.ResponseTypesSupported, SupportedResponseTypes.ToArray());
            //entities.Add(OidcConstants.Discovery.ResponseModesSupported, SupportedResponseModes.ToArray());
            //if(_suktSigningCredentialStores.Any())
            //{
            //    var credentials = new List<SigningCredentials>();

            //    foreach ( var suktSigningCredentialStore  in _suktSigningCredentialStores)
            //    {
            //        credentials.Add(await suktSigningCredentialStore.GetSigningCredentialsAsync());
            //    }
            //    if (credentials.Any())
            //    {
            //        entities.Add(OidcConstants.Discovery.IdTokenSigningAlgorithmsSupported, credentials.Select(x => x.Algorithm).Distinct());
            //    }
            //}
            //entities.Add(OidcConstants.Discovery.SubjectTypesSupported, new[] { "public" });
            //entities.Add(OidcConstants.Discovery.CodeChallengeMethodsSupported, new[] { OidcConstants.CodeChallengeMethods.Plain, OidcConstants.CodeChallengeMethods.Sha256 });
            //entities.Add(OidcConstants.Discovery.TlsClientCertificateBoundAccessTokens, true);
            await Task.CompletedTask;
            return entities;
        }
        public async  Task<IEnumerable<SuktJsonWebKey>> GetJwkDocumentAsync()
        {
            await Task.CompletedTask;
            var webKeys = new List<SuktJsonWebKey>();

            foreach (var store in _suktValidationKeysStores)
            {
                foreach (var key in (await store.GetValidationKeysAsync()))
                {
                    if (key.Key is X509SecurityKey x509Key)
                    {
                        var cert64 = Convert.ToBase64String(x509Key.Certificate.RawData);
                        var thumbprint = Base64Url.Encode(x509Key.Certificate.GetCertHash());

                        if (x509Key.PublicKey is RSA rsa)
                        {
                            var parameters = rsa.ExportParameters(false);
                            var exponent = Base64Url.Encode(parameters.Exponent);
                            var modulus = Base64Url.Encode(parameters.Modulus);

                            var rsaJsonWebKey = new SuktJsonWebKey
                            {
                                kty = "RSA",
                                use = "sig",
                                kid = x509Key.KeyId,
                                x5t = thumbprint,
                                e = exponent,
                                n = modulus,
                                x5c = new[] { cert64 },
                                alg = key.SigningAlgorithm
                            };
                            webKeys.Add(rsaJsonWebKey);
                        }
                        else if (x509Key.PublicKey is ECDsa ecdsa)
                        {
                            var parameters = ecdsa.ExportParameters(false);
                            var x = Base64Url.Encode(parameters.Q.X);
                            var y = Base64Url.Encode(parameters.Q.Y);

                            var ecdsaJsonWebKey = new SuktJsonWebKey
                            {
                                kty = "EC",
                                use = "sig",
                                kid = x509Key.KeyId,
                                x5t = thumbprint,
                                x = x,
                                y = y,
                                crv = CryptoHelper.GetCrvValueFromCurve(parameters.Curve),
                                x5c = new[] { cert64 },
                                alg = key.SigningAlgorithm
                            };
                            webKeys.Add(ecdsaJsonWebKey);
                        }
                        else
                        {
                            throw new InvalidOperationException($"key type: {x509Key.PublicKey.GetType().Name} not supported.");
                        }
                    }
                    else if (key.Key is RsaSecurityKey rsaKey)
                    {
                        var parameters = rsaKey.Rsa?.ExportParameters(false) ?? rsaKey.Parameters;
                        var exponent = Base64Url.Encode(parameters.Exponent);
                        var modulus = Base64Url.Encode(parameters.Modulus);

                        var webKey = new SuktJsonWebKey
                        {
                            kty = "RSA",
                            use = "sig",
                            kid = rsaKey.KeyId,
                            e = exponent,
                            n = modulus,
                            alg = key.SigningAlgorithm
                        };

                        webKeys.Add(webKey);
                    }
                    else if (key.Key is ECDsaSecurityKey ecdsaKey)
                    {
                        var parameters = ecdsaKey.ECDsa.ExportParameters(false);
                        var x = Base64Url.Encode(parameters.Q.X);
                        var y = Base64Url.Encode(parameters.Q.Y);

                        var ecdsaJsonWebKey = new SuktJsonWebKey
                        {
                            kty = "EC",
                            use = "sig",
                            kid = ecdsaKey.KeyId,
                            x = x,
                            y = y,
                            crv = CryptoHelper.GetCrvValueFromCurve(parameters.Curve),
                            alg = key.SigningAlgorithm
                        };
                        webKeys.Add(ecdsaJsonWebKey);
                    }
                    else if (key.Key is JsonWebKey jsonWebKey)
                    {
                        var webKey = new SuktJsonWebKey
                        {
                            kty = jsonWebKey.Kty,
                            use = jsonWebKey.Use ?? "sig",
                            kid = jsonWebKey.Kid,
                            x5t = jsonWebKey.X5t,
                            e = jsonWebKey.E,
                            n = jsonWebKey.N,
                            x5c = jsonWebKey.X5c?.Count == 0 ? null : jsonWebKey.X5c.ToArray(),
                            alg = jsonWebKey.Alg,
                            crv = jsonWebKey.Crv,
                            x = jsonWebKey.X,
                            y = jsonWebKey.Y
                        };

                        webKeys.Add(webKey);
                    }

                }
            }

            
            return webKeys;
        }
    }
}
