using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateKit.DB;

namespace Budgetify.Domain.BudgetPlans
{
    public interface IBudgetPlanRepository
    {
        Task<BudgetPlan> GetForPeriodAsync(DateTimeRange period);
    }
}
