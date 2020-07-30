using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using AutoMapper;
using FluentValidation;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public static class Helper
    {
        public const string Category = nameof(Category);
        public const string UnitTest = nameof(UnitTest);
        public const string IntegrationTest = nameof(IntegrationTest);
        
        public static T GetRequiredService<T>()
        {
            var provider = Provider();

            provider.GetRequiredService<FakeDataManager>()
                .UseFakeContext();

            return provider.GetRequiredService<T>();
        }

        private static IServiceProvider Provider()
        {
            var services = new ServiceCollection();

            services.AddScoped<IEnvConfiguration, EnvConfiguration>();
            services.AddScoped<IAiofRepository, AiofRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<FakeDataManager>();

            services.AddSingleton(new MapperConfiguration(x => { x.AddProfile(new AutoMappingProfileDto()); }).CreateMapper());

            services.AddScoped<AbstractValidator<AssetDto>, AssetDtoValidator>();
            services.AddScoped<AbstractValidator<LiabilityDto>, LiabilityDtoValidator>();
            services.AddScoped<AbstractValidator<GoalDto>, GoalDtoValidator>();
            services.AddScoped<AbstractValidator<FinanceDto>, FinanceDtoValidator>();

            services.AddDbContext<AiofContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddLogging();

            return services.BuildServiceProvider();
        }
    }
}
