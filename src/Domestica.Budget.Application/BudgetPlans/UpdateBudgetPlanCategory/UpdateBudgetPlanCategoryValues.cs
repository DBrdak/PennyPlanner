using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domestica.Budget.Application.BudgetPlans.UpdateBudgetPlanCategory
{
    public sealed record UpdateBudgetPlanCategoryValues(Money.DB.Money? NewBudgetAmount, bool IsBudgetToReset)
    {
    }
}
