using FluentValidation;
using PennyPlanner.Application.Settings.ValidationSettings;

namespace PennyPlanner.Application.TransactionCategories.AddTransactionCategory
{
    public sealed class AddTransactionCategoryCommandValidator : AbstractValidator<AddTransactionCategoryCommand>
    {
        public AddTransactionCategoryCommandValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Transaction category name is required")
                .MaximumLength(TransactionCategoryValidationSettings.TransactionCategoryNameMaxLength)
                .WithMessage("Transaction category name must be between 1 and 30 characters")
                .Matches(TransactionCategoryValidationSettings.TransactionCategoryNamePattern)
                .WithMessage("Special characters are not allowed in Transaction category name");

            RuleFor(x => x.Type)
                .Must(type => TransactionCategoryType.All.Any(
                        tcType => string.Equals(
                            tcType.Value,
                            type,
                            StringComparison.CurrentCultureIgnoreCase)))
                .WithMessage("Invalid Transaction category type");
        }
    }
}
