using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Sukt.Core.IdentityServer4Store
{
    public class Config
    {
        /// <summary>
        /// 资源授权类型
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles", "角色", new List<string> { JwtClaimTypes.Role }),
            };
        }

        /// <summary>
        /// API资源名称
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource() {
                    Name="SuktCore.API",
                    // include the following using claims in access token (in addition to subject id)
                    //requires using using IdentityModel;
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Role },
                    ApiSecrets = new List<Secret>()
                    {
                        new Secret("SuktCore.API_secret".Sha256())
                    },
                    Scopes =
                    {
                        "SuktCore.API"
                    }
                }
            };
        }

        // 客户端
        public static IEnumerable<Client> GetClients()
        {
            // javascript client
            return new List<Client> {
                new Client {
                    ClientId = "SuktCoreWebClient",
                    ClientName = "SuktCoreWebClient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    ClientSecrets =
                    {
                        new Secret("SuktCore.API_secret".Sha256())
                    },

                    RedirectUris =           { "http://localhost:8080/Callback" },
                    PostLogoutRedirectUris = { "http://localhost:8080" },
                    AllowedCorsOrigins =     { "http://localhost:8080" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "SuktCore.API"
                    },
                },
                new Client {
                    ClientId = "SuktCoreWebClientpwd",
                    ClientName = "SuktCoreWebClientpwd",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    ClientSecrets =
                    {
                        new Secret("SuktCoreAPI".Sha256())
                    },

                    RedirectUris =           { "http://localhost:8080/Callback" },
                    PostLogoutRedirectUris = { "http://localhost:8080" },
                    AllowedCorsOrigins =     { "http://localhost:8080" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "SuktCore.API"
                    },
                },
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope()
                {
                    Name = "SuktCore.API",
                    DisplayName = "SuktCore.API",
                }
            };
        }
    }
}