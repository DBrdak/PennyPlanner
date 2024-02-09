using FluentValidation;

namespace Domestica.Budget.Application.BudgetPlans.UpdateBudgetPlanCategory
{
    public sealed class UpdateBudgetPlanCategoryCommandValidator : AbstractValidator<UpdateBudgetPlanCategoryCommand>
    {
        public UpdateBudgetPlanCategoryCommandValidator()
        {
            RuleFor(x => x)
                .Must(x => x.Values.NewBudgetAmount is not null || x.Values.IsBudgetToReset)
                .WithMessage("Specify whether you want to reset category or update budgeted amount");

            RuleFor(x => x.Values.NewBudgetAmount)
                .Must(amount => !amount.HasValue || amount.Value > 0)
                .WithMessage("Budgeted amount must be greater than 0");
        }
    }
}
