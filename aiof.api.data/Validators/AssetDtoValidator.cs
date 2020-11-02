using System;

using FluentValidation;

namespace aiof.api.data
{
    public class AssetDtoValidator : AbstractValidator<AssetDto>
    {
        public AssetDtoValidator()
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
                .WithMessage(CommonValidator.ValueMessage);
        }
    }
}