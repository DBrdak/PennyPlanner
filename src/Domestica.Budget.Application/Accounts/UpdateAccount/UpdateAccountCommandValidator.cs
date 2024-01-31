using FluentValidation;
using Money.DB;

namespace Domestica.Budget.Application.Accounts.UpdateAccount
{
    public sealed class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(x => x.AccountUpdateData.Name)
                .NotEmpty()
                .WithMessage("Account name is required")
                .MaximumLength(30)
                .WithMessage("Account name must be between 1 and 30 characters")
                .Matches("^[a-zA-Z0-9\\s]*$")
                .WithMessage("Special characters are not allowed in account name");
        }
    }
}
