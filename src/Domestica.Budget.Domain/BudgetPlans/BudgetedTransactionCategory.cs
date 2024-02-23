using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.Transactions;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.BudgetPlans
{
    public sealed record BudgetedTransactionCategory
    {
        public TransactionCategoryId CategoryId { get; private set; }
        public TransactionCategory Category { get; private set; }
        public Money.DB.Money BudgetedAmount { get; private set; }
        public Money.DB.Money ActualAmount { get; private set; }

        private BudgetedTransactionCategory()
        { }

        public BudgetedTransactionCategory(TransactionCategory category, Money.DB.Money budgetedAmount)
        {
            CategoryId = category.Id;
            Category = category;
            BudgetedAmount = budgetedAmount;
            ActualAmount = new(0, budgetedAmount.Currency);
        }

        internal void AddTransaction(Transaction transaction)
        {
            ActualAmount += new Money.DB.Money(Math.Abs(transaction.TransactionAmount.Amount), transaction.TransactionAmount.Currency);
        }

        internal void UpdateBudgetedAmount(Money.DB.Money budgetedAmount)
        {
            BudgetedAmount = budgetedAmount;
        }
    }
}
