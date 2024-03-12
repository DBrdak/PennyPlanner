using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.BudgetPlans;

namespace PennyPlanner.Application.BudgetPlans.UpdateBudgetPlanCategory
{
    public sealed record UpdateBudgetPlanCategoryCommand(
        string BudgetPlanId,
        string CategoryId,
        UpdateBudgetPlanCategoryValues Values) : ICommand<BudgetPlan>
    {
    }
}
