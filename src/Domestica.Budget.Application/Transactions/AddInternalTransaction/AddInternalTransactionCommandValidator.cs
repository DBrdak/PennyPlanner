using Domestica.Budget.Domain.Transactions;
using FluentValidation;
using Money.DB;

namespace Domestica.Budget.Application.Transactions.AddInternalTransaction
{
    public sealed class AddInternalTransactionCommandValidator : AbstractValidator<AddInternalTransactionCommand>
    {
        public AddInternalTransactionCommandValidator()
        {
        }
    }
}
