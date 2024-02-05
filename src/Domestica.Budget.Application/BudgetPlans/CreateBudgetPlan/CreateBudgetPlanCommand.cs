using CommonAbstractions.DB.Messaging;
using DateKit.DB;
using Domestica.Budget.Domain.BudgetPlans;

namespace Domestica.Budget.Application.BudgetPlans.CreateBudgetPlan
{
    public sealed record CreateBudgetPlanCommand(DateTimeRange BudgetPeriod) : ICommand<BudgetPlan>
    {
    }
}
