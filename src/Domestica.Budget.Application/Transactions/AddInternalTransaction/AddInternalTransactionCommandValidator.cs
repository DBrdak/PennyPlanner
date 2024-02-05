using FluentValidation;

namespace Domestica.Budget.Application.Transactions.AddInternalTransaction
{
    public sealed class AddInternalTransactionCommandValidator : AbstractValidator<AddInternalTransactionCommand>
    {
        public AddInternalTransactionCommandValidator()
        {
        }
    }
}
