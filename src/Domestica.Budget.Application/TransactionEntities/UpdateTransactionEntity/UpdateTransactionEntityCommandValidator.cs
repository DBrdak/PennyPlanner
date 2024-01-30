using FluentValidation;

namespace Domestica.Budget.Application.TransactionEntities.UpdateTransactionEntity
{
    public sealed class UpdateTransactionEntityCommandValidator : AbstractValidator<UpdateTransactionEntityCommand>
    {
        public UpdateTransactionEntityCommandValidator()
        {
            RuleFor(x => x.NewName)
                .NotEmpty()
                .WithMessage("Name of transaction entity is required")
                .MaximumLength(30)
                .WithMessage("Transaction entity name must be between 1 and 30 characters")
                .Matches("^[a-zA-Z0-9\\s]*$")
                .WithMessage("Special characters are not allowed in transaction entity name");
        }
    }
}
