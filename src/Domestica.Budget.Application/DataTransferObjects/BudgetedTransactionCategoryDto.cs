using System.Text.Json.Serialization;

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record BudgetedTransactionCategoryDto
    {
        public TransactionCategoryDto Category { get; init; }
        public MoneyDto BudgetedAmount { get; init; }
        public MoneyDto ActualAmount { get; init; }

        private BudgetedTransactionCategoryDto(
            TransactionCategoryDto category,
            MoneyDto budgetedAmount,
            MoneyDto actualAmount)
        {
            Category = category;
            BudgetedAmount = budgetedAmount;
            ActualAmount = actualAmount;
        }

        [JsonConstructor]
        private BudgetedTransactionCategoryDto(string budgetedTransactionCategoryId)
        { }

        internal static BudgetedTransactionCategoryDto FromDomainObject(
            Domestica.Budget.Domain.BudgetPlans.BudgetedTransactionCategory domainObject)
        {
            var budgetedAmount = MoneyDto.FromDomainObject(domainObject.BudgetedAmount);
            var actualAmount = MoneyDto.FromDomainObject(domainObject.ActualAmount);

            return new(TransactionCategoryDto.FromDomainObject(domainObject.Category)!, budgetedAmount, actualAmount);
        }

    }
}
