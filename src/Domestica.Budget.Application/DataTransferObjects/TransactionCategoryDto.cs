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

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record TransactionCategoryDto
    {
        public string TransactionCategoryId { get; init; }
        public string Value { get; init; }
        public string Type { get; init; }

        private TransactionCategoryDto(string transactionCategoryId, string value, string transactionCategoryType)
        {
            TransactionCategoryId = transactionCategoryId;
            Value = value;
            Type = transactionCategoryType;
        }

        [JsonConstructor]
        private TransactionCategoryDto(string transactionCategoryType)
        {
            Type = transactionCategoryType;
        }


        internal static TransactionCategoryDto? FromDomainObject(TransactionCategory? domainObject)
        {
            if (domainObject is OutcomeTransactionCategory)
            {
                return new(domainObject.Id.Value.ToString(), domainObject.Value.Value, TransactionCategoryType.Outcome.Value);
            }

            if (domainObject is IncomeTransactionCategory)
            {
                return new(domainObject.Id.Value.ToString(), domainObject.Value.Value, TransactionCategoryType.Income.Value);
            }

            return null;
        }
    }
}
