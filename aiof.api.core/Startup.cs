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
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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
            services.AddScoped<FakeDataManager>();

            services.AddHttpClient<IAiofMetadataRepository, AiofMetadataRepository>("metadata", c =>
                {
                    c.BaseAddress = new Uri(_configuration["Metadata:BaseUrl"]);
                    c.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

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

            services.AddControllers();
            services.AddMvcCore()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.WriteIndented = true;
                    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(_configuration["OpenApi:Version"], new OpenApiInfo
                {
                    Title = _configuration["OpenApi:Title"],
                    Version = _configuration["OpenApi:Version"],
                    Description = _configuration["OpenApi:Description"],
                    Contact = new OpenApiContact
                    {
                        Name = _configuration["OpenApi:Contact:Name"],
                        Email = _configuration["OpenApi:Contact:Email"],
                        Url = new Uri(_configuration["OpenApi:Contact:Url"])
                    },
                    License = new OpenApiLicense
                    {
                        Name = _configuration["OpenApi:License:Name"],
                        Url = new Uri(_configuration["OpenApi:License:Url"]),
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
                x.SwaggerEndpoint($"/swagger/{_configuration["OpenApi:Version"]}/swagger.json",
                    $"{_configuration["OpenApi:Title"]} {_configuration["OpenApi:Version"]}");
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
