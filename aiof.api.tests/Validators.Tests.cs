using System;
using System.Collections.Generic;

using FluentValidation;

using Xunit;

using aiof.api.data;

namespace aiof.api.tests
{
    [Trait(Helper.Category, Helper.UnitTest)]
    public class CommonValidatorTests
    {
        [Theory]
        [InlineData("123-456-7890")]
        [InlineData("(123) 456-7890")]
        [InlineData("123 456 7890")]
        [InlineData("123.456.7890")]
        [InlineData("+91 (123) 456-7890")]
        public void PhoneNumber_IsValid(string phoneNumber)
        {
            Assert.True(CommonValidator.IsValidPhoneNumber(phoneNumber));
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("09-12-123-456-7890")]
        [InlineData("(12343) 456-7890")]
        [InlineData("123 456 4295 7890")]
        [InlineData("123.456.7890.2345")]
        [InlineData("+91 (123) 456-7890-234")]
        public void PhoneNumber_IsInvalid(string phoneNumber)
        {
            Assert.False(CommonValidator.IsValidPhoneNumber(phoneNumber));
        }
    }

    [Trait(Helper.Category, Helper.UnitTest)]
    public class ValidatorsTests
    {
        private readonly AbstractValidator<AssetDto> _assetDtoValidator;
        private readonly AbstractValidator<GoalDto> _goalDtoValidator;
        private readonly AbstractValidator<LiabilityDto> _liabilityDtoValidator;
        private readonly AbstractValidator<LiabilityType> _liabilityTypeValidator;
        private readonly AbstractValidator<SubscriptionDto> _subscriptionDtoValidator;
        private readonly AbstractValidator<AccountDto> _accountDtoValidator;

        public ValidatorsTests()
        {
            var serviceHelper = new ServiceHelper();
            _assetDtoValidator = serviceHelper.GetRequiredService<AbstractValidator<AssetDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<AssetDto>));
            _goalDtoValidator = serviceHelper.GetRequiredService<AbstractValidator<GoalDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<GoalDto>));
            _liabilityDtoValidator = serviceHelper.GetRequiredService<AbstractValidator<LiabilityDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<LiabilityDto>));
            _liabilityTypeValidator = serviceHelper.GetRequiredService<AbstractValidator<LiabilityType>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<LiabilityType>));
            _subscriptionDtoValidator = serviceHelper.GetRequiredService<AbstractValidator<SubscriptionDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<SubscriptionDto>));
            _accountDtoValidator = serviceHelper.GetRequiredService<AbstractValidator<AccountDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<AccountDto>));
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
                TypeName = typeName,
                Value = 15000M
            };
            Assert.False(_assetDtoValidator.Validate(assetDto).IsValid);

            var liabilityDto = new LiabilityDto
            {
                Name = name,
                TypeName = typeName,
                Value = 15000M
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
        [InlineData("Netflix", 10, "monthly", 12)]
        [InlineData("Hulu", 8.99, "monthly", 24)]
        [InlineData("Subscription", 99.88, "monthly", 36)]
        public void Subscription_Dto_Validate_IsSuccessful(
            string name,
            decimal amount,
            string paymentFrequencyName,
            int paymentLength)
        {
            var subscriptionDto = new SubscriptionDto
            {
                Name = name,
                Amount = amount,
                PaymentFrequencyName = paymentFrequencyName,
                PaymentLength = paymentLength
            };

            Assert.True(_subscriptionDtoValidator.Validate(subscriptionDto).IsValid);
        }

        [Fact]
        public void AccountDto_Validate_RandomDto_IsSuccessful()
        {
            var dto = Helper.RandomAccountDto();

            Assert.True(_accountDtoValidator.Validate(dto).IsValid);
        }
    }

    [Trait(Helper.Category, Helper.UnitTest)]
    public class UserDtoValidatorTests
    {
        private readonly AbstractValidator<UserDto> _userDtoValidator;

        public UserDtoValidatorTests()
        {
            _userDtoValidator = new ServiceHelper().GetRequiredService<AbstractValidator<UserDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<UserDto>));
        }

        [Theory]
        [MemberData(nameof(Helper.RandomUserDtos), MemberType = typeof(Helper))]
        public void UserDto_Validation_IsSuccessful(
            List<AssetDto> assets,
            List<LiabilityDto> liabilities,
            List<GoalDto> goals)
        {
            var userDto = new UserDto
            {
                Assets = assets,
                Liabilities = liabilities,
                Goals = goals
            };

            Assert.True(_userDtoValidator.Validate(userDto).IsValid);
        }

        [Fact]
        public void UserDto_Validation_AssetsOnly_IsSuccessful()
        {
            var assetDtos = Helper.RandomAssetDtos();
            var userDto = new UserDto { Assets = assetDtos };

            Assert.True(_userDtoValidator.Validate(userDto).IsValid);
        }

        [Fact]
        public void UserDto_Validation_LiabilitiesOnly_IsSuccessful()
        {
            var liabilityDtos = Helper.RandomLiabilityDtos();
            var userDto = new UserDto { Liabilities = liabilityDtos };

            Assert.True(_userDtoValidator.Validate(userDto).IsValid);
        }

        [Fact]
        public void UserDto_Validation_GoalsOnly_IsSuccessful()
        {
            var goalDtos = Helper.RandomGoalDtos();
            var userDto = new UserDto { Goals = goalDtos };

            Assert.True(_userDtoValidator.Validate(userDto).IsValid);
        }
    }
}