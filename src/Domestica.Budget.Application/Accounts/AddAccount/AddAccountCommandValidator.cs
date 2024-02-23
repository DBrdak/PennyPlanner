using FluentValidation;

namespace Domestica.Budget.Application.Accounts.AddAccount
{
    public sealed class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
    {
        public AddAccountCommandValidator()
        {
            RuleFor(x => x.NewAccountData.Name)
                .NotEmpty()
                .WithMessage("Account name is required")
                .MaximumLength(30)
                .WithMessage("Account name must be between 1 and 30 characters")
                .Matches("^[a-zA-Z0-9\\s]*$")
                .WithMessage("Special characters are not allowed in account name");

            RuleFor(x => x.NewAccountData.Type)
                .Must(type => AccountType.All.Any(accountType => accountType.Value == type))
                .WithMessage("Invalid account type");
        }
    }
}
