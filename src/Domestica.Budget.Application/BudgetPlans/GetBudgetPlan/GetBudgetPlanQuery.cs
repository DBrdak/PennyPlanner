using Domestica.Budget.Application.Messaging;
using Domestica.Budget.Application.Messaging.Caching;

namespace Domestica.Budget.Application.BudgetPlans.GetBudgetPlan
{
    public sealed record GetBudgetPlanQuery(DateTime ValidOnDate) : ICachedQuery<BudgetPlanModel>
    {
        public CacheKey CacheKey => CacheKey.BudgetPlans(null);
        public TimeSpan? Expiration => null;
    }
}
