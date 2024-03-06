using System.Linq.Expressions;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Domain.BudgetPlans;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class BudgetPlanRepository : Repository<BudgetPlan, BudgetPlanId>, IBudgetPlanRepository
    {
        public BudgetPlanRepository(ApplicationDbContext dbContext, IUserContext userContext) : base(dbContext, userContext)
        {
        }

        public async Task<BudgetPlan?> GetByIdAsync(BudgetPlanId id, CancellationToken cancellationToken = default, bool asNoTracking = false)
        {
            return null;
        }

        public async Task<BudgetPlan?> GetBudgetPlanByDateAsync(DateTime dateTime, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            IQueryable<BudgetPlan> query = DbContext.Set<BudgetPlan>()
                .Include(bp => bp.Transactions)
                .ThenInclude(t => t.Category)
                .Include(bp => bp.Transactions)
                .ThenInclude(t => t.Subcategory);

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
                .Where(bp => bp.UserId == UserId)
                .ToListAsync(cancellationToken);
        }

        public async Task<BudgetPlan?> GetByIdIncludeAsync<TProperty>(
            BudgetPlanId id,
            Expression<Func<BudgetPlan, TProperty>> includeExpression,
            CancellationToken cancellationToken = default)
        {
            var query = DbContext.Set<BudgetPlan>();

            return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<BudgetPlan?> GetBudgetPlanByIdAsync(BudgetPlanId budgetPlanId, CancellationToken cancellationToken)
        {
            return await DbContext.Set<BudgetPlan>()
                .Include(bp => bp.BudgetedTransactionCategories)
                .ThenInclude(btc => btc.Category)
                .Include(bp => bp.Transactions)
                .ThenInclude(t => t.Category)
                .FirstOrDefaultAsync(
                    budgetPlan => budgetPlan.Id == budgetPlanId,
                    cancellationToken);
        }
    }
}
