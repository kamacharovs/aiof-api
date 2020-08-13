using System;

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

            RuleFor(x => x.TypeName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Amount)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.CurrentAmount)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Contribution)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ContributionFrequency)
                .NotNull()
                .NotEmpty()
                .Must(x =>
                {
                    return x.IsValidFrequency();
                })
                .WithMessage($"Frequency is invalid. It must be '{Frequency.Daily}', " +
                    $"'{Frequency.Weekly}', " +
                    $"'{Frequency.Monthly}', or " +
                    $"'{Frequency.Yearly}', ");
        }
    }
}