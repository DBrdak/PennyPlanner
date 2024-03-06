using Domestica.Budget.Application.Settings.ValidationSettings;
using FluentValidation;

namespace Domestica.Budget.Application.TransactionCategories.UpdateTransactionCategory
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
