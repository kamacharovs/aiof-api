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
            services.AddScoped<FakeDataManager>();

            //if (_env.IsDevelopment())
            //    services.AddDbContext<AiofContext>(o => o.UseInMemoryDatabase(nameof(AiofContext)));
            //else
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/ping");

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(e =>
            {
                e.MapControllers();
            });
        }
    }
}
