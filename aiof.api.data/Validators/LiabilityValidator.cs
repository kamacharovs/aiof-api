using System;

using FluentValidation;

namespace aiof.api.data
{
    public class LiabilityDtoValidator : AbstractValidator<LiabilityDto>
    {
        public LiabilityDtoValidator()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

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

    public class LiabilityTypeValidator : AbstractValidator<LiabilityType>
    {
        public LiabilityTypeValidator()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}