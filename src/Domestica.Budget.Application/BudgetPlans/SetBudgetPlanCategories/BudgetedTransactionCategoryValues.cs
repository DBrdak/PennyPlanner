using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.BudgetPlans.SetBudgetPlanCategories
{
    public sealed record BudgetedTransactionCategoryValues(TransactionCategory Category, Money.DB.Money BudgetedAmount)
    {
    }
}
