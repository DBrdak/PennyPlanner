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

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record TransactionCategoryDto
    {
        public string TransactionCategoryId { get; init; }
        public string Value { get; init; }
        public string Type { get; init; }
        public List<TransactionSubcategoryDto> Subcategories { get; init; }

        private TransactionCategoryDto(string transactionCategoryId, string value, string transactionCategoryType, List<TransactionSubcategoryDto> subcategories)
        {
            TransactionCategoryId = transactionCategoryId;
            Value = value;
            Type = transactionCategoryType;
            Subcategories = subcategories;
        }

        [JsonConstructor]
        private TransactionCategoryDto()
        {
        }


        internal static TransactionCategoryDto? FromDomainObject(TransactionCategory? domainObject)
        {
            if (domainObject is OutcomeTransactionCategory)
            {
                return new(
                    domainObject.Id.Value.ToString(),
                    domainObject.Value.Value,
                    TransactionCategoryType.Outcome.Value,
                    domainObject.Subcategories.Select(TransactionSubcategoryDto.FromDomainObject).ToList());
            }

            if (domainObject is IncomeTransactionCategory)
            {
                return new(
                    domainObject.Id.Value.ToString(),
                    domainObject.Value.Value,
                    TransactionCategoryType.Income.Value,
                    domainObject.Subcategories.Select(TransactionSubcategoryDto.FromDomainObject).ToList());
            }

            return null;
        }
    }
}
