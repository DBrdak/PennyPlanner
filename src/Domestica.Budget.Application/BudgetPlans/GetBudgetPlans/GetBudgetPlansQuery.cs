using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;

namespace Domestica.Budget.Application.BudgetPlans.GetBudgetPlans
{
    public sealed record GetBudgetPlansQuery : IQuery<IReadOnlyCollection<BudgetPlanDto>>
    {
    }
}
