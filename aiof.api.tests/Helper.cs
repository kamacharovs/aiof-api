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

            services.AddScoped<IAiofRepository, AiofRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IAssetRepository, AssetRepository>()
                .AddScoped<IGoalRepository, GoalRepository>()
                .AddScoped<ILiabilityRepository, LiabilityRepository>()
                .AddScoped<IEnvConfiguration, EnvConfiguration>()
                .AddScoped<FakeDataManager>()
                .AddSingleton(GetMockedMetadataRepo());

            services.AddSingleton(new MapperConfiguration(x => { x.AddProfile(new AutoMappingProfileDto()); }).CreateMapper());

            services.AddScoped<AbstractValidator<AssetDto>, AssetDtoValidator>()
                .AddScoped<AbstractValidator<LiabilityDto>, LiabilityDtoValidator>()
                .AddScoped<AbstractValidator<LiabilityType>, LiabilityTypeValidator>()
                .AddScoped<AbstractValidator<GoalDto>, GoalDtoValidator>()
                .AddScoped<AbstractValidator<SubscriptionDto>, SubscriptionDtoValidator>();

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
                It.IsAny<decimal>(),
                It.IsAny<decimal>(),
                It.IsAny<decimal>(),
                It.IsAny<string>()
            )).ReturnsAsync(
                new List<LoanPayment>
                {
                    new LoanPayment
                    {
                        InitialBalance =  15000,
                        EndingBalance = 14818.14M,
                        Interest = 56.25M,
                        Month = 1,
                        Payment = 238.11M,
                        Principal = 181.86M
                    },
                    new LoanPayment
                    {
                        InitialBalance =  14818.14M,
                        EndingBalance = 14635.6M,
                        Interest = 55.57M,
                        Month = 2,
                        Payment = 238.11M,
                        Principal = 182.54M
                    }
                }
            );

            return mockedRepo.Object;
        }



        #region Unit Tests
        static FakeDataManager _Fake
            => GetRequiredService<FakeDataManager>() ?? throw new ArgumentNullException(nameof(FakeDataManager));

        public static IEnumerable<object[]> UsersId()
        {
            return _Fake.GetFakeUsersData(
                id: true);
        }
        public static IEnumerable<object[]> UsersUsername()
        {
            return _Fake.GetFakeUsersData(
                username: true);
        }
        public static IEnumerable<object[]> UserProfilesUsername()
        {
            return _Fake.GetFakeUserProfilesData(
                username: true);
        }

        public static IEnumerable<object[]> AssetsNameTypeNameValueUserId()
        {
            return _Fake.GetFakeAssetsData(
                name: true,
                typeName: true,
                value: true,
                userId: true);
        }
        public static IEnumerable<object[]> AssetsId()
        {
            return _Fake.GetFakeAssetsData(
                id: true);
        }
        public static IEnumerable<object[]> AssetsPublicKey()
        {
            return _Fake.GetFakeAssetsData(
                publicKey: true);
        }
        public static IEnumerable<object[]> AssetsTypeName()
        {
            return _Fake.GetFakeAssetsData(
                typeName: true);
        }
        public static IEnumerable<object[]> Assets()
        {
            return _Fake.GetFakeAssetsData(
                name: true,
                typeName: true,
                value: true,
                userId: true);
        }

        public static IEnumerable<object[]> RandomAssetDtos()
        {
            var fakeAssetDtos = new Faker<AssetDto>()
                .RuleFor(x => x.Name, f => "car to buy")
                .RuleFor(x => x.TypeName, f => "car")
                .RuleFor(x => x.Value, f => f.Random.Int(1000, 10000))
                .RuleFor(x => x.UserId, f => f.Random.Int(1, 2))
                .Generate(GeneratedAmount);

            var toReturn = new List<object[]>();

            foreach (var fakeAssetDto in fakeAssetDtos)
            {
                toReturn.Add(new object[] 
                { 
                    fakeAssetDto.Name, 
                    fakeAssetDto.TypeName, 
                    fakeAssetDto.Value, 
                    fakeAssetDto.UserId
                });
            }

            return toReturn;
        }

        public static IEnumerable<object[]> RandomeGoalDtos()
        {
            var fakeGoalDtos = new Faker<GoalDto>()
                .RuleFor(x => x.Name, f => f.Random.String())
                .RuleFor(x => x.TypeName, f => "save for a rainy day")
                .RuleFor(x => x.Amount, f => f.Random.Decimal(5000, 10000))
                .RuleFor(x => x.CurrentAmount, f => f.Random.Decimal(1000, 4000))
                .RuleFor(x => x.Contribution, f => f.Random.Decimal(700, 900))
                .RuleFor(x => x.ContributionFrequencyName, f => "monthly")
                .RuleFor(x => x.UserId, f => f.Random.Int(1, 2))
                .Generate(GeneratedAmount);

            var toReturn = new List<object[]>();

            foreach (var fakeGoalDto in fakeGoalDtos)
            {
                toReturn.Add(new object[] 
                { 
                    fakeGoalDto.Name, 
                    fakeGoalDto.TypeName,
                    fakeGoalDto.Amount,
                    fakeGoalDto.CurrentAmount,
                    fakeGoalDto.Contribution,
                    fakeGoalDto.ContributionFrequencyName,
                    fakeGoalDto.PlannedDate,
                    fakeGoalDto.UserId
                });
            }

            return toReturn;
        }

        public static IEnumerable<object[]> RandomLiabilityDtos()
        {
            var fakeLiabilityDtos = new Faker<LiabilityDto>()
                .RuleFor(x => x.Name, f => f.Random.String())
                .RuleFor(x => x.TypeName, f => "car")
                .RuleFor(x => x.Value, f => f.Random.Int(1000, 10000))
                .RuleFor(x => x.UserId, f => f.Random.Int(1, 2))
                .Generate(GeneratedAmount);

            var toReturn = new List<object[]>();

            foreach (var fakeLiabilityDto in fakeLiabilityDtos)
            {
                toReturn.Add(new object[] 
                { 
                    fakeLiabilityDto.Name, 
                    fakeLiabilityDto.TypeName, 
                    fakeLiabilityDto.Value, 
                    fakeLiabilityDto.UserId
                });
            }

            return toReturn;
        }

        public static UserProfileDto RandomUserProfileDto()
        {
            return new Faker<UserProfileDto>()
                .RuleFor(x => x.Gender, f => f.Person.Gender.ToString())
                .RuleFor(x => x.DateOfBirth, f => f.Date.Past(f.Random.Int(18, 99)))
                .RuleFor(x => x.EducationLevel, f => "Bachelors")
                .Generate();
        }

        public static int GeneratedAmount = 3; 
        #endregion
    }
}
