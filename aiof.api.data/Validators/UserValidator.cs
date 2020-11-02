using System;
using System.Linq;

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
                .GreaterThanOrEqualTo(CommonValidator.MinimumValue);

            RuleFor(x => x.PaymentFrequencyName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.PaymentLength)
                .GreaterThan(CommonValidator.MinimumValueInt);


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

    public class AccountDtoValidator : AbstractValidator<AccountDto>
    {
        public AccountDtoValidator()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.TypeName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);
        }
    }

    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleForEach(x => x.Assets)
                .SetValidator(new AssetDtoValidator());
            RuleForEach(x => x.Liabilities)
                .SetValidator(new LiabilityDtoValidator());
            RuleForEach(x => x.Goals)
                .SetValidator(new GoalDtoValidator());
            RuleForEach(x => x.Subscriptions)
                .SetValidator(new SubscriptionDtoValidator());
            RuleForEach(x => x.Accounts)
                .SetValidator(new AccountDtoValidator());
        }
    }
}