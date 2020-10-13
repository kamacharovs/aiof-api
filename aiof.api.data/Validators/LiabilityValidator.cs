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
         
            RuleFor(x => x.Value)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThan(CommonValidator.MaximumValue)
                .When(x => x.Value != null)
                .WithMessage(CommonValidator.ValueMessage);
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

            RuleFor(x => x.PublicKey)
                .NotNull()
                .Must(x => x != Guid.Empty);
        }
    }
}