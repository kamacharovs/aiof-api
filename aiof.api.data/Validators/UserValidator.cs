using System;
using System.Linq;

using FluentValidation;
using FluentValidation.Validators;

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
                .SetValidator(new GoalDtoValidator<GoalDto>());
            RuleForEach(x => x.Subscriptions)
                .SetValidator(new SubscriptionDtoValidator());
            RuleForEach(x => x.Accounts)
                .SetValidator(new AccountDtoValidator());
        }
    }

    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.StreetLine1)
                .NotEmpty()
                .MaximumLength(200)
                .When(x => x.StreetLine1 != null);

            RuleFor(x => x.StreetLine2)
                .NotEmpty()
                .MaximumLength(200)
                .When(x => x.StreetLine2 != null);

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(200)
                .When(x => x.City != null);

            RuleFor(x => x.State)
                .NotEmpty()
                .MaximumLength(2)
                .Must(x =>
                {
                    return CommonValidator.IsValidState(x);
                })
                .When(x => x.State != null);

            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .MaximumLength(5)
                .Must(x =>
                {
                    return CommonValidator.IsValidZipCode(x);
                })
                .When(x => x.ZipCode != null);
        }
    }

    public class UserDependentDtoValidator : AbstractValidator<UserDependentDto>
    {
        public UserDependentDtoValidator()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Age)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Email)
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.AmountOfSupportProvided)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.UserRelationship)
                .NotNull()
                .NotEmpty()
                .Must(x =>
                {
                    if (Constants.UserRelationships.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                        return true;

                    return false;
                })
                .WithMessage(CommonValidator.UserRelationshipsMessage);
        }
    }
}