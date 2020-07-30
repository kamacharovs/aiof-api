using System;

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

        public ValidatorsTests()
        {
            _assetDtoValidator = Helper.GetRequiredService<AbstractValidator<AssetDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<AssetDto>));
            _goalDtoValidator = Helper.GetRequiredService<AbstractValidator<GoalDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<GoalDto>));
            _liabilityDtoValidator = Helper.GetRequiredService<AbstractValidator<LiabilityDto>>() ?? throw new ArgumentNullException(nameof(AbstractValidator<LiabilityDto>));
        }

        [Theory]
        [InlineData("Name1", "TypeName1")]
        [InlineData("Name2", "TypeName2")]
        public void AssetDto_Valid(
            string name, 
            string typeName)
        {
            var assetDto = new AssetDto
            {
                Name = name,
                TypeName = typeName
            };

            Assert.True(_assetDtoValidator.Validate(assetDto).IsValid);
        }

        [Theory]
        [InlineData("Name1", null)]
        [InlineData(null, "TypeName2")]
        [InlineData("Name1", "")]
        [InlineData("", "TypeName2")]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void AssetDto_Name_TypeName_Invalid(
            string name, 
            string typeName)
        {
            var assetDto = new AssetDto
            {
                Name = name,
                TypeName = typeName
            };

            Assert.False(_assetDtoValidator.Validate(assetDto).IsValid);
        }
    }
}