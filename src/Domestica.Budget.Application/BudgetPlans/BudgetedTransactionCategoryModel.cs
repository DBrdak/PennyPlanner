using System.Text.Json.Serialization;
using Domestica.Budget.Application.Shared.Models;

namespace Domestica.Budget.Application.BudgetPlans
{
    public sealed record BudgetedTransactionCategoryModel
    {
        public string CategoryId { get; init; }
        public MoneyModel BudgetedAmount { get; init; }
        public MoneyModel ActualAmount { get; init; }

        private BudgetedTransactionCategoryModel(
            string categoryId,
            MoneyModel budgetedAmount,
            MoneyModel actualAmount)
        {
            CategoryId = categoryId;
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

            return new(domainObject.CategoryId.Value.ToString(), budgetedAmount, actualAmount);
        }

    }
}
