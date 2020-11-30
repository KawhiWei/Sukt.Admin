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
                },
                new ApiResource() {
                    Name="Destiny.Core.Flow.API",
                    // include the following using claims in access token (in addition to subject id)
                    //requires using using IdentityModel;
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Role },
                    ApiSecrets = new List<Secret>()
                    {
                        new Secret("DestinyCoreFlowAPI_secret".Sha256())
                    },
                    Scopes =
                    {
                        "Destiny.Core.Flow.API"
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
                    ClientId = "BasicsWebClient",
                    ClientName = "BasicsWebClient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    ClientSecrets =
                    {
                        new Secret("BasicsWebClient_secret".Sha256())
                    },
                    AllowOfflineAccess=true,//返回刷新token
                    RedirectUris ={ "http://localhost:8848/callback","https://admin.destinycore.club" },
                    PostLogoutRedirectUris = { "http://localhost:8848" ,"https://admin.destinycore.club"},
                    AllowedCorsOrigins =     { "http://localhost:8848" ,"https://admin.destinycore.club"},
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "SuktCore.API",
                        "Destiny.Core.Flow.API"
                    },
                },
                new Client {

                    ClientId = "BasicsPwdClient",
                    ClientName = "BasicsPwdClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    ClientSecrets =
                    {
                        new Secret("BasicsPwdClient_secret".Sha256())
                    },
                    AllowOfflineAccess=true,//返回刷新token
                    RedirectUris =           { "http://localhost:8848/callback","https://admin.destinycore.club" },
                    PostLogoutRedirectUris = { "http://localhost:8848" ,"https://admin.destinycore.club"},
                    AllowedCorsOrigins =     { "http://localhost:8848" ,"https://admin.destinycore.club"},
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "SuktCore.API",
                        "Destiny.Core.Flow.API"
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
                },
                new ApiScope()
                {
                    Name = "Destiny.Core.Flow.API",
                    DisplayName = "Destiny.Core.Flow.API",
                }
            };
        }
    }
}
