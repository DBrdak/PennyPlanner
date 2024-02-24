using System.Text.Json.Serialization;
using Domestica.Budget.Application.Shared.Models;
using Domestica.Budget.Application.TransactionCategories;

namespace Domestica.Budget.Application.BudgetPlans
{
    public sealed record BudgetedTransactionCategoryModel
    {
        public TransactionCategoryModel Category { get; init; }
        public MoneyModel BudgetedAmount { get; init; }
        public MoneyModel ActualAmount { get; init; }

        private BudgetedTransactionCategoryModel(
            TransactionCategoryModel category,
            MoneyModel budgetedAmount,
            MoneyModel actualAmount)
        {
            Category = category;
            BudgetedAmount = budgetedAmount;
            ActualAmount = actualAmount;
        }

        [JsonConstructor]
        private BudgetedTransactionCategoryModel()
        { }

        internal static BudgetedTransactionCategoryModel FromDomainObject(
            Domain.BudgetPlans.BudgetedTransactionCategory domainObject)
        {
            var budgetedAmount = MoneyModel.FromDomainObject(domainObject.BudgetedAmount);
            var actualAmount = MoneyModel.FromDomainObject(domainObject.ActualAmount);

            return new(TransactionCategoryModel.FromDomainObject(domainObject.Category)!, budgetedAmount, actualAmount);
        }

    }
}
