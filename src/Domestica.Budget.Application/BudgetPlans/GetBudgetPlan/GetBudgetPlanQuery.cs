using CommonAbstractions.DB.Messaging;

namespace Domestica.Budget.Application.BudgetPlans.GetBudgetPlan
{
    public sealed record GetBudgetPlanQuery(DateTime ValidOnDate) : IQuery<BudgetPlanModel>;
}
