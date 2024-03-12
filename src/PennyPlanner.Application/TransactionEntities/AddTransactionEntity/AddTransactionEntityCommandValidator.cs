using FluentValidation;
using PennyPlanner.Application.Settings.ValidationSettings;

namespace PennyPlanner.Application.TransactionEntities.AddTransactionEntity
{
    public sealed class AddTransactionEntityCommandValidator : AbstractValidator<AddTransactionEntityCommand>
    {
        public AddTransactionEntityCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name of transaction entity is required")
                .MaximumLength(TransactionEntityValidationSettings.TransactionEntityNameMaxLength)
                .WithMessage("Transaction entity name must be between 1 and 30 characters")
                .Matches(TransactionEntityValidationSettings.TransactionEntityNamePattern)
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
