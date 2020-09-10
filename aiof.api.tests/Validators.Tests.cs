using System;
using System.Collections.Generic;

using FluentValidation;

using Xunit;

using aiof.api.data;

namespace aiof.api.tests
{
    [Trait(Helper.Category, Helper.UnitTest)]
    public class ValidatorsTests
    {
        private readonly AbstractValidator<AssetDto> _assetDtoValidator;
        private readonly AbstractValidator<GoalDto> _goalDtoValidator;
        private readonly AbstractValidator<LiabilityDto> _liabilityDtoValidator;
        private readonly AbstractValidator<LiabilityType> _liabilityTypeValidator;
        private readonly AbstractValidator<SubscriptionDto> _subscriptionDtoValidator;

        public ValidatorsTests()
        {
            _assetDtoValidator = Helper.GetRequiredService<AbstractValidator<AssetDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<AssetDto>));
            _goalDtoValidator = Helper.GetRequiredService<AbstractValidator<GoalDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<GoalDto>));
            _liabilityDtoValidator = Helper.GetRequiredService<AbstractValidator<LiabilityDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<LiabilityDto>));
            _liabilityTypeValidator = Helper.GetRequiredService<AbstractValidator<LiabilityType>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<LiabilityType>));
            _subscriptionDtoValidator = Helper.GetRequiredService<AbstractValidator<SubscriptionDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<SubscriptionDto>));
        }

        [Theory]
        [InlineData("Name1", "TypeName1")]
        [InlineData("Name2", "TypeName2")]
        public void Asset_Liability_Goal_Dto_Valid(
            string name, 
            string typeName)
        {
            var assetDto = new AssetDto
            {
                Name = name,
                TypeName = typeName
            };
            Assert.True(_assetDtoValidator.Validate(assetDto).IsValid);

            var liabilityDto = new LiabilityDto
            {
                Name = name,
                TypeName = typeName
            };
            Assert.True(_liabilityDtoValidator.Validate(liabilityDto).IsValid);

            var goalDto = new GoalDto
            {
                Name = name,
                TypeName = typeName,
                Amount = 10M,
                CurrentAmount = 1M,
                Contribution = 1M,
                ContributionFrequencyName = "monthly"
            };
            Assert.True(_goalDtoValidator.Validate(goalDto).IsValid);
        }

        [Theory]
        [InlineData("Name1", null)]
        [InlineData(null, "TypeName2")]
        [InlineData("Name1", "")]
        [InlineData("", "TypeName2")]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void Asset_Liability_Goal_Dto_Name_TypeName_Invalid(
            string name, 
            string typeName)
        {
            var assetDto = new AssetDto
            {
                Name = name,
                TypeName = typeName
            };
            Assert.False(_assetDtoValidator.Validate(assetDto).IsValid);

            var liabilityDto = new LiabilityDto
            {
                Name = name,
                TypeName = typeName
            };
            Assert.False(_liabilityDtoValidator.Validate(liabilityDto).IsValid);

            var goalDto = new GoalDto
            {
                Name = name,
                TypeName = typeName
            };
            Assert.False(_goalDtoValidator.Validate(goalDto).IsValid);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("definitely doesn't exist yet")]
        [InlineData("something financial")]
        public void LiabilityType_Validate_IsSuccessful(string name)
        {
            var liabilityType = new LiabilityType { Name = name };

            Assert.True(_liabilityTypeValidator.Validate(liabilityType).IsValid);
        }


        [Theory]
        [InlineData("Netflix", 10, "monthly", 12, 1)]
        [InlineData("Hulu", 8.99, "monthly", 24, 1)]
        [InlineData("Subscription", 99.88, "monthly", 36, 1)]
        public void Subscription_Dto_Validate_IsSuccessful(
            string name,
            decimal amount,
            string paymentFrequencyName,
            int paymentLength,
            int userId)
        {
            var subscriptionDto = new SubscriptionDto
            {
                Name = name,
                Amount = amount,
                PaymentFrequencyName = paymentFrequencyName,
                PaymentLength = paymentLength,
                UserId = userId
            };

            Assert.True(_subscriptionDtoValidator.Validate(subscriptionDto).IsValid);
        }
    }

    [Trait(Helper.Category, Helper.UnitTest)]
    public class UserDtoValidatorTests
    {
        private readonly AbstractValidator<UserDto2> _userDtoValidator;

        public UserDtoValidatorTests()
        {
            _userDtoValidator = Helper.GetRequiredService<AbstractValidator<UserDto2>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<UserDto2>));
        }

        [Fact]
        public void UserDto2()
        {
            var assets = new List<AssetDto>
            {
                new AssetDto
                {
                    Name = "asset-1",
                    TypeName = "car",
                    Value = 15000
                }
            };

            var userDto = new UserDto2 { Assets = assets };

            Assert.True(_userDtoValidator.Validate(userDto).IsValid);
        }
    }
}