using Domestica.Budget.Application.TransactionCategories.AddTransactionCategory;
using FluentValidation;

namespace Domestica.Budget.Application.BudgetPlans.SetBudgetPlanCategories
{
    public sealed class SetBudgetPlanCategoriesCommandValidator : AbstractValidator<SetBudgetPlanCategoriesCommand>
    {
        public SetBudgetPlanCategoriesCommandValidator()
        {
            RuleForEach(x => x.BudgetedTransactionCategoryValues)
                .Must(values => values.BudgetedAmount > 0)
                .WithMessage("Budgeted amount must be greater than 0")
                .Must(
                    values => TransactionCategoryType.All.Any(
                        tcType => string.Equals(
                            tcType.Value,
                            values.CategoryType,
                            StringComparison.CurrentCultureIgnoreCase)))
                .WithMessage("Invalid Transaction category type");

            RuleFor(x => x.BudgetPlanForDate.Month)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Month)
                .WithMessage("Cannot create budget plan for past month");
        }
    }
}
