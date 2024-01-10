﻿using Domestica.Budget.Domain.BudgetPlans;
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
                .FirstOrDefaultAsync(
                    budgetPlan => budgetPlan.BudgetPeriod.Contains(dateTime) /*&& budgetPlan.UserId == userId*/,
                    cancellationToken);
        }
    }
}