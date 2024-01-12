using Domestica.Budget.Domain.Transactions;
using Exceptions.DB;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.BudgetPlans
{
    public sealed record BudgetedTransactionCategory
    {
        public TransactionCategory Category { get; private set; }
        public Money.DB.Money BudgetedAmount { get; private set; }
        public Money.DB.Money ActualAmount { get; private set; }

        private BudgetedTransactionCategory()
        { }

        public BudgetedTransactionCategory(TransactionCategory category, Money.DB.Money budgetedAmount)
        {
            Category = category;
            BudgetedAmount = budgetedAmount;
            ActualAmount = new(0, budgetedAmount.Currency);
        }

        internal void AddTransaction(Transaction transaction)
        {
            ActualAmount += new Money.DB.Money(Math.Abs(transaction.TransactionAmount.Amount), transaction.TransactionAmount.Currency);
        }
    }
}
