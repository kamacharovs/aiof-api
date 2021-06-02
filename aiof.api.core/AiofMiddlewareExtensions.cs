using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;

using FluentValidation;
using AspNetCoreRateLimit;

using aiof.api.data;

namespace aiof.api.core
{
    public static partial class AiofMiddlewareExtensions
    {
        public static IServiceCollection AddAiofAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                Keys.Bearer,
                x =>
                {
                    var rsa = RSA.Create();
                    rsa.FromXmlString(Startup._config[Keys.JwtPublicKey]);

                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Startup._config[Keys.JwtIssuer],
                        ValidateAudience = true,
                        ValidAudience = Startup._config[Keys.JwtAudience],
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new RsaSecurityKey(rsa)
                    };
                });

            return services;
        }

        public static IServiceCollection AddAiofSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(Startup._config[Keys.OpenApiVersion], new OpenApiInfo
                {
                    Title = Startup._config[Keys.OpenApiTitle],
                    Version = Startup._config[Keys.OpenApiVersion],
                    Description = Startup._config[Keys.OpenApiDescription],
                    Contact = new OpenApiContact
                    {
                        Name = Startup._config[Keys.OpenApiContactName],
                        Email = Startup._config[Keys.OpenApiContactEmail],
                        Url = new Uri(Startup._config[Keys.OpenApiContactUrl])
                    },
                    License = new OpenApiLicense
                    {
                        Name = Startup._config[Keys.OpenApiLicenseName],
                        Url = new Uri(Startup._config[Keys.OpenApiLicenseUrl]),
                    }
                });
                x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });

            return services;
        }

        public static IServiceCollection AddAiofFluentValidators(this IServiceCollection services)
        {
            services.AddSingleton<AbstractValidator<LiabilityDto>, LiabilityDtoValidator>()
                .AddSingleton<AbstractValidator<LiabilityType>, LiabilityTypeValidator>()
                .AddSingleton<AbstractValidator<GoalDto>, GoalDtoValidator<GoalDto>>()
                .AddSingleton<AbstractValidator<GoalTripDto>, GoalTripDtoValidator>()
                .AddSingleton<AbstractValidator<GoalHomeDto>, GoalHomeDtoValidator>()
                .AddSingleton<AbstractValidator<GoalCarDto>, GoalCarDtoValidator>()
                .AddSingleton<AbstractValidator<GoalCollegeDto>, GoalCollegeDtoValidator>()
                .AddSingleton<AbstractValidator<SubscriptionDto>, SubscriptionDtoValidator>()
                .AddSingleton<AbstractValidator<AccountDto>, AccountDtoValidator>()
                .AddSingleton<AbstractValidator<UserDto>, UserDtoValidator>()
                .AddSingleton<AbstractValidator<AddressDto>, AddressDtoValidator>()
                .AddSingleton<AbstractValidator<UserDependentDto>, UserDependentDtoValidator>();

            return services;
        }

        public static IServiceCollection AddRateLimit(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.Configure<IpRateLimitOptions>(o =>
            {
                var rateLimitSecond = int.Parse(Startup._config[Keys.RateLimitSecond]);
                var rateLimitMinute = int.Parse(Startup._config[Keys.RateLimitMinute]);
                var rateLimitHour = int.Parse(Startup._config[Keys.RateLimitHour]);

                var aiofProblemBase = new AiofProblemDetailBase
                {
                    Code = StatusCodes.Status429TooManyRequests,
                    Message = Constants.DefaultTooManyRequestsMessage,
                };
                var content = JsonSerializer.Serialize(aiofProblemBase)
                    .Replace("{", "{{")
                    .Replace("}", "}}");

                o.QuotaExceededResponse = new QuotaExceededResponse
                {
                    ContentType = Constants.ApplicationProblemJson,
                    Content = content,
                    StatusCode = StatusCodes.Status429TooManyRequests
                };

                o.EndpointWhitelist = new List<string>
                {
                    "get:/health"
                };

                o.EnableEndpointRateLimiting = false;
                o.StackBlockedRequests = false;
                o.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Period = "1s",
                        Limit = rateLimitSecond,
                    },
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Period = "1m",
                        Limit = rateLimitMinute,
                    },
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Period = "1h",
                        Limit = rateLimitHour,
                    }
                };
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
