using System;
using System.Linq;

using FluentValidation;

namespace aiof.api.data
{
    public class GoalDtoValidator : AbstractValidator<GoalDto>
    {
        public GoalDtoValidator()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Type)
                .Must(x =>
                {
                    return Constants.GoalTypes
                        .ToList()
                        .Contains(x.ToString());
                });

            RuleFor(x => x.Amount)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThan(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage);

            RuleFor(x => x.CurrentAmount)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThan(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage);
        }
    }
}