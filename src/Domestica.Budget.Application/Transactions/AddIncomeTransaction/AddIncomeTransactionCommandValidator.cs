using FluentValidation;

namespace Domestica.Budget.Application.Transactions.AddIncomeTransaction
{
    public sealed class AddIncomeTransactionValidator : AbstractValidator<AddIncomeTransactionCommand>
    {
        public AddIncomeTransactionValidator()
        {
        }
    }
}
