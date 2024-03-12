using System.Text.Json.Serialization;
using PennyPlanner.Application.TransactionCategories.AddTransactionCategory;
using PennyPlanner.Application.TransactionSubcategories;
using PennyPlanner.Domain.TransactionCategories;

namespace PennyPlanner.Application.TransactionCategories
{
    public sealed record TransactionCategoryModel
    {
        public string TransactionCategoryId { get; init; }
        public string Value { get; init; }
        public string Type { get; init; }
        public List<TransactionSubcategoryModel> Subcategories { get; init; }

        private TransactionCategoryModel(string transactionCategoryId, string value, string transactionCategoryType, List<TransactionSubcategoryModel> subcategories)
        {
            TransactionCategoryId = transactionCategoryId;
            Value = value;
            Type = transactionCategoryType;
            Subcategories = subcategories;
        }

        [JsonConstructor]
        private TransactionCategoryModel()
        {
        }


        internal static TransactionCategoryModel? FromDomainObject(TransactionCategory? domainObject)
        {
            if (domainObject is OutcomeTransactionCategory)
            {
                return new(
                    domainObject.Id.Value.ToString(),
                    domainObject.Value.Value,
                    TransactionCategoryType.Outcome.Value,
                    domainObject.Subcategories.Select(TransactionSubcategoryModel.FromDomainObject).ToList());
            }

            if (domainObject is IncomeTransactionCategory)
            {
                return new(
                    domainObject.Id.Value.ToString(),
                    domainObject.Value.Value,
                    TransactionCategoryType.Income.Value,
                    domainObject.Subcategories.Select(TransactionSubcategoryModel.FromDomainObject).ToList());
            }

            return null;
        }
    }
}
