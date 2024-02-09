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
                .Must(values => TransactionCategoryType.All.Any(tcType => tcType.Value == values.CategoryType))
                .WithMessage("Invalid Transaction category type");
            ;
        }
    }
}
