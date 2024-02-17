using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domestica.Budget.Domain.TransactionSubcategories;

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record TransactionSubcategoryDto
    {
        public string TransactionSubcategoryId { get; init; }
        public string Value { get; init; }
        public string CategoryId { get; init; }

        private TransactionSubcategoryDto(string transactionSubcategoryId, string value, string categoryId)
        {
            Value = value;
            TransactionSubcategoryId = transactionSubcategoryId;
            CategoryId = categoryId;
        }

        [JsonConstructor]
        private TransactionSubcategoryDto()
        {
        }

        internal static TransactionSubcategoryDto FromDomainObject(TransactionSubcategory domainObject)
        {
            return new(domainObject.Id.Value.ToString(), domainObject.Value.Value, domainObject.CategoryId.Value.ToString());
        }
    }
}
