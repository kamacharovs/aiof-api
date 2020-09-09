using System;

using FluentValidation;

namespace aiof.api.data
{
    public class SubscriptionDtoValidator : AbstractValidator<SubscriptionDto>
    {
        public SubscriptionDtoValidator()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Amount)
                .GreaterThan(0);

            RuleFor(x => x.PaymentFrequencyName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.PaymentLength)
                .GreaterThan(0);

            RuleFor(x => x.UserId)
                .NotNull();


            RuleFor(x => x.From)
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.From));

            RuleFor(x => x.Url)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Url));

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}