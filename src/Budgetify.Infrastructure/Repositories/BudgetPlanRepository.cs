using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.BudgetPlans;
using DateKit.DB;
using Microsoft.EntityFrameworkCore;

namespace Budgetify.Infrastructure.Repositories
{
    public sealed class BudgetPlanRepository : Repository<BudgetPlan, BudgetPlanId>, IBudgetPlanRepository
    {
        public BudgetPlanRepository(BudgetifyContext dbContext) : base(dbContext)
        {
        }

    }
}
