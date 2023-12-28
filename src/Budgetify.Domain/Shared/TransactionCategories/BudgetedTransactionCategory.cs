using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Money.DB;

namespace Budgetify.Domain.Shared.TransactionCategories
{
    public sealed record BudgetedTransactionCategory
    {
        public TransactionCategory Category { get; private set; }
        public Money.DB.Money BudgetedAmount { get; private set; }
        public Money.DB.Money ActualAmount { get; private set; }

        public BudgetedTransactionCategory(TransactionCategory category, Money.DB.Money budgetedAmount)
        {
            Category = category;
            BudgetedAmount = budgetedAmount;
            ActualAmount = new (0, budgetedAmount.Currency);
        }
    }
}
