using FluentValidation;
using Money.DB;

namespace Domestica.Budget.Application.Accounts.AddAccount
{
    public sealed class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
    {
        public AddAccountCommandValidator()
        {
            RuleFor(x => x.NewAccountData.Name.Value)
                .NotEmpty()
                .WithMessage("Account name is required")
                .MaximumLength(30)
                .WithMessage("Account name must be between 1 and 30 characters")
                .Matches("^[a-zA-Z0-9\\s]*$")
                .WithMessage("Special characters are not allowed in account name");

            RuleFor(x => x.NewAccountData.InitialBalance)
                .Must(balance => Currency.All.Any(currency => currency.Code == balance.Currency.Code))
                .WithMessage("Invalid currency");
        }
    }
}
