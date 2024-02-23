using System.Text.Json.Serialization;
using DateKit.DB;
using Domestica.Budget.Application.Transactions;
using Domestica.Budget.Domain.BudgetPlans;

namespace Domestica.Budget.Application.BudgetPlans
{
    public sealed record BudgetPlanModel
    {
        public string BudgetPlanId { get; init; }
        public DateTimeRange BudgetPeriod { get; init; }
        public IEnumerable<BudgetedTransactionCategoryModel> BudgetedTransactionCategories { get; init; }
        public IEnumerable<TransactionModel> Transactions { get; init; }

        private BudgetPlanModel(
            string budgetPlanId,
            DateTimeRange BudgetPeriod,
            IEnumerable<BudgetedTransactionCategoryModel> BudgetedTransactionCategories,
            IEnumerable<TransactionModel> Transactions)
        {
            this.BudgetPeriod = BudgetPeriod;
            this.BudgetedTransactionCategories = BudgetedTransactionCategories;
            this.Transactions = Transactions;
            BudgetPlanId = budgetPlanId;
        }

        [JsonConstructor]
        private BudgetPlanModel()
        {
        }

        internal static BudgetPlanModel? FromDomainObject(BudgetPlan? domainObject)
        {
            if (domainObject is null)
            {
                return null;
            }

            var transactions = domainObject.Transactions.Select(TransactionModel.FromDomainObject);
            var budgetedTransactionCategories =
                domainObject.BudgetedTransactionCategories.Select(BudgetedTransactionCategoryModel.FromDomainObject);

            return new(domainObject.Id.Value.ToString(), domainObject.BudgetPeriod, budgetedTransactionCategories, transactions);
        }
    }
}
