using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.BudgetPlans;
using DateKit.DB;

namespace Budgetify.Infrastructure.Repositories
{
    public sealed class BudgetPlanRepository : Repository<BudgetPlan>, IBudgetPlanRepository
    {
        public BudgetPlanRepository(BudgetifyContext dbContext) : base(dbContext)
        {
        }

        public async Task<BudgetPlan> GetForPeriodAsync(DateTimeRange period)
        {
            return null;
        }

    }
}
