using Domestica.Budget.Domain.Transactions;
using FluentValidation;
using Money.DB;

namespace Domestica.Budget.Application.Transactions.AddInternalTransaction
{
    public sealed class AddInternalTransactionCommandValidator : AbstractValidator<AddInternalTransactionCommand>
    {
        public AddInternalTransactionCommandValidator()
        {
            RuleFor(x => x.TransactionAmount.Currency)
                .Must(currency => Currency.All.Any(c => c.Code == currency))
                .WithMessage("Invalid currency");
        }
    }
}
