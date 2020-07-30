using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;

using AutoMapper;
using FluentValidation;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.core
{
    public class Startup
    {
        public readonly IConfiguration _configuration;
        public readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IEnvConfiguration, EnvConfiguration>();
            services.AddScoped<IAiofRepository, AiofRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<FakeDataManager>();

            services.AddSingleton(new MapperConfiguration(x => { x.AddProfile(new AutoMappingProfileDto()); }).CreateMapper());
            services.AddHttpClient<IAiofMetadataRepository, AiofMetadataRepository>("metadata", c =>
                {
                    c.BaseAddress = new Uri(_configuration[$"{Keys.Metadata}:{Keys.BaseUrl}"]);
                    c.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddScoped<AbstractValidator<AssetDto>, AssetDtoValidator>();
            services.AddScoped<AbstractValidator<LiabilityDto>, LiabilityDtoValidator>();
            services.AddScoped<AbstractValidator<GoalDto>, GoalDtoValidator>();
            services.AddScoped<AbstractValidator<FinanceDto>, FinanceDtoValidator>();

            if (_env.IsDevelopment())
                services.AddDbContext<AiofContext>(o => o.UseInMemoryDatabase(nameof(AiofContext)));
            else
                services.AddDbContext<AiofContext>(o =>
                    o.UseNpgsql(_configuration.GetConnectionString("PostgreSQL")
                        .Replace("$DB_HOST", _configuration["DB_HOST"])
                        .Replace("$DB_NAME", _configuration["DB_NAME"])
                        .Replace("$DB_USER", _configuration["DB_USER"])
                        .Replace("$DB_PASSWORD", _configuration["DB_PASSWORD"])));

            services.AddHealthChecks();
            services.AddLogging();
            services.AddFeatureManagement();

            services.AddControllers();
            services.AddMvcCore()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.WriteIndented = true;
                    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(_configuration[$"{Keys.OpenApi}:{Keys.Version}"], new OpenApiInfo
                {
                    Title = _configuration[$"{Keys.OpenApi}:{Keys.Title}"],
                    Version = _configuration[$"{Keys.OpenApi}:{Keys.Version}"],
                    Description = _configuration[$"{Keys.OpenApi}:{Keys.Description}"],
                    Contact = new OpenApiContact
                    {
                        Name = _configuration[$"{Keys.OpenApi}:{Keys.Contact}:{Keys.Name}"],
                        Email = _configuration[$"{Keys.OpenApi}:{Keys.Contact}:{Keys.Email}"],
                        Url = new Uri(_configuration[$"{Keys.OpenApi}:{Keys.Contact}:{Keys.Url}"])
                    },
                    License = new OpenApiLicense
                    {
                        Name = _configuration[$"{Keys.OpenApi}:{Keys.License}:{Keys.Name}"],
                        Url = new Uri(_configuration[$"{Keys.OpenApi}:{Keys.License}:{Keys.Url}"]),
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IServiceProvider services)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                services.GetRequiredService<FakeDataManager>()
                    .UseFakeContext();
            }

            app.UseHealthChecks("/ping");
            app.UseAiofExceptionMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint($"/swagger/{_configuration[$"{Keys.OpenApi}:{Keys.Version}"]}/swagger.json",
                    $"{_configuration[$"{Keys.OpenApi}:{Keys.Title}"]} {_configuration[$"{Keys.OpenApi}:{Keys.Version}"]}");
                x.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });
        }
    }
}
