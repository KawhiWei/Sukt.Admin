//using IdentityServer4.Models;
//using Sukt.Module.Core;
//using System.Collections.Generic;

//namespace Sukt.Core.API.Config
//{
//    public class Config
//    {
//        /// <summary>
//        /// 资源授权类型
//        /// </summary>
//        /// <returns></returns>
//        public static IEnumerable<IdentityResource> GetIdentityResources()
//        {
//            return new List<IdentityResource>
//            {
//                new IdentityResources.OpenId(),
//                new IdentityResources.Profile(),
//            };
//        }
//        /// <summary>
//        /// API资源名称
//        /// </summary>
//        /// <returns></returns>
//        public static IEnumerable<ApiResource> GetApiResources()
//        {
//            return new List<ApiResource> {
//                new ApiResource() {
//                    Name="Sukt.Core.API.Agile.Admin",
//                    DisplayName="通用后台管理Admin敏捷开发框架",
//                    // include the following using claims in access token (in addition to subject id)
//                    //requires using using IdentityModel;
//                    UserClaims = { Shared.JwtClaimTypes.Name, Shared.JwtClaimTypes.Role },
//                    ApiSecrets = new List<Secret>()
//                    {
//                        new Secret("SuktCore.API.Admin_secret".Sha256())
//                    },
//                    Scopes =//必须和APiScope表内的名称相同
//                    {
//                        "SuktCore.API.Admin"
//                    }
//                }
//            };
//        }
//        // 客户端
//        public static IEnumerable<Client> GetClients()
//        {
//            // javascript client
//            return new List<Client> {
//                new Client {
//                    ClientId = "Sukt.Core.ReactAdmin.Spa",
//                    ClientName = "Sukt.Core、React的单页面敏捷开发框架",
//                    AllowedGrantTypes = Shared.GrantTypes.Implicit,
//                    AllowAccessTokensViaBrowser = true,
//                    ClientSecrets =
//                    {
//                        new Secret("Sukt.Core.ReactAdmin.Spa.Secret".Sha256())
//                    },

//                    RedirectUris =           { "http://localhost:6017/callback" },
//                    PostLogoutRedirectUris = { "http://localhost:6017"},
//                    AllowedCorsOrigins =     { "http://localhost:6017"},
//                    AllowedScopes = {
//                        IdentityServerConstants.StandardScopes.OpenId,
//                        IdentityServerConstants.StandardScopes.Profile,
//                        "SuktCore.API.Admin"
//                    },

//                },
//            };
//        }
//        public static IEnumerable<ApiScope> GetApiScopes()
//        {
//            return new[]
//            {
//                new ApiScope()
//                {
//                    Name = "SuktCore.API.Admin",
//                    DisplayName = "添加授权客户端访问admin框架资源范围",
//                }
//            };
//        }
//    }
//}
