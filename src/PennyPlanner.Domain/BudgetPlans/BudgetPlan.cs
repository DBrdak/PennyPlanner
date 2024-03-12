﻿using DateKit.DB;
using Exceptions.DB;
using PennyPlanner.Domain.BudgetPlans.DomainEvents;
using PennyPlanner.Domain.Shared;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.Transactions;
using PennyPlanner.Domain.Users;

#pragma warning disable CS8618

namespace PennyPlanner.Domain.BudgetPlans
{
    public sealed class BudgetPlan : IdentifiedEntity<BudgetPlanId>
    {
        public DateTimeRange BudgetPeriod { get; init; }
        public IReadOnlyCollection<BudgetedTransactionCategory> BudgetedTransactionCategories => _budgetedTransactionCategories;
        private readonly List<BudgetedTransactionCategory> _budgetedTransactionCategories;
        public IReadOnlyCollection<Transaction> Transactions => _transactions;
        private readonly List<Transaction> _transactions;

        private BudgetPlan()
        { }

        private BudgetPlan(DateTimeRange budgetPeriod, UserId userId): base(userId)
        {
            _budgetedTransactionCategories = new ();
            BudgetPeriod = budgetPeriod.ParseToUTC();
            _transactions = new ();
            UserId = userId;
        }

        public static BudgetPlan Create(DateTimeRange budgetPeriod, UserId userId)
        {
            Validate(budgetPeriod);

            return new BudgetPlan(budgetPeriod, userId);
        }

        public static BudgetPlan CreateForMonth(DateTime date, UserId userId) => Create(GetMonthRangeFromDateTime(date), userId);

        private static DateTimeRange GetMonthRangeFromDateTime(DateTime date)
        {
            if (date.Month == 12)
            {
                return new DateTimeRange(
                    new DateTime(date.Year, date.Month, 1),
                    new DateTime(date.Year + 1, 1, 1));
            }

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
            (date.Month >= DateTime.UtcNow.Month &&
             date.Year >= DateTime.UtcNow.Year) || 
            date.Year > DateTime.UtcNow.Year;

        private bool IsCategoryAlreadyBudgeted(TransactionCategory category, Money.DB.Money budgetedAmount) =>
            _budgetedTransactionCategories.Any(
                budgetedTransactionCategory => budgetedTransactionCategory.Category == category &&
                                               budgetedTransactionCategory.BudgetedAmount == budgetedAmount);

        public void SetBudgetForCategory(TransactionCategory category, Money.DB.Money budgetedAmount)
        {
            if (IsCategoryAlreadyBudgeted(category, budgetedAmount))
            {
                return;
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
            var budgetedTransactionCategory = _budgetedTransactionCategories.FirstOrDefault(budgetedTransactionCategory => budgetedTransactionCategory.Category == category);

            if (budgetedTransactionCategory is null)
            {
                throw new DomainException<BudgetPlan>(
                                       $"Category: {category.Value} is not budgeted for period: [{BudgetPeriod.Start:dd/MM/yyyy} - {BudgetPeriod.End:dd/MM/yyyy}]");
            }

            budgetedTransactionCategory.UpdateBudgetedAmount(budgetedAmount);
        }

        public void ResetBudgetCategory(TransactionCategory category)
        {
            var budgetedTransactionCategory = _budgetedTransactionCategories.FirstOrDefault(budgetedTransactionCategory => budgetedTransactionCategory.Category == category);

            if (budgetedTransactionCategory is null)
            {
                throw new DomainException<BudgetPlan>(
                    $"Category: {category.Value} is not budgeted for period: [{BudgetPeriod.Start:dd/MM/yyyy} - {BudgetPeriod.End:dd/MM/yyyy}]");
            }

            _budgetedTransactionCategories.Remove(budgetedTransactionCategory);

            _transactions.RemoveAll(transaction => transaction.CategoryId == category.Id);
        }

        public void RemoveTransaction(Transaction transaction)
        {
            var budgetedCategory = _budgetedTransactionCategories
                .FirstOrDefault(btc => btc.CategoryId == transaction.CategoryId);

            budgetedCategory?.RemoveTransaction(transaction);
        }
    }
}