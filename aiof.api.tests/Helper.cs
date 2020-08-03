using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using AutoMapper;
using FluentValidation;
using Moq;
using Bogus;

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
            services.AddSingleton(GetMockedMetadataRepo());

            services.AddSingleton(new MapperConfiguration(x => { x.AddProfile(new AutoMappingProfileDto()); }).CreateMapper());

            services.AddScoped<AbstractValidator<AssetDto>, AssetDtoValidator>();
            services.AddScoped<AbstractValidator<LiabilityDto>, LiabilityDtoValidator>();
            services.AddScoped<AbstractValidator<GoalDto>, GoalDtoValidator>();
            services.AddScoped<AbstractValidator<FinanceDto>, FinanceDtoValidator>();

            services.AddDbContext<AiofContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddLogging();

            return services.BuildServiceProvider();
        }

        public static IAiofMetadataRepository GetMockedMetadataRepo()
        {
            var mockedRepo = new Mock<IAiofMetadataRepository>();

            mockedRepo.Setup(x => x.GetFrequenciesAsync())
                .ReturnsAsync(
                    new List<string>
                    {
                        "daily",
                        "monthly",
                        "quarterly",
                        "half-year",
                        "yearly"
                    });

            mockedRepo.Setup(x => x.GetLoanPaymentsAsync(
                It.IsAny<double>(),
                It.IsAny<double>(),
                It.IsAny<double>(),
                It.IsAny<string>()
            )).ReturnsAsync(
                new List<object>
                {
                    new
                    {
                        initialBalance =  15000,
                        endingBalance = 14818.14,
                        interest = 56.25,
                        month = 1,
                        payment = 238.11,
                        principal = 181.86
                    },
                    new
                    {
                        initialBalance =  14818.14,
                        endingBalance = 14635.6,
                        interest = 55.57,
                        month = 2,
                        payment = 238.11,
                        principal = 182.54
                    }
                }
            );

            return mockedRepo.Object;
        }



        #region Unit Tests
        static FakeDataManager _Fake
            => Helper.GetRequiredService<FakeDataManager>() ?? throw new ArgumentNullException(nameof(FakeDataManager));

        public static IEnumerable<object[]> AssetsNameTypeNameValueFinanceId()
        {
            return _Fake.GetFakeAssetsData(
                name: true,
                typeName: true,
                value: true,
                financeId: true);
        }
        public static IEnumerable<object[]> AssetsId()
        {
            return _Fake.GetFakeAssetsData(
                id: true);
        }
        public static IEnumerable<object[]> AssetsTypeName()
        {
            return _Fake.GetFakeAssetsData(
                typeName: true);
        }

        public static IEnumerable<object[]> RandomAssetDtos()
        {
            var fakeAssetDtos = new Faker<AssetDto>()
                .RuleFor(x => x.Name, f => f.Random.String())
                .RuleFor(x => x.TypeName, f => "car")
                .RuleFor(x => x.Value, f => f.Random.Int(1000, 10000))
                .RuleFor(x => x.FinanceId, f => 1)
                .Generate(3);

            var toReturn = new List<object[]>();

            foreach (var fakeAssetDto in fakeAssetDtos)
            {
                toReturn.Add(new object[] 
                { 
                    fakeAssetDto.Name, 
                    fakeAssetDto.TypeName, 
                    fakeAssetDto.Value, 
                    fakeAssetDto.FinanceId
                });
            }

            return toReturn;
        }
        #endregion
    }
}
