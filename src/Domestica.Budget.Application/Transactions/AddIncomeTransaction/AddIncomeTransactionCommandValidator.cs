using Domestica.Budget.Domain.Transactions;
using FluentValidation;
using Money.DB;

namespace Domestica.Budget.Application.Transactions.AddIncomeTransaction
{
    public sealed class AddIncomeTransactionValidator : AbstractValidator<AddIncomeTransactionCommand>
    {
        public AddIncomeTransactionValidator()
        {
            RuleFor(x => x.Category)
                .Must(
                    category => IncomingTransactionCategory.All.Any(
                        incomingTransactionCategory => incomingTransactionCategory.Value == category.Value))
                .WithMessage("Invalid transaction category");

            RuleFor(x => x.TransactionAmount.Currency)
                .Must(currency => Currency.All.Any(c => c.Code == currency.Code))
                .WithMessage("Invalid currency");
        }
    }
}
