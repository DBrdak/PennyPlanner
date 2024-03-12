using CommonAbstractions.DB.Messaging;

namespace PennyPlanner.Application.BudgetPlans.GetBudgetPlan
{
    public sealed record GetBudgetPlanQuery(DateTime ValidOnDate) : IQuery<BudgetPlanModel>;
}
