using Domestica.Budget.Domain.Transactions;
using FluentValidation;
using Money.DB;

namespace Domestica.Budget.Application.Transactions.AddOutcomeTransaction
{
    public sealed class AddOutcomeTransactionValidator : AbstractValidator<AddOutcomeTransactionCommand>
    {
        public AddOutcomeTransactionValidator()
        {
            RuleFor(x => x.Category)
                .Must(
                    category =>  OutgoingTransactionCategory.All.Any(
                        incomingTransactionCategory => incomingTransactionCategory.Value == category))
                .WithMessage("Invalid transaction category");

            RuleFor(x => x.TransactionAmount.Currency)
                .Must(currency => Currency.All.Any(c => c.Code == currency))
                .WithMessage("Invalid currency");
        }
    }
}
