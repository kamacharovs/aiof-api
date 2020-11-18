using System;
using System.Text.Json;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

using AutoMapper;
using FluentValidation;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.Authorization;

using aiof.api.data;
using aiof.api.services;

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
                .AddAutoMapper(typeof(AutoMappingProfileDto).Assembly)
                .AddAiofGraphQLTypes()
                .AddAiofFluentValidators();

            services.AddHttpClient<IAiofMetadataRepository, AiofMetadataRepository>(Keys.Metadata, x =>
                {
                    x.BaseAddress = new Uri(_config[Keys.MetadataBaseUrl]);
                    x.DefaultRequestHeaders.Add(Keys.Accept, Keys.ApplicationJson);
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddDbContext<AiofContext>(o => o.UseNpgsql(_config[Keys.DataPostgreSQL], o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddLogging();
            services.AddApplicationInsightsTelemetry();
            services.AddHealthChecks();
            services.AddFeatureManagement();
            services.AddHttpContextAccessor();
            services.AddAiofAuthentication(_config);
            services.AddAiofSwaggerGen(_config);

            services.AddGraphQL(options =>
                {
                    options.EnableMetrics = true;
                })
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                .AddSystemTextJson();

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

            app.UseGraphQL<ISchema>();
            app.UseGraphQLPlayground();

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
