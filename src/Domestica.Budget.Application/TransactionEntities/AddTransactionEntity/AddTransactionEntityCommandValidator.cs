using FluentValidation;

namespace Domestica.Budget.Application.TransactionEntities.AddTransactionEntity
{
    public sealed class AddTransactionEntityCommandValidator : AbstractValidator<AddTransactionEntityCommand>
    {
        public AddTransactionEntityCommandValidator()
        {
            RuleFor(x => x.Name.Value)
                .NotEmpty()
                .WithMessage("Name of transaction entity is required")
                .MaximumLength(30)
                .WithMessage("Transaction entity name must be between 1 and 30 characters")
                .Matches("^[a-zA-Z0-9\\s]*$")
                .WithMessage("Special characters are not allowed in transaction entity name");

            RuleFor(x => x.Type)
                .Must(type => TransactionEntityType.All.Any(transactionEntityType => transactionEntityType.Value == type.Value))
                .WithMessage("Invalid transaction entity type");
        }
    }
}
