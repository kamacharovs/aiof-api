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
        }
    }
}