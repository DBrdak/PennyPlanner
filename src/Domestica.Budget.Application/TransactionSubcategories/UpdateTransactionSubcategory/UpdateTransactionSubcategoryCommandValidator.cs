using Domestica.Budget.Application.Settings.ValidationSettings;
using FluentValidation;

namespace Domestica.Budget.Application.TransactionSubcategories.UpdateTransactionSubcategory
{
    public sealed class UpdateTransactionSubcategoryCommandValidator : AbstractValidator<UpdateTransactionSubcategoryCommand>
    {
        public UpdateTransactionSubcategoryCommandValidator()
        {
            RuleFor(x => x.NewValue)
                .NotEmpty()
                .WithMessage("Transaction subcategory name is required")
                .MaximumLength(TransactionSubcategoryValidationSettings.TransactionSubcategoryNameMaxLength)
                .WithMessage("Transaction subcategory name must be between 1 and 30 characters")
                .Matches(TransactionSubcategoryValidationSettings.TransactionSubcategoryNamePattern)
                .WithMessage("Special characters are not allowed in transaction subcategory name");
        }
    }
}
