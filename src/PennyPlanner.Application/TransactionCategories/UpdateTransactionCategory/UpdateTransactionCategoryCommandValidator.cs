using FluentValidation;
using PennyPlanner.Application.Settings.ValidationSettings;

namespace PennyPlanner.Application.TransactionCategories.UpdateTransactionCategory
{
    public sealed class UpdateTransactionCategoryCommandValidator : AbstractValidator<UpdateTransactionCategoryCommand>
    {
        public UpdateTransactionCategoryCommandValidator()
        {
            RuleFor(x => x.NewValue)
                .NotEmpty()
                .WithMessage("Transaction category name is required")
                .MaximumLength(TransactionCategoryValidationSettings.TransactionCategoryNameMaxLength)
                .WithMessage("Transaction category name must be between 1 and 30 characters")
                .Matches(TransactionCategoryValidationSettings.TransactionCategoryNamePattern)
                .WithMessage("Special characters are not allowed in Transaction category name");
        }
    }
}
