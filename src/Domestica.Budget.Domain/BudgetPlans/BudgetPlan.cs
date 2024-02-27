using System.ComponentModel.DataAnnotations;
using CommonAbstractions.DB.Entities;
using DateKit.DB;
using Domestica.Budget.Domain.BudgetPlans.DomainEvents;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.Transactions;
using Exceptions.DB;
using MediatR;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.BudgetPlans
{
    public sealed class BudgetPlan : Entity<BudgetPlanId>
    {
        public DateTimeRange BudgetPeriod { get; init; }
        public IReadOnlyCollection<BudgetedTransactionCategory> BudgetedTransactionCategories => _budgetedTransactionCategories;
        private readonly List<BudgetedTransactionCategory> _budgetedTransactionCategories;
        public IReadOnlyCollection<Transaction> Transactions => _transactions;
        private readonly List<Transaction> _transactions;

        private BudgetPlan()
        { }

        private BudgetPlan(DateTimeRange budgetPeriod): base(new BudgetPlanId())
        {
            _budgetedTransactionCategories = new ();
            BudgetPeriod = budgetPeriod.ParseToUTC();
            _transactions = new ();
        }

        public static BudgetPlan Create(DateTimeRange budgetPeriod)
        {
            Validate(budgetPeriod);

            return new BudgetPlan(budgetPeriod);
        }

        public static BudgetPlan CreateForMonth(DateTime date) => Create(GetMonthRangeFromDateTime(date));

        private static DateTimeRange GetMonthRangeFromDateTime(DateTime date)
        {
            return new DateTimeRange(
                new DateTime(date.Year, date.Month, 1),
                new DateTime(date.Year, date.Month + 1, 1));
        }


        private static void Validate(DateTimeRange budgetPeriod)
        {
            if (!IsMonthDateGreaterThanUtcNow(budgetPeriod.Start) ||
                !IsMonthDateGreaterThanUtcNow(budgetPeriod.End))
            {
                throw new DomainException<BudgetPlan>("Budget plan period should be a future period");
            }
        }

        private static bool IsMonthDateGreaterThanUtcNow(DateTime date) =>
            date.Month >= DateTime.UtcNow.Month &&
            date.Year >= DateTime.UtcNow.Year;

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

            RaiseDomainEvent(new BudgetPlanCategoryBudgetedDomainEvent(Id, category));
        }

        public void AddTransaction(Transaction transaction)
        {
            var budgetedTransactionCategory = _budgetedTransactionCategories
                .SingleOrDefault(budgetedTransactionCategory => budgetedTransactionCategory.CategoryId == transaction.CategoryId);

            if (budgetedTransactionCategory is null)
            {
                throw new DomainException<BudgetPlan>(
                    $"Category: {transaction.Category?.Value} is not budgeted for period: [{BudgetPeriod.Start:dd/MM/yyyy} - {BudgetPeriod.End:dd/MM/yyyy}]");
            }

            budgetedTransactionCategory.AddTransaction(transaction);
            _transactions.Add(transaction);
        }

        public void UpdateBudgetCategory(TransactionCategory category, Money.DB.Money budgetedAmount)
        {
            var budgetedTransactionCategory = _budgetedTransactionCategories.SingleOrDefault(budgetedTransactionCategory => budgetedTransactionCategory.Category == category);

            if (budgetedTransactionCategory is null)
            {
                throw new DomainException<BudgetPlan>(
                                       $"Category: {category.Value} is not budgeted for period: [{BudgetPeriod.Start:dd/MM/yyyy} - {BudgetPeriod.End:dd/MM/yyyy}]");
            }

            budgetedTransactionCategory.UpdateBudgetedAmount(budgetedAmount);
        }

        public void ResetBudgetCategory(TransactionCategory category)
        {
            var budgetedTransactionCategory = _budgetedTransactionCategories.SingleOrDefault(budgetedTransactionCategory => budgetedTransactionCategory.Category == category);

            if (budgetedTransactionCategory is null)
            {
                throw new DomainException<BudgetPlan>(
                    $"Category: {category.Value} is not budgeted for period: [{BudgetPeriod.Start:dd/MM/yyyy} - {BudgetPeriod.End:dd/MM/yyyy}]");
            }

            _budgetedTransactionCategories.Remove(budgetedTransactionCategory);

            _transactions.RemoveAll(transaction => transaction.Category == category);
        }

        public void RemoveTransaction(Transaction transaction)
        {
            var budgetedCategory = _budgetedTransactionCategories
                .FirstOrDefault(btc => btc.CategoryId == transaction.CategoryId);

            if (budgetedCategory is null)
            {
                throw new DomainException<BudgetPlan>(
                    $"Category with ID: {budgetedCategory?.CategoryId} is not budgeted for period: [{BudgetPeriod.Start:dd/MM/yyyy} - {BudgetPeriod.End:dd/MM/yyyy}]");
            }

            budgetedCategory.RemoveTransaction(transaction);
        }
    }
}
