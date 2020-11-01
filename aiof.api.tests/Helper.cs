using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;

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
    public class ServiceHelper
    {
        public int? UserId { get; set; }
        public int? ClientId { get; set; }

        public T GetRequiredService<T>()
        {
            var provider = Services().BuildServiceProvider();

            provider.GetRequiredService<FakeDataManager>()
                .UseFakeContext();

            return provider.GetRequiredService<T>();
        }

        public ServiceCollection Services()
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
            
            services.AddScoped<ITenant>(x => GetMockTenant());
            services.AddSingleton(new MapperConfiguration(x => { x.AddProfile(new AutoMappingProfileDto()); }).CreateMapper());

            services.AddScoped<AbstractValidator<AssetDto>, AssetDtoValidator>()
                .AddScoped<AbstractValidator<LiabilityDto>, LiabilityDtoValidator>()
                .AddScoped<AbstractValidator<LiabilityType>, LiabilityTypeValidator>()
                .AddScoped<AbstractValidator<GoalDto>, GoalDtoValidator>()
                .AddScoped<AbstractValidator<SubscriptionDto>, SubscriptionDtoValidator>()
                .AddScoped<AbstractValidator<UserDto>, UserDtoValidator>();

            services.AddDbContext<AiofContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddLogging();
            services.AddHttpContextAccessor();

            return services;
        }

        public IAiofMetadataRepository GetMockedMetadataRepo()
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

        public ITenant GetMockTenant()
        {
            var mockedTenant = new Mock<ITenant>();
            var userId = UserId ?? 1;
            var clientId = ClientId ?? 1;

            mockedTenant.Setup(x => x.UserId).Returns(userId);
            mockedTenant.Setup(x => x.ClientId).Returns(clientId);

            return mockedTenant.Object;
        }
    }

    public static class Helper
    {
        public const string Category = nameof(Category);
        public const string UnitTest = nameof(UnitTest);
        public const string IntegrationTest = nameof(IntegrationTest);


        #region Unit Tests
        static FakeDataManager _Fake
            => new ServiceHelper().GetRequiredService<FakeDataManager>() ?? throw new ArgumentNullException(nameof(FakeDataManager));

        public static IEnumerable<object[]> UsersId()
        {
            return _Fake.GetFakeUsersData(
                id: true);
        }
        public static IEnumerable<object[]> UsersIdUsername()
        {
            return _Fake.GetFakeUsersData(
                id: true,
                username: true);
        }
        public static IEnumerable<object[]> UserProfilesId()
        {
            return _Fake.GetFakeUserProfilesData(
                userId: true);
        }
        public static IEnumerable<object[]> UserProfilesIdUsername()
        {
            return _Fake.GetFakeUserProfilesData(
                userId: true,
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
        public static IEnumerable<object[]> AssetsIdUsersId()
        {
            return _Fake.GetFakeAssetsData(
                id: true,
                userId: true);
        }
        public static IEnumerable<object[]> AssetsUsersId()
        {
            return _Fake.GetFakeAssetsData(
                userId: true);
        }
        public static IEnumerable<object[]> AssetsPublicKey()
        {
            return _Fake.GetFakeAssetsData(
                publicKey: true);
        }
        public static IEnumerable<object[]> AssetsTypeNameUserId()
        {
            return _Fake.GetFakeAssetsData(
                typeName: true,
                userId: true);
        }
        public static IEnumerable<object[]> Assets()
        {
            return _Fake.GetFakeAssetsData(
                name: true,
                typeName: true,
                value: true,
                userId: true);
        }

        public static IEnumerable<object[]> SubscriptionsId()
        {
            return _Fake.GetFakeSubscriptionsData(
                userId: true,
                id: true);
        }
        public static IEnumerable<object[]> SubscriptionsPublicKey()
        {
            return _Fake.GetFakeSubscriptionsData(
                userId: true,
                publicKey: true);
        }

        public static IEnumerable<object[]> AccountsId()
        {
            return _Fake.GetFakeAccountsData(
                userId: true,
                id: true);
        }
        public static IEnumerable<object[]> AccountsPublicKey()
        {
            return _Fake.GetFakeAccountsData(
                userId: true,
                publicKey: true);
        }
        public static IEnumerable<object[]> AccountsUserId()
        {
            return _Fake.GetFakeAccountsData(
                userId: true);
        }

        public static UserDto RandomUserDto(int userId = 1)
        {
            return new Faker<UserDto>()
                .RuleFor(x => x.Assets, f => FakerAssetDtos())
                .RuleFor(x => x.Liabilities, f => FakerLiabilityDtos(userId))
                .RuleFor(x => x.Goals, f => FakerGoalDtos(userId))
                .Generate();
        }

        public static UserProfileDto RandomUserProfileDto()
        {
            return new Faker<UserProfileDto>()
                .RuleFor(x => x.Gender, f => f.Person.Gender.ToString())
                .RuleFor(x => x.DateOfBirth, f => f.Date.Past(f.Random.Int(18, 99)))
                .RuleFor(x => x.EducationLevel, f => EducationLevels.Bachelors.ToString())
                .Generate();
        }

        public static List<AssetDto> FakerAssetDtos()
        {
            return new Faker<AssetDto>()
                .RuleFor(x => x.Name, f => f.Random.String2(10))
                .RuleFor(x => x.TypeName, f => f.Random.String2(5))
                .RuleFor(x => x.Value, f => f.Random.Int(1000, 10000))
                .Generate(GeneratedAmount);
        }
        public static List<LiabilityDto> FakerLiabilityDtos(int userId = 1)
        {
            return new Faker<LiabilityDto>()
                .RuleFor(x => x.Name, f => f.Random.String2(10))
                .RuleFor(x => x.TypeName, f => f.Random.String2(5))
                .RuleFor(x => x.Value, f => f.Random.Int(1000, 10000))
                .Generate(GeneratedAmount);
        }
        public static List<GoalDto> FakerGoalDtos(int userId = 1)
        {
            return new Faker<GoalDto>()
                .RuleFor(x => x.Name, f => f.Random.String())
                .RuleFor(x => x.TypeName, f => "save for a rainy day")
                .RuleFor(x => x.Amount, f => f.Random.Decimal(5000, 10000))
                .RuleFor(x => x.CurrentAmount, f => f.Random.Decimal(1000, 4000))
                .RuleFor(x => x.Contribution, f => f.Random.Decimal(700, 900))
                .RuleFor(x => x.ContributionFrequencyName, f => "monthly")
                .Generate(GeneratedAmount);
        }

        public static SubscriptionDto RandomSubscriptionDto(int userId = 1)
        {
            return new Faker<SubscriptionDto>()
                .RuleFor(x => x.Name, f => f.Random.String2(10))
                .RuleFor(x => x.Description, f => f.Random.String2(50))
                .RuleFor(x => x.Amount, f => f.Random.Decimal(20, 150))
                .RuleFor(x => x.PaymentFrequencyName, f => "monthly")
                .RuleFor(x => x.PaymentLength, f => 365)
                .RuleFor(x => x.From, f => f.Random.String2(10))
                .RuleFor(x => x.Url, f => f.Internet.Url())
                .RuleFor(x => x.UserId, f => userId)
                .Generate();
        }

        public static AccountDto RandomAccountDto(int userId = 1)
        {
            return new Faker<AccountDto>()
                .RuleFor(x => x.Name, f => f.Random.String2(10))
                .RuleFor(x => x.Description, f => f.Random.String2(50))
                .RuleFor(x => x.TypeName, f => "retirement")
                .RuleFor(x => x.UserId, f => userId)
                .Generate();
        }

        public static IEnumerable<object[]> RandomUserDtos()
        {
            return new List<object[]>
            {
                new object[]
                {
                    FakerAssetDtos(),
                    FakerLiabilityDtos(),
                    FakerGoalDtos()
                }
            };
        }

        public static IEnumerable<object[]> RandomAssetDtos()
        {
            var toReturn = new List<object[]>();

            foreach (var fakeAssetDto in FakerAssetDtos())
            {
                toReturn.Add(new object[]
                {
                    fakeAssetDto.Name,
                    fakeAssetDto.TypeName,
                    fakeAssetDto.Value
                });
            }

            return toReturn;
        }
        public static IEnumerable<object[]> RandomAssetDtosList()
        {
            return new List<object[]>
            {
                new object[]
                {
                    FakerAssetDtos()
                }
            };
        }

        public static IEnumerable<object[]> RandomeGoalDtosList()
        {
            return new List<object[]>
            {
                new object[]
                {
                    FakerGoalDtos()
                }
            };
        }

        public static IEnumerable<object[]> RandomLiabilityDtosList()
        {
            return new List<object[]>
            {
                new object[]
                {
                    FakerLiabilityDtos()
                }
            };
        }

        public static int GeneratedAmount = 3;
        #endregion
    }
}
