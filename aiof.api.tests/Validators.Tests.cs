using System;
using System.Linq;
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
        [InlineData("Name1", "typename1", GoalType.Generic)]
        [InlineData("Name2", "typename1", GoalType.Trip)]
        public void Asset_Liability_Goal_Dto_Valid(
            string name,
            string typeName,
            GoalType type)
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
                Type = type,
                Amount = 10M,
                CurrentAmount = 1M,
                MonthlyContribution = 1M
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
                Name = string.Empty,
                Type = GoalType.Generic
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
        private readonly AbstractValidator<UserDependentDto> _userDependentDtoValidator;

        public UserDtoValidatorTests()
        {
            var serviceHelper = new ServiceHelper();
            _userDtoValidator = serviceHelper.GetRequiredService<AbstractValidator<UserDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<UserDto>));
            _userDependentDtoValidator = serviceHelper.GetRequiredService<AbstractValidator<UserDependentDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<UserDependentDto>));
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

        [Theory]
        [InlineData("Firstname1", "Lastname1", 12)]
        [InlineData("Firstname2", "Lastname2", 13)]
        [InlineData("Firstname3", "Lastname3", 14)]
        public void UserDependentDto_Validation_IsSuccessful(
            string firstName, 
            string lastName,
            int age)
        {
            var dto = new UserDependentDto
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Email = $"{firstName}.{lastName}@aiof.com",
                AmountOfSupportProvided = 15M,
                UserRelationship = UserRelationships.Child.ToString()
            };

            Assert.True(_userDependentDtoValidator.Validate(dto).IsValid);
        }

        [Theory]
        [InlineData("", "Lastname")]
        [InlineData(null, "Lastname")]
        [InlineData("Firstname", "")]
        [InlineData("Firstname", null)]
        [InlineData("A", "Lastname")]
        [InlineData("Firstname", "A")]
        public void UserDependentDto_Validation_FirstLastName_IsNotValid(
            string firstName,
            string lastName)
        {
            if (firstName?.Length == 1)
                firstName = new string('\t', 201);
            else if (lastName?.Length == 1)
                lastName = new string('\t', 201);

            var dto = new UserDependentDto
            {
                FirstName = firstName,
                LastName = lastName,
                Age = 12,
                Email = $"{firstName}.{lastName}@aiof.com",
                AmountOfSupportProvided = 15M,
                UserRelationship = UserRelationships.Child.ToString()
            };

            Assert.False(_userDependentDtoValidator.Validate(dto).IsValid);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void UserDependentDto_Validation_Age_IsNotValid(int age)
        {
            var dto = new UserDependentDto
            {
                FirstName = "Firstname",
                LastName = "Lastname",
                Age = age,
                Email = "firstName.lastName@aiof.com",
                AmountOfSupportProvided = 15M,
                UserRelationship = UserRelationships.Child.ToString()
            };

            Assert.False(_userDependentDtoValidator.Validate(dto).IsValid);
        }

        [Theory]
        [InlineData("email")]
        [InlineData("definitelynotemail")]
        public void UserDependentDto_Validation_Email_IsNotValid(string email)
        {
            var dto = new UserDependentDto
            {
                FirstName = "Firstname",
                LastName = "Lastname",
                Age = 12,
                Email = email,
                AmountOfSupportProvided = 15M,
                UserRelationship = UserRelationships.Child.ToString()
            };

            Assert.False(_userDependentDtoValidator.Validate(dto).IsValid);
        }

        [Theory]
        [InlineData(5000.0)]
        [InlineData(1.0)]
        [InlineData(0.0)]
        public void UserDependentDto_Validation_AmountOfSupportProvided_IsNotValid(decimal amountOfSupportProvided)
        {
            var dto = new UserDependentDto
            {
                FirstName = "Firstname",
                LastName = "Lastname",
                Age = 12,
                Email = "firstname.lastname@aiof.com",
                AmountOfSupportProvided = decimal.Negate(amountOfSupportProvided),
                UserRelationship = UserRelationships.Child.ToString()
            };

            Assert.False(_userDependentDtoValidator.Validate(dto).IsValid);
        }

        [Theory]
        [InlineData("definitelyincorrect")]
        [InlineData("stepchild")]
        [InlineData(null)]
        [InlineData("")]
        public void UserDependentDto_Validation_UserRelationship_IsNotValid(string userRelationship)
        {
            var dto = new UserDependentDto
            {
                FirstName = "Firstname",
                LastName = "Lastname",
                Age = 12,
                Email = "firstname.lastname@aiof.com",
                AmountOfSupportProvided = 15M,
                UserRelationship = userRelationship
            };

            var validation = _userDependentDtoValidator.Validate(dto);

            Assert.False(validation.IsValid);

            if (!string.IsNullOrWhiteSpace(userRelationship))
                Assert.NotNull(validation.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("User relationship must be")));
        }
    }
}