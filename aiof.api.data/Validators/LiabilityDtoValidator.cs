using System;

using FluentValidation;

namespace aiof.api.data
{
    public class LiabilityDtoValidator : AbstractValidator<LiabilityDto>
    {
        public LiabilityDtoValidator()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.TypeName)
                .NotNull()
                .NotEmpty();
        }
    }
}