using System;
using System.Linq;

using FluentValidation;

namespace aiof.api.data
{
    public class GoalDtoValidator<T> : AbstractValidator<T> where T : GoalDto
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
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage);

            RuleFor(x => x.CurrentAmount)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage);
        }
    }

    public class GoalTripDtoValidator : GoalDtoValidator<GoalTripDto>
    {
        public GoalTripDtoValidator()
        {
            RuleFor(x => x.Destination)
                .NotNull()
                .NotEmpty()
                .MaximumLength(300);

            RuleFor(x => x.TripType)
                .NotNull()
                .NotEmpty()
                .WithMessage($"Invalid Type. Allowed values are {string.Join(", ", Constants.GoalTripTypes)}");

            RuleFor(x => x.Duration)
                .LessThanOrEqualTo(1000)
                .When(x => x.Duration != null);

            RuleFor(x => x.Travelers)
                .LessThanOrEqualTo(1000)
                .When(x => x.Travelers != null);

            RuleFor(x => x.Flight)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .When(x => x.Flight != null)
                .WithMessage(CommonValidator.ValueMessage);

            RuleFor(x => x.Hotel)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .When(x => x.Hotel != null)
                .WithMessage(CommonValidator.ValueMessage);

            RuleFor(x => x.Car)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .When(x => x.Car != null)
                .WithMessage(CommonValidator.ValueMessage);

            RuleFor(x => x.Food)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .When(x => x.Food != null)
                .WithMessage(CommonValidator.ValueMessage);

            RuleFor(x => x.Activities)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .When(x => x.Activities != null)
                .WithMessage(CommonValidator.ValueMessage);

            RuleFor(x => x.Other)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .When(x => x.Other != null)
                .WithMessage(CommonValidator.ValueMessage);
        }
    }

    public class GoalHomeDtoValidator : GoalDtoValidator<GoalHomeDto>
    {
        public GoalHomeDtoValidator()
        {
            RuleFor(x => x.HomeValue)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .When(x => x.HomeValue != null)
                .WithMessage(CommonValidator.ValueMessage);

            RuleFor(x => x.MortgageRate)
                .GreaterThanOrEqualTo(CommonValidator.MinimumPercentageValue)
                .LessThanOrEqualTo(CommonValidator.MaximumPercentageValue)
                .When(x => x.MortgageRate != null)
                .WithMessage(CommonValidator.PercentageMessage);

            RuleFor(x => x.PercentDownPayment)
                .GreaterThanOrEqualTo(CommonValidator.MinimumPercentageValue)
                .LessThanOrEqualTo(CommonValidator.MaximumPercentageValue)
                .When(x => x.PercentDownPayment != null)
                .WithMessage(CommonValidator.PercentageMessage);

            RuleFor(x => x.AnnualInsurance)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .When(x => x.AnnualInsurance != null)
                .WithMessage(CommonValidator.ValueMessage);

            RuleFor(x => x.AnnualPropertyTax)
                .GreaterThanOrEqualTo(CommonValidator.MinimumPercentageValue)
                .LessThanOrEqualTo(CommonValidator.MaximumPercentageValue)
                .When(x => x.AnnualPropertyTax != null)
                .WithMessage(CommonValidator.PercentageMessage);

            RuleFor(x => x.RecommendedAmount)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .When(x => x.RecommendedAmount != null)
                .WithMessage(CommonValidator.ValueMessage);
        }
    }
}