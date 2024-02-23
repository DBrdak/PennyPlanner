using Domestica.Budget.Domain.Accounts;
using Money.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domestica.Budget.Application.TransactionCategories.AddTransactionCategory;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionSubcategories;
using Domestica.Budget.Application.TransactionSubcategories;

namespace Domestica.Budget.Application.TransactionCategories
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
