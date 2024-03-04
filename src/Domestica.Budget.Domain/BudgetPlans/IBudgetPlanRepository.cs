using System.Linq.Expressions;

namespace Domestica.Budget.Domain.BudgetPlans
{
    public interface IBudgetPlanRepository
    {
        Task<BudgetPlan?> GetByIdAsync(BudgetPlanId id, CancellationToken cancellationToken = default, bool asNoTracking = false);
        Task<BudgetPlan?> GetBudgetPlanByDateAsync(DateTime dateTime, /*UserId userId,*/ CancellationToken cancellationToken, bool asNoTracking = false);
        Task<IReadOnlyCollection<BudgetPlan>> BrowseUserBudgetPlansAsync(/*UserId userId,*/CancellationToken cancellationToken = default);
        Task AddAsync(BudgetPlan budgetPlan, CancellationToken cancellationToken);

        Task<BudgetPlan?> GetByIdIncludeAsync<TProperty>(
            BudgetPlanId id,
            Expression<Func<BudgetPlan, TProperty>> includeExpression,
            CancellationToken cancellationToken = default);

        Task<BudgetPlan?> GetBudgetPlanByIdAsync(BudgetPlanId budgetPlanId, CancellationToken cancellationToken);

        void Remove(BudgetPlan entity);
    }
}
