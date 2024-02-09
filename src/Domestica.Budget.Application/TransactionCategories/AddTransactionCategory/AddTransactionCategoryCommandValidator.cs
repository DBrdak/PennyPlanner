using FluentValidation;

namespace Domestica.Budget.Application.TransactionCategories.AddTransactionCategory
{
    public sealed class AddTransactionCategoryCommandValidator : AbstractValidator<AddTransactionCategoryCommand>
    {
        public AddTransactionCategoryCommandValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Transaction category name is required")
                .MaximumLength(30)
                .WithMessage("Transaction category name must be between 1 and 30 characters")
                .Matches("^[a-zA-Z0-9\\s]*$")
                .WithMessage("Special characters are not allowed in Transaction category name");

            RuleFor(x => x.Type)
                .Must(type => TransactionCategoryType.All.Any(tcType => tcType.Value == type))
                .WithMessage("Invalid Transaction category type");
        }
    }
}
