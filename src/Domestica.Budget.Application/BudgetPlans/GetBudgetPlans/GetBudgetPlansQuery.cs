using Domestica.Budget.Application.Caching;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Application.Messaging;

namespace Domestica.Budget.Application.BudgetPlans.GetBudgetPlans
{
    public sealed record GetBudgetPlansQuery : ICachedQuery<IReadOnlyCollection<BudgetPlanDto>>
    {
        public CacheKey CacheKey => CacheKey.BudgetPlans(null);
        public TimeSpan? Expiration => null;
    }
}
