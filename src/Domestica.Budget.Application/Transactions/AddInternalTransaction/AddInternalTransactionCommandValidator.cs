using FluentValidation;

namespace Domestica.Budget.Application.Transactions.AddInternalTransaction
{
    public sealed class AddInternalTransactionCommandValidator : AbstractValidator<AddInternalTransactionCommand>
    {
        public AddInternalTransactionCommandValidator()
        {
            RuleFor(x => x.TransactionAmount)
                .GreaterThan(0)
                .WithMessage("Transaction amount must be positive")
                .LessThan(decimal.MaxValue)
                .WithMessage("Transaction amount is too large");

            RuleFor(x => x.FromAccountId)
                .Must(fromAccountId => Guid.TryParse(fromAccountId, out _))
                .WithMessage("Wrong source account ID format");

            RuleFor(x => x.ToAccountId)
                .Must(toAccountId => Guid.TryParse(toAccountId, out _))
                .WithMessage("Wrong destination account ID format");

            RuleFor(x => x)
                .Must(x => x.FromAccountId != x.ToAccountId)
                .WithMessage("Account must differ");
            
            RuleFor(x => x.TransactionDateTime)
                .NotEmpty()
                .WithMessage("Date of transaction is required")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Select past date");
        }
    }
}
