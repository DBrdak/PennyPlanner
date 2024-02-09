using Domestica.Budget.Domain.Accounts;
using Money.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domestica.Budget.Domain.TransactionCategories;

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record TransactionCategoryDto
    {
        public string TransactionCategoryId { get; set; }
        public string Value { get; init; }

        private TransactionCategoryDto(string transactionCategoryId, string value)
        {
            TransactionCategoryId = transactionCategoryId;
            Value = value;
        }

        [JsonConstructor]
        private TransactionCategoryDto()
        {
            
        }


        internal static TransactionCategoryDto? FromDomainObject(TransactionCategory? domainObject)
        {
            if (domainObject is null)
            {
                return null;
            }

            return new(domainObject.Id.Value.ToString(), domainObject.Value.Value);
        }
    }
}
