using FluentValidation;
using PennyPlanner.Application.Settings.ValidationSettings;

namespace PennyPlanner.Application.TransactionSubcategories.AddTransactionSubcategory
{
    public sealed class AddTransactionSubcategoryCommandValidator : AbstractValidator<AddTransactionSubcategoryCommand>
    {
        public AddTransactionSubcategoryCommandValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Transaction subcategory name is required")
                .MaximumLength(TransactionSubcategoryValidationSettings.TransactionSubcategoryNameMaxLength)
                .WithMessage("Transaction subcategory name must be between 1 and 30 characters")
                .Matches(TransactionSubcategoryValidationSettings.TransactionSubcategoryNamePattern)
                .WithMessage("Special characters are not allowed in transaction subcategory name");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Category is required for subcategory");
        }
    }
}
