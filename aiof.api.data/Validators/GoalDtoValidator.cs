using System;
using System.Collections.Generic;
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
                        .Contains(x);
                });

            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.Amount.HasValue);

            RuleFor(x => x.CurrentAmount)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.CurrentAmount.HasValue);
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
                .Must(x =>
                {
                    return Constants.GoalTripTypes
                        .ToList()
                        .Contains(x);
                })
                .WithMessage(CommonValidator.GoalTripTypesMessage);

            RuleFor(x => x.Duration)
                .LessThanOrEqualTo(1000)
                .When(x => x.Duration.HasValue);

            RuleFor(x => x.Travelers)
                .LessThanOrEqualTo(1000)
                .When(x => x.Travelers.HasValue);

            RuleFor(x => x.Flight)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.Flight.HasValue);

            RuleFor(x => x.Hotel)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.Hotel.HasValue);

            RuleFor(x => x.Car)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.Car.HasValue);

            RuleFor(x => x.Food)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.Food.HasValue);

            RuleFor(x => x.Activities)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.Activities.HasValue);

            RuleFor(x => x.Other)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.Other.HasValue);
        }
    }

    public class GoalHomeDtoValidator : GoalDtoValidator<GoalHomeDto>
    {
        public GoalHomeDtoValidator()
        {
            RuleFor(x => x.HomeValue)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.HomeValue.HasValue);

            RuleFor(x => x.MortgageRate)
                .GreaterThanOrEqualTo(CommonValidator.MinimumPercentageValue)
                .LessThanOrEqualTo(CommonValidator.MaximumPercentageValue)
                .WithMessage(CommonValidator.PercentageMessage)
                .When(x => x.MortgageRate.HasValue);

            RuleFor(x => x.PercentDownPayment)
                .GreaterThanOrEqualTo(CommonValidator.MinimumPercentageValue)
                .LessThanOrEqualTo(CommonValidator.MaximumPercentageValue)
                .WithMessage(CommonValidator.PercentageMessage)
                .When(x => x.PercentDownPayment.HasValue);

            RuleFor(x => x.AnnualInsurance)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.AnnualInsurance.HasValue);

            RuleFor(x => x.AnnualPropertyTax)
                .GreaterThanOrEqualTo(CommonValidator.MinimumPercentageValue)
                .LessThanOrEqualTo(CommonValidator.MaximumPercentageValue)
                .WithMessage(CommonValidator.PercentageMessage)
                .When(x => x.AnnualPropertyTax.HasValue);

            RuleFor(x => x.RecommendedAmount)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.RecommendedAmount.HasValue);
        }
    }

    public class GoalCarDtoValidator : GoalDtoValidator<GoalCarDto>
    {
        public GoalCarDtoValidator()
        {
            RuleFor(x => x.Year)
                .GreaterThanOrEqualTo(1900)
                .When(x => x.Year.HasValue);

            RuleFor(x => x.Make)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Make));

            RuleFor(x => x.Model)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Model));

            RuleFor(x => x.Trim)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Trim));

            RuleFor(x => x.DesiredMonthlyPayment)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.DesiredMonthlyPayment.HasValue);

            RuleFor(x => x.LoanTermMonths)
                .Must(x =>
                {
                    return CommonValidator
                        .ValidCarLoanTerms
                        .Contains((int)x);
                })
                .When(x => x.LoanTermMonths.HasValue)
                .WithMessage(CommonValidator.ValidCarLoanTermsMessage);

            RuleFor(x => x.InterestRate)
                .GreaterThanOrEqualTo(CommonValidator.MinimumPercentageValue)
                .LessThanOrEqualTo(CommonValidator.MaximumPercentageValue)
                .WithMessage(CommonValidator.PercentageMessage)
                .When(x => x.InterestRate.HasValue);
        }
    }

    public class GoalCollegeDtoValidator : GoalDtoValidator<GoalCollegeDto>
    {
        public GoalCollegeDtoValidator()
        {
            RuleFor(x => x.CollegeType)
                .NotNull()
                .NotEmpty()
                .Must(x =>
                {
                    return Constants.GoalCollegeTypes
                        .ToList()
                        .Contains(x);
                })
                .WithMessage(CommonValidator.GoalCollegeTypesMessage);

            RuleFor(x => x.CostPerYear)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage);

            RuleFor(x => x.StudentAge)
                .GreaterThanOrEqualTo(CommonValidator.MinimumYears)
                .LessThanOrEqualTo(CommonValidator.MaximumYears)
                .WithMessage(CommonValidator.YearsMessage);

            RuleFor(x => x.Years)
                .GreaterThanOrEqualTo(CommonValidator.MinimumYears)
                .LessThanOrEqualTo(CommonValidator.MaximumYears)
                .WithMessage(CommonValidator.YearsMessage);

            RuleFor(x => x.CollegeName)
                .MaximumLength(300)
                .When(x => !string.IsNullOrWhiteSpace(x.CollegeName));

            RuleFor(x => x.AnnualCostIncrease)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue)
                .LessThanOrEqualTo(CommonValidator.MaximumValue)
                .WithMessage(CommonValidator.ValueMessage)
                .When(x => x.AnnualCostIncrease.HasValue);

            RuleFor(x => x.BeginningCollegeAge)
                .GreaterThanOrEqualTo(CommonValidator.MinimumValueInt)
                .LessThanOrEqualTo(CommonValidator.MaximumValueInt)
                .WithMessage(CommonValidator.IntMessage)
                .When(x => x.AnnualCostIncrease.HasValue);
        }
    }
}