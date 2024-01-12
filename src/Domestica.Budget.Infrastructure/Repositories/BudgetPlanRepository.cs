using Domestica.Budget.Domain.BudgetPlans;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class BudgetPlanRepository : Repository<BudgetPlan, BudgetPlanId>, IBudgetPlanRepository
    {
        public BudgetPlanRepository(BudgetifyContext dbContext) : base(dbContext)
        {
        }

        public async Task<BudgetPlan?> GetBudgetPlanByDateAsync(DateTime dateTime, CancellationToken cancellationToken)
        {
            return await DbContext.Set<BudgetPlan>()
                .Include(bp => bp.Transactions)
                .FirstOrDefaultAsync(
                    budgetPlan => budgetPlan.BudgetPeriod.Start <= dateTime && budgetPlan.BudgetPeriod.End >= dateTime /*&& budgetPlan.UserId == userId*/,
                    cancellationToken);
        }

        public async Task<IReadOnlyCollection<BudgetPlan>> BrowseUserBudgetPlansAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<BudgetPlan>()
                .AsNoTracking()
                .Include(bp => bp.Transactions)
                .Where(bp => /*bp.UserId == userId*/ true)
                .ToListAsync(cancellationToken);
        }
    }
}
