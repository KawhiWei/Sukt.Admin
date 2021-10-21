// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Sukt.AuthServer.AccessTokenValidation
{
    /// <summary>
    /// Constants for SuktAuthServer authentication.
    /// </summary>
    public class SuktAuthServerAuthenticationDefaults
    {
        /// <summary>
        /// The authentication scheme
        /// </summary>
        public const string AuthenticationScheme = "Bearer";

        /// <summary>
        /// Jwt类型的标头
        /// </summary>
        public const string JwtAccessTokenTyp = "at+jwt";

        internal const string IntrospectionAuthenticationScheme = "SuktAuthServerAuthenticationIntrospection";
        internal const string JwtAuthenticationScheme = "SuktAuthenticationJwt";
        internal const string TokenItemsKey = "suktsrv:tokenvalidation:token";
        internal const string EffectiveSchemeKey = "suktsrv:tokenvalidation:effective:";
    }
}