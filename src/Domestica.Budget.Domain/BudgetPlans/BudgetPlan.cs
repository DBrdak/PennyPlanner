using CommonAbstractions.DB.Entities;
using DateKit.DB;
using Domestica.Budget.Domain.Transactions;
using Exceptions.DB;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.BudgetPlans
{
    public sealed class BudgetPlan : Entity<BudgetPlanId>
    {
        public DateTimeRange BudgetPeriod { get; private set; }
        public IReadOnlyCollection<BudgetedTransactionCategory> BudgetedTransactionCategories => _budgetedTransactionCategories;
        private readonly List<BudgetedTransactionCategory> _budgetedTransactionCategories;
        public IReadOnlyCollection<Transaction> Transactions => _transactions;
        private readonly List<Transaction> _transactions;

        private BudgetPlan()
        { }

        public BudgetPlan(DateTimeRange budgetPeriod): base(new BudgetPlanId())
        {
            _budgetedTransactionCategories = new ();
            BudgetPeriod = budgetPeriod;
            _transactions = new ();
        }

        public void SetBudgetForCategory(TransactionCategory category, Money.DB.Money budgetedAmount)
        {
            if (_budgetedTransactionCategories.Any(
                    budgetedTransactionCategory => budgetedTransactionCategory.Category == category))
            {
                throw new DomainException<BudgetPlan>(
                    $"Category: {category.Value} is already budgeted for period: [{BudgetPeriod.Start:dd/MM/yyyy} - {BudgetPeriod.End:dd/MM/yyyy}]");
            }

            var budgetedTransactionCategory = new BudgetedTransactionCategory(category, budgetedAmount);

            _budgetedTransactionCategories.Add(budgetedTransactionCategory);
        }

        public void AddTransaction(Transaction transaction)
        {
            var budgetedTransactionCategory = _budgetedTransactionCategories
                .SingleOrDefault(budgetedTransactionCategory => budgetedTransactionCategory.Category == transaction.Category);

            if (budgetedTransactionCategory is null)
            {
                throw new DomainException<BudgetPlan>(
                    $"Category: {transaction.Category.Value} is not budgeted for period: [{BudgetPeriod.Start:dd/MM/yyyy} - {BudgetPeriod.End:dd/MM/yyyy}]");
            }

            budgetedTransactionCategory.AddTransaction(transaction);
            _transactions.Add(transaction);
        }
    }
}
