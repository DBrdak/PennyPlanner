using Domestica.Budget.Domain.BudgetPlans;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class BudgetPlanRepository : Repository<BudgetPlan, BudgetPlanId>, IBudgetPlanRepository
    {
        public BudgetPlanRepository(BudgetifyContext dbContext) : base(dbContext)
        {
        }

    }
}
