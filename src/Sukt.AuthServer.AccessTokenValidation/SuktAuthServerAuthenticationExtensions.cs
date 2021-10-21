// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sukt.AuthServer.AccessTokenValidation;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extensions for registering the SuktAuthServer authentication handler
    /// </summary>
    public static class SuktAuthServerAuthenticationExtensions
    {
        /// <summary>
        /// Registers the SuktAuthServer authentication handler.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSuktAuthServerAuthentication(this AuthenticationBuilder builder)
            => builder.AddSuktAuthServerAuthentication(SuktAuthServerAuthenticationDefaults.AuthenticationScheme);

        /// <summary>
        /// Registers the SuktAuthServer authentication handler.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSuktAuthServerAuthentication(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddSuktAuthServerAuthentication(authenticationScheme, configureOptions: null);

        /// <summary>
        /// Registers the SuktAuthServer authentication handler.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="configureOptions">The configure options.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSuktAuthServerAuthentication(this AuthenticationBuilder builder, Action<SuktAuthServerAuthenticationOptions> configureOptions) =>
            builder.AddSuktAuthServerAuthentication(SuktAuthServerAuthenticationDefaults.AuthenticationScheme, configureOptions);

        /// <summary>
        /// Registers the SuktAuthServer authentication handler.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="configureOptions">The configure options.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSuktAuthServerAuthentication(this AuthenticationBuilder builder, string authenticationScheme, Action<SuktAuthServerAuthenticationOptions> configureOptions)
        {
            builder.AddJwtBearer(authenticationScheme + SuktAuthServerAuthenticationDefaults.JwtAuthenticationScheme, configureOptions: null);
            builder.AddOAuth2Introspection(authenticationScheme + SuktAuthServerAuthenticationDefaults.IntrospectionAuthenticationScheme, configureOptions: null);

            builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>>(services =>
            {
                var monitor = services.GetRequiredService<IOptionsMonitor<SuktAuthServerAuthenticationOptions>>();
                return new ConfigureInternalOptions(monitor.Get(authenticationScheme), authenticationScheme);
            });
            
            builder.Services.AddSingleton<IConfigureOptions<OAuth2IntrospectionOptions>>(services =>
            {
                var monitor = services.GetRequiredService<IOptionsMonitor<SuktAuthServerAuthenticationOptions>>();
                return new ConfigureInternalOptions(monitor.Get(authenticationScheme), authenticationScheme);
            });
            
            return builder.AddScheme<SuktAuthServerAuthenticationOptions, SuktAuthServerAuthenticationHandler>(authenticationScheme, configureOptions);
        }

        /// <summary>
        /// Registers the SuktAuthServer authentication handler.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="authenticationScheme">The authentication scheme.</param>
        /// <param name="jwtBearerOptions">The JWT bearer options.</param>
        /// <param name="introspectionOptions">The introspection options.</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSuktAuthServerAuthentication(this AuthenticationBuilder builder, string authenticationScheme, 
            Action<JwtBearerOptions> jwtBearerOptions,
            Action<OAuth2IntrospectionOptions> introspectionOptions)
        {
            if (jwtBearerOptions != null)
            {
                builder.AddJwtBearer(authenticationScheme + SuktAuthServerAuthenticationDefaults.JwtAuthenticationScheme, jwtBearerOptions);
            }

            if (introspectionOptions != null)
            {
                builder.AddOAuth2Introspection(authenticationScheme + SuktAuthServerAuthenticationDefaults.IntrospectionAuthenticationScheme, introspectionOptions);
            }

            return builder.AddScheme<SuktAuthServerAuthenticationOptions, SuktAuthServerAuthenticationHandler>(authenticationScheme, (o) => { });
        }
    }
}