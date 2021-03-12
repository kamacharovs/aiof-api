using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

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
    [ExcludeFromCodeCoverage]
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
                .AddScoped<IUtilityRepository, UtilityRepository>()
                .AddScoped<IEnvConfiguration, EnvConfiguration>()
                .AddScoped<FakeDataManager>();
            
            services.AddScoped<ITenant>(x => GetMockTenant());
            services.AddSingleton(new MapperConfiguration(x => { x.AddProfile(new AutoMappingProfileDto()); }).CreateMapper());

            services.AddSingleton<AbstractValidator<AssetDto>, AssetDtoValidator>()
                .AddSingleton<AbstractValidator<LiabilityDto>, LiabilityDtoValidator>()
                .AddSingleton<AbstractValidator<LiabilityType>, LiabilityTypeValidator>()
                .AddSingleton<AbstractValidator<GoalDto>, GoalDtoValidator<GoalDto>>()
                .AddSingleton<AbstractValidator<GoalTripDto>, GoalTripDtoValidator>()
                .AddSingleton<AbstractValidator<GoalHomeDto>, GoalHomeDtoValidator>()
                .AddSingleton<AbstractValidator<GoalCarDto>, GoalCarDtoValidator>()
                .AddSingleton<AbstractValidator<SubscriptionDto>, SubscriptionDtoValidator>()
                .AddSingleton<AbstractValidator<AccountDto>, AccountDtoValidator>()
                .AddSingleton<AbstractValidator<UserDto>, UserDtoValidator>()
                .AddSingleton<AbstractValidator<UserDependentDto>, UserDependentDtoValidator>();

            services.AddDbContext<AiofContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddLogging();
            services.AddHttpContextAccessor();

            return services;
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

    [ExcludeFromCodeCoverage]
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
        public static IEnumerable<object[]> UsersIdEmail()
        {
            return _Fake.GetFakeUsersData(
                id: true,
                email: true);
        }
        public static IEnumerable<object[]> UserIdWithDependents()
        {
            return _Fake.GetFakeUserDependentsData(
                userId: true);
        }
        public static IEnumerable<object[]> UserIdUserDependentId()
        {
            return _Fake.GetFakeUserDependentsData(
                id: true,
                userId: true);
        }
        public static IEnumerable<object[]> UserProfilesId()
        {
            return _Fake.GetFakeUserProfilesData(
                userId: true);
        }
        public static IEnumerable<object[]> UserProfilesIdEmail()
        {
            return _Fake.GetFakeUserProfilesData(
                userId: true,
                email: true);
        }

        public static IEnumerable<object[]> AssetsNameTypeNameValueUserId()
        {
            return _Fake.GetFakeAssetsData(
                name: true,
                typeName: true,
                value: true,
                userId: true);
        }
        public static IEnumerable<object[]> AssetsIdUserId()
        {
            return _Fake.GetFakeAssetsData(
                id: true,
                userId: true);
        }
        public static IEnumerable<object[]> AssetsUserId()
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

        public static IEnumerable<object[]> LiabilitiesUserId()
        {
            return _Fake.GetFakeLiabilitiesData(
                userId: true);
        }
        public static IEnumerable<object[]> LiabilitiesIdUserId()
        {
            return _Fake.GetFakeLiabilitiesData(
                id: true,
                userId: true);
        }
        public static IEnumerable<object[]> LiabilitiesTypeName()
        {
            return _Fake.GetFakeLiabilitiesData(
                typeName: true);
        }

        public static IEnumerable<object[]> GoalsUserId()
        {
            return _Fake.GetFakeGoalsData(
                userId: true);
        }
        public static IEnumerable<object[]> GoalsIdUserId()
        {
            return _Fake.GetFakeGoalsData(
                id: true,
                userId: true);
        }
        public static IEnumerable<object[]> GoalsType()
        {
            return _Fake.GetFakeGoalsData(
                type: true);
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

        public static IEnumerable<object[]> UsefulDocumentationPage()
        {
            return _Fake.GetFakeUsefulDocumentationsData(
                page: true);
        }
        public static IEnumerable<object[]> UsefulDocumentationCategory()
        {
            return _Fake.GetFakeUsefulDocumentationsData(
                category: true);
        }

        public static IEnumerable<object[]> CalculateProjectcedDates()
        {
            return _Fake.GetFakeAmountCurrentAmountMonthlyContribution();
        }
        public static IEnumerable<object[]> CalculateProjectedDatesNegatives()
        {
            return _Fake.GetFakeAmountCurrentAmountMonthlyContributionNegatives();
        }

        public static UserDto RandomUserDto()
        {
            return new Faker<UserDto>()
                .RuleFor(x => x.Assets, f => RandomAssetDtos())
                .RuleFor(x => x.Liabilities, f => RandomLiabilityDtos())
                .RuleFor(x => x.Goals, f => RandomGoalDtos())
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

        public static AssetDto RandomAssetDto()
        {
            return FakerAssetDto().Generate();
        }
        public static List<AssetDto> RandomAssetDtos(int? n = null)
        {
            return FakerAssetDto().Generate(n ?? GeneratedAmount);
        }
        private static Faker<AssetDto> FakerAssetDto()
        {
            return new Faker<AssetDto>()
                .RuleFor(x => x.Name, f => f.Random.String2(10))
                .RuleFor(x => x.TypeName, f => _Fake.GetFakeAssetTypes().First().Name)
                .RuleFor(x => x.Value, f => f.Random.Int(1000, 10000));
        }

        public static LiabilityDto RandomLiabilityDto(int? n = null)
        {
            return FakerLiabilityDto().Generate();
        }
        public static List<LiabilityDto> RandomLiabilityDtos(int? n = null)
        {
            return FakerLiabilityDto().Generate(n ?? GeneratedAmount);
        }
        private static Faker<LiabilityDto> FakerLiabilityDto()
        {
            return new Faker<LiabilityDto>()
                .RuleFor(x => x.Name, f => f.Random.String2(10))
                .RuleFor(x => x.TypeName, f => _Fake.GetFakeLiabilityTypes().First().Name)
                .RuleFor(x => x.Value, f => f.Random.Int(1000, 10000))
                .RuleFor(x => x.MonthlyPayment, f => f.Random.Decimal(50, 500))
                .RuleFor(x => x.Years, f => f.Random.Int(1, 5));
        }

        public static GoalDto RandomGoalDto()
        {
            return FakerGoalDto().Generate();
        }
        public static List<GoalDto> RandomGoalDtos(int? n = null)
        {
            return FakerGoalDto().Generate(n ?? GeneratedAmount);
        }
        private static Faker<GoalDto> FakerGoalDto()
        {
            return new Faker<GoalDto>()
                .RuleFor(x => x.Name, f => f.Random.String2(10))
                .RuleFor(x => x.Type, f => GoalType.Generic)
                .RuleFor(x => x.Amount, f => f.Random.Decimal(5000, 10000))
                .RuleFor(x => x.CurrentAmount, f => f.Random.Decimal(1000, 4000))
                .RuleFor(x => x.MonthlyContribution, f => f.Random.Decimal(100, 200))
                .RuleFor(x => x.PlannedDate, f => DateTime.Now.AddDays(7));
        }

        public static GoalTripDto RandomGoalTripDto()
        {
            return FakerGoalTripDto().Generate();
        }
        public static List<GoalTripDto> RandomGoalTripDtos(int? n = null)
        {
            return FakerGoalTripDto().Generate(n ?? GeneratedAmount);
        }
        private static Faker<GoalTripDto> FakerGoalTripDto()
        {
            return new Faker<GoalTripDto>()
                .RuleFor(x => x.Name, f => f.Random.String2(7))
                .RuleFor(x => x.Type, f => GoalType.Trip)
                .RuleFor(x => x.Amount, f => f.Random.Decimal(5000, 10000))
                .RuleFor(x => x.CurrentAmount, f => f.Random.Decimal(1000, 4000))
                .RuleFor(x => x.MonthlyContribution, f => f.Random.Decimal(100, 200))
                .RuleFor(x => x.PlannedDate, f => DateTime.Now.AddDays(60))
                .RuleFor(x => x.Destination, f => f.Random.String2(10))
                .RuleFor(x => x.TripType, f => GoalTripType.Adventure)
                .RuleFor(x => x.Duration, f => f.Random.Double(5, 14))
                .RuleFor(x => x.Travelers, f => f.Random.Int(1, 4))
                .RuleFor(x => x.Flight, f => f.Random.Decimal(0, 1000))
                .RuleFor(x => x.Hotel, f => f.Random.Decimal(0, 1000))
                .RuleFor(x => x.Car, f => f.Random.Decimal(0, 1000))
                .RuleFor(x => x.Food, f => f.Random.Decimal(0, 1000))
                .RuleFor(x => x.Activities, f => f.Random.Decimal(0, 1000))
                .RuleFor(x => x.Other, f => f.Random.Decimal(0, 1000));
        }

        public static GoalHomeDto RandomGoalHomeDto()
        {
            return FakerGoalHomeDto().Generate();
        }
        public static List<GoalHomeDto> RandomGoalHomeDtos(int? n = null)
        {
            return FakerGoalHomeDto().Generate(n ?? GeneratedAmount);
        }
        private static Faker<GoalHomeDto> FakerGoalHomeDto()
        {
            return new Faker<GoalHomeDto>()
                .RuleFor(x => x.Name, f => f.Random.String2(7))
                .RuleFor(x => x.Type, f => GoalType.BuyAHome)
                .RuleFor(x => x.Amount, f => f.Random.Decimal(30000, 35000))
                .RuleFor(x => x.CurrentAmount, f => f.Random.Decimal(20000, 35000))
                .RuleFor(x => x.MonthlyContribution, f => f.Random.Decimal(100, 200))
                .RuleFor(x => x.PlannedDate, f => DateTime.Now.AddYears(2))
                .RuleFor(x => x.HomeValue, f => f.Random.Decimal(300000, 350000))
                .RuleFor(x => x.MortgageRate, f => f.Random.Decimal(0.025M, 0.035M))
                .RuleFor(x => x.PercentDownPayment, f => f.Random.Decimal(0.1M, 0.15M))
                .RuleFor(x => x.AnnualInsurance, f => f.Random.Decimal(500, 750))
                .RuleFor(x => x.AnnualPropertyTax, f => f.Random.Decimal(0.005M, 0.01M))
                .RuleFor(x => x.RecommendedAmount, f => f.Random.Decimal(33000, 38000));
        }

        public static SubscriptionDto RandomSubscriptionDto()
        {
            return FakerSubscriptionDto().Generate();
        }
        private static Faker<SubscriptionDto> FakerSubscriptionDto()
        {
            return new Faker<SubscriptionDto>()
                .RuleFor(x => x.Name, f => f.Random.String2(10))
                .RuleFor(x => x.Description, f => f.Random.String2(50))
                .RuleFor(x => x.Amount, f => f.Random.Decimal(20, 150))
                .RuleFor(x => x.PaymentFrequencyName, f => "monthly")
                .RuleFor(x => x.PaymentLength, f => 365)
                .RuleFor(x => x.From, f => f.Random.String2(10))
                .RuleFor(x => x.Url, f => f.Internet.Url());
        }

        public static AccountDto RandomAccountDto()
        {
            return new Faker<AccountDto>()
                .RuleFor(x => x.Name, f => f.Random.String2(10))
                .RuleFor(x => x.Description, f => f.Random.String2(50))
                .RuleFor(x => x.TypeName, f => "Brokerage")
                .Generate();
        }

        public static IEnumerable<object[]> RandomUserDtos()
        {
            return new List<object[]>
            {
                new object[]
                {
                    RandomAssetDtos(),
                    RandomLiabilityDtos(),
                    RandomGoalDtos()
                }
            };
        }

        public static int GeneratedAmount = 3;
        #endregion
    }
}
