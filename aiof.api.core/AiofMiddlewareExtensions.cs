using System;
using System.Text.Json;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.core
{
    public static partial class AiofMiddlewareExtensions
    {
        public static IServiceCollection AddAiofAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                Keys.Bearer,
                x =>
                {
                    var rsa = RSA.Create();
                    rsa.FromXmlString(config[Keys.JwtPublicKey]);

                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = config[Keys.JwtIssuer],
                        ValidateAudience = true,
                        ValidAudience = config[Keys.JwtAudience],
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new RsaSecurityKey(rsa)
                    };
                });

            return services;
        }

        public static IServiceCollection AddAiofSwaggerGen(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(config[Keys.OpenApiVersion], new OpenApiInfo
                {
                    Title = config[Keys.OpenApiTitle],
                    Version = config[Keys.OpenApiVersion],
                    Description = config[Keys.OpenApiDescription],
                    Contact = new OpenApiContact
                    {
                        Name = config[Keys.OpenApiContactName],
                        Email = config[Keys.OpenApiContactEmail],
                        Url = new Uri(config[Keys.OpenApiContactUrl])
                    },
                    License = new OpenApiLicense
                    {
                        Name = config[Keys.OpenApiLicenseName],
                        Url = new Uri(config[Keys.OpenApiLicenseUrl]),
                    }
                });
                x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });

            return services;
        }

        public static IApplicationBuilder UseAiofExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AiofExceptionMiddleware>();
        }

        public static IApplicationBuilder UseAiofUnauthorizedMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AiofUnauthorizedMiddleware>();
        }
    }
}
