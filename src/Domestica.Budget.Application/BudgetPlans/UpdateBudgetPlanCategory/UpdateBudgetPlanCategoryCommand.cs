using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.BudgetPlans;

namespace Domestica.Budget.Application.BudgetPlans.UpdateBudgetPlanCategory
{
    public sealed record UpdateBudgetPlanCategoryCommand(
        string BudgetPlanId,
        string CategoryId,
        UpdateBudgetPlanCategoryValues Values) : ICommand<BudgetPlan>
    {
    }
}
