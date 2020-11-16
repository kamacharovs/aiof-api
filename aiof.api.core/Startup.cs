using System;
using System.Text.Json;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;

using AutoMapper;
using FluentValidation;

using aiof.api.data;
using aiof.api.services;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace aiof.api.core
{
    public class Startup
    {
        public readonly IConfiguration _config;
        public readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAiofRepository, AiofRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IAssetRepository, AssetRepository>()
                .AddScoped<IGoalRepository, GoalRepository>()
                .AddScoped<ILiabilityRepository, LiabilityRepository>()
                .AddScoped<IEnvConfiguration, EnvConfiguration>()
                .AddScoped<ITenant, Tenant>()
                .AddScoped<FakeDataManager>()
                .AddAutoMapper(typeof(AutoMappingProfileDto).Assembly);

            services.AddHttpClient<IAiofMetadataRepository, AiofMetadataRepository>(Keys.Metadata, x =>
                {
                    x.BaseAddress = new Uri(_config[Keys.MetadataBaseUrl]);
                    x.DefaultRequestHeaders.Add(Keys.Accept, Keys.ApplicationJson);
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddSingleton<AbstractValidator<AssetDto>, AssetDtoValidator>()
                .AddSingleton<AbstractValidator<LiabilityDto>, LiabilityDtoValidator>()
                .AddSingleton<AbstractValidator<LiabilityType>, LiabilityTypeValidator>()
                .AddSingleton<AbstractValidator<GoalDto>, GoalDtoValidator>()
                .AddSingleton<AbstractValidator<SubscriptionDto>, SubscriptionDtoValidator>()
                .AddSingleton<AbstractValidator<AccountDto>, AccountDtoValidator>()
                .AddSingleton<AbstractValidator<UserDto>, UserDtoValidator>();

            services.AddDbContext<AiofContext>(o => o.UseNpgsql(_config[Keys.DataPostgreSQL], x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddLogging();
            services.AddApplicationInsightsTelemetry();
            services.AddHealthChecks();
            services.AddFeatureManagement();
            services.AddHttpContextAccessor();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                Keys.Bearer,
                x =>
                {
                    var rsa = RSA.Create();
                    rsa.FromXmlString(_config[Keys.JwtPublicKey]);

                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _config[Keys.JwtIssuer],
                        ValidateAudience = true,
                        ValidAudience = _config[Keys.JwtAudience],
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new RsaSecurityKey(rsa)
                    };
                });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(_config[Keys.OpenApiVersion], new OpenApiInfo
                {
                    Title = _config[Keys.OpenApiTitle],
                    Version = _config[Keys.OpenApiVersion],
                    Description = _config[Keys.OpenApiDescription],
                    Contact = new OpenApiContact
                    {
                        Name = _config[Keys.OpenApiContactName],
                        Email = _config[Keys.OpenApiContactEmail],
                        Url = new Uri(_config[Keys.OpenApiContactUrl])
                    },
                    License = new OpenApiLicense
                    {
                        Name = _config[Keys.OpenApiLicenseName],
                        Url = new Uri(_config[Keys.OpenApiLicenseUrl]),
                    }
                });
                x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });

            services.AddControllers();
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.WriteIndented = true;
                    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
        }

        public void Configure(IApplicationBuilder app, IServiceProvider services)
        {
            if (_env.IsDevelopment())
            {
                app.UseCors(x => x.WithOrigins(_config[Keys.CorsPortal]).AllowAnyHeader().AllowAnyMethod());
            }

            app.UseAiofExceptionMiddleware();
            app.UseAiofUnauthorizedMiddleware();
            app.UseHealthChecks("/health");
            app.UseSwagger();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });
        }
    }
}
