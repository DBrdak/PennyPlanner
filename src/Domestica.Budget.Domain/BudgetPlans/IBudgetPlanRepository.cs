using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.BudgetPlans
{
    public interface IBudgetPlanRepository
    {
        Task<BudgetPlan?> GetByIdAsync(BudgetPlanId id, CancellationToken cancellationToken = default);
        Task<BudgetPlan?> GetBudgetPlanByDateAsync(DateTime dateTime, /*UserId userId,*/ CancellationToken cancellationToken);
        Task AddAsync(BudgetPlan budgetPlan, CancellationToken cancellationToken);
    }
}
