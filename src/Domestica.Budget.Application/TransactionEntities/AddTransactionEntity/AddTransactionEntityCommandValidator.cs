using FluentValidation;

namespace Domestica.Budget.Application.TransactionEntities.AddTransactionEntity
{
    public sealed class AddTransactionEntityCommandValidator : AbstractValidator<AddTransactionEntityCommand>
    {
        public AddTransactionEntityCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name of transaction entity is required")
                .MaximumLength(30)
                .WithMessage("Transaction entity name must be between 1 and 30 characters")
                .Matches("^[a-zA-Z0-9][a-zA-Z0-9\\s]*[a-zA-Z0-9]$")
                .WithMessage("Special characters are not allowed in transaction entity name");

            RuleFor(x => x.Type)
                .Must(
                    type => TransactionEntityType.All.Any(
                        transactionEntityType => string.Equals(
                            transactionEntityType.Value,
                            type,
                            StringComparison.CurrentCultureIgnoreCase)))
                .WithMessage("Invalid transaction entity type");
        }
    }
}
