using System;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

using aiof.api.data;
using aiof.api.services;
using AspNetCoreRateLimit;

namespace aiof.api.core
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public static IConfiguration _config;
        public static IWebHostEnvironment _env;

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
                .AddScoped<IUtilityRepository, UtilityRepository>()
                .AddScoped<IEnvConfiguration, EnvConfiguration>()
                .AddScoped<ITenant, Tenant>()
                .AddScoped<FakeDataManager>()
                .AddAutoMapper(typeof(AutoMappingProfileDto).Assembly)
                .AddAiofFluentValidators();

            services.AddDbContext<AiofContext>(o => o.UseNpgsql(_config[Keys.DataPostgreSQL], o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddHealthChecks();
            services.AddFeatureManagement();
            services.AddLogging()
                .AddApplicationInsightsTelemetry()
                .AddHttpContextAccessor()
                .AddAiofAuthentication()
                .AddAiofSwaggerGen()
                .AddRateLimit();

            services.AddControllers();
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.WriteIndented = true;
                    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseCors(x => x.WithOrigins(_config[Keys.CorsPortal]).AllowAnyHeader().AllowAnyMethod());
            }

            app.UseAiofExceptionMiddleware();
            app.UseAiofUnauthorizedMiddleware();
            app.UseClientRateLimiting();
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
