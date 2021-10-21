// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Sukt.AuthServer.AccessTokenValidation
{
    internal class ConfigureInternalOptions : 
        IConfigureNamedOptions<JwtBearerOptions>,
        IConfigureNamedOptions<OAuth2IntrospectionOptions>
    {
        private readonly SuktAuthServerAuthenticationOptions _identityServerOptions;
        private string _scheme;

        public ConfigureInternalOptions(SuktAuthServerAuthenticationOptions identityServerOptions, string scheme)
        {
            _identityServerOptions = identityServerOptions;
            _scheme = scheme;
        }

        public void Configure(string name, JwtBearerOptions options)
        {
            if (name == _scheme + SuktAuthServerAuthenticationDefaults.JwtAuthenticationScheme &&
                _identityServerOptions.SupportsJwt)
            {
                _identityServerOptions.ConfigureJwtBearer(options);
            }
        }

        public void Configure(string name, OAuth2IntrospectionOptions options)
        {
            if (name == _scheme + SuktAuthServerAuthenticationDefaults.IntrospectionAuthenticationScheme &&
                _identityServerOptions.SupportsIntrospection)
            {
                _identityServerOptions.ConfigureIntrospection(options);
            }
        }

        public void Configure(JwtBearerOptions options)
        { }

        public void Configure(OAuth2IntrospectionOptions options)
        { }
    }
}