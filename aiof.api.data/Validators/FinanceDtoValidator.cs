using System;

using FluentValidation;

namespace aiof.api.data
{
    public class FinanceDtoValidator : AbstractValidator<FinanceDto>
    {
        public FinanceDtoValidator(
            AbstractValidator<AssetDto> assetDtoValidator, 
            AbstractValidator<LiabilityDto> liabilityDtoValidator, 
            AbstractValidator<GoalDto> goalDtoValidator)
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty();

            RuleForEach(x => x.AssetDtos)
                .SetValidator(assetDtoValidator);
                
            RuleForEach(x => x.LiabilityDtos)
                .SetValidator(liabilityDtoValidator);
        }
    }
}