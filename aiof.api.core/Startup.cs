using System;
using System.Net.Http;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;

using AutoMapper;
using FluentValidation;
using Polly;

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
            services.AddScoped<IAiofRepository, AiofRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<ILiabilityRepository, LiabilityRepository>();
            services.AddScoped<FakeDataManager>();
            services.AddScoped<IEnvConfiguration, EnvConfiguration>();
            services.AddAutoMapper(typeof(AutoMappingProfileDto).Assembly);

            services.AddSingleton(Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .RetryAsync(int.Parse(_configuration[Keys.PollyDefaultRetry])));
            services.AddHttpClient<IAiofMetadataRepository, AiofMetadataRepository>(Keys.Metadata, x =>
                {
                    x.BaseAddress = new Uri(_configuration[Keys.MetadataBaseUrl]);
                    x.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddScoped<AbstractValidator<AssetDto>, AssetDtoValidator>();
            services.AddScoped<AbstractValidator<LiabilityDto>, LiabilityDtoValidator>();
            services.AddScoped<AbstractValidator<GoalDto>, GoalDtoValidator>();

            if (_env.IsDevelopment())
                services.AddDbContext<AiofContext>(o => o.UseInMemoryDatabase(nameof(AiofContext)));
            else
                services.AddDbContext<AiofContext>(o => o.UseNpgsql(_configuration.GetConnectionString(Keys.Database)));

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
                x.SwaggerDoc(_configuration[Keys.OpenApiVersion], new OpenApiInfo
                {
                    Title = _configuration[Keys.OpenApiTitle],
                    Version = _configuration[Keys.OpenApiVersion],
                    Description = _configuration[Keys.OpenApiDescription],
                    Contact = new OpenApiContact
                    {
                        Name = _configuration[Keys.OpenApiContactName],
                        Email = _configuration[Keys.OpenApiContactEmail],
                        Url = new Uri(_configuration[Keys.OpenApiContactUrl])
                    },
                    License = new OpenApiLicense
                    {
                        Name = _configuration[Keys.OpenApiLicenseName],
                        Url = new Uri(_configuration[Keys.OpenApiLicenseUrl]),
                    }
                });
                //x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
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

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });
        }
    }
}
