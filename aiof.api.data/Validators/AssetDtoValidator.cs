using System;

using FluentValidation;

namespace aiof.api.data
{
    public class AssetDtoValidator : AbstractValidator<AssetDto>
    {
        public AssetDtoValidator()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.TypeName)
                .NotNull()
                .NotEmpty();
        }
    }
}