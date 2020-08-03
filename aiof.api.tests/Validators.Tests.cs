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
        private readonly AbstractValidator<FinanceDto> _financeDtoValidator;

        public ValidatorsTests()
        {
            _assetDtoValidator = Helper.GetRequiredService<AbstractValidator<AssetDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<AssetDto>));
            _goalDtoValidator = Helper.GetRequiredService<AbstractValidator<GoalDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<GoalDto>));
            _liabilityDtoValidator = Helper.GetRequiredService<AbstractValidator<LiabilityDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<LiabilityDto>));
            _financeDtoValidator = Helper.GetRequiredService<AbstractValidator<FinanceDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<FinanceDto>));
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
                TypeName = typeName
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

        [Fact]
        public void FinanceDto_Valid()
        {
            var financeDto = new FinanceDto
            {
                UserId = 1,
                AssetDtos = new List<AssetDto>
                {
                    new AssetDto { Name = "car", TypeName = "car", Value = 1500M }
                },
                LiabilityDtos = new List<LiabilityDto>
                {
                    new LiabilityDto { Name = "car loan", TypeName = "car loan", Value = 1500M }
                }
            };

            Assert.True(_financeDtoValidator.Validate(financeDto).IsValid);
        }
    }
}