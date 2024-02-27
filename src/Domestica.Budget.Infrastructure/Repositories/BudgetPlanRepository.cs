using Domestica.Budget.Domain.BudgetPlans;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class BudgetPlanRepository : Repository<BudgetPlan, BudgetPlanId>, IBudgetPlanRepository
    {
        public BudgetPlanRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<BudgetPlan?> GetBudgetPlanByDateAsync(DateTime dateTime, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            IQueryable<BudgetPlan> query = DbContext.Set<BudgetPlan>()
                .Include(bp => bp.Transactions);

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(
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
