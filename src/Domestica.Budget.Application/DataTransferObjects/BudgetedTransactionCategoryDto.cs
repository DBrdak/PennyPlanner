using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record BudgetedTransactionCategoryDto
    {
        public string Category { get; init; }
        public MoneyDto BudgetedAmount { get; init; }
        public MoneyDto ActualAmount { get; init; }

        private BudgetedTransactionCategoryDto(string Category,
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

            return new (domainObject.Category.Value, budgetedAmount, actualAmount);
        }

    }
}
