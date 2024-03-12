using FluentValidation;
using PennyPlanner.Application.Settings.ValidationSettings;

namespace PennyPlanner.Application.Transactions.AddOutcomeTransaction
{
    public sealed class AddOutcomeTransactionValidator : AbstractValidator<AddOutcomeTransactionCommand>
    {
        public AddOutcomeTransactionValidator()
        {
            RuleFor(x => x.CategoryValue)
                .NotEmpty()
                .WithMessage("Category is required")
                .MaximumLength(TransactionCategoryValidationSettings.TransactionCategoryNameMaxLength)
                .WithMessage("Transaction category name must be between 1 and 30 characters")
                .Matches(TransactionCategoryValidationSettings.TransactionCategoryNamePattern)
                .WithMessage("Special characters are not allowed in Transaction category name");

            RuleFor(x => x.TransactionAmount)
                .GreaterThan(0)
                .WithMessage("Transaction amount must be positive")
                .LessThan(decimal.MaxValue)
                .WithMessage("Transaction amount is too large");

            RuleFor(x => x.SourceAccountId)
                .Must(sourceAccountId => Guid.TryParse(sourceAccountId, out _))
                .WithMessage("Wrong account ID format");

            RuleFor(x => x.RecipientName)
                .NotEmpty()
                .WithMessage("Recipient is required")
                .MaximumLength(TransactionEntityValidationSettings.TransactionEntityNameMaxLength)
                .WithMessage("Transaction entity name must be between 1 and 30 characters")
                .Matches(TransactionEntityValidationSettings.TransactionEntityNamePattern)
                .WithMessage("Special characters are not allowed in transaction entity name");

            RuleFor(x => x.TransactionDateTime)
                .NotEmpty()
                .WithMessage("Date of transaction is required")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Select past date");
        }
    }
}
