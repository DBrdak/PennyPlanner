using System.Text.Json.Serialization;

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record BudgetedTransactionCategoryDto
    {
        public TransactionCategoryDto Category { get; init; }
        public MoneyDto BudgetedAmount { get; init; }
        public MoneyDto ActualAmount { get; init; }

        private BudgetedTransactionCategoryDto(TransactionCategoryDto Category,
            MoneyDto BudgetedAmount,
            MoneyDto ActualAmount)
        {
            this.Category = Category;
            this.BudgetedAmount = BudgetedAmount;
            this.ActualAmount = ActualAmount;
        }

        [JsonConstructor]
        private BudgetedTransactionCategoryDto()
        {
        }

        internal static BudgetedTransactionCategoryDto FromDomainObject(
            Domestica.Budget.Domain.BudgetPlans.BudgetedTransactionCategory domainObject)
        {
            var budgetedAmount = MoneyDto.FromDomainObject(domainObject.BudgetedAmount);
            var actualAmount = MoneyDto.FromDomainObject(domainObject.ActualAmount);

            return new(TransactionCategoryDto.FromDomainObject(domainObject.Category)!, budgetedAmount, actualAmount);
        }

    }
}
