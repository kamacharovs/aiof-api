using System;

using FluentValidation;

namespace aiof.api.data
{
    public class GoalDtoValidator : AbstractValidator<GoalDto>
    {
        public GoalDtoValidator()
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