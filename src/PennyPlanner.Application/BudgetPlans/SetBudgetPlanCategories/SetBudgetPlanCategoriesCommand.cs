using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.BudgetPlans;

namespace PennyPlanner.Application.BudgetPlans.SetBudgetPlanCategories
{
    public sealed record SetBudgetPlanCategoriesCommand(
        DateTime BudgetPlanForDate,
        IEnumerable<BudgetedTransactionCategoryValues> BudgetedTransactionCategoryValues) : ICommand<BudgetPlan>
    {
    }
}
