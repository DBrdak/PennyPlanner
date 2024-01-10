using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.BudgetPlans;

namespace Domestica.Budget.Application.BudgetPlans.SetBudgetPlanCategories
{
    public sealed record SetBudgetPlanCategoriesCommand(BudgetPlanId BudgetPlanId, IEnumerable<BudgetedTransactionCategoryValues> BudgetedTransactionCategoryValues) : ICommand
    {
    }
}
