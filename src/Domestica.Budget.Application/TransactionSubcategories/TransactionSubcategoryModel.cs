using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domestica.Budget.Domain.TransactionSubcategories;

namespace Domestica.Budget.Application.TransactionSubcategories
{
    public sealed record TransactionSubcategoryModel
    {
        public string TransactionSubcategoryId { get; init; }
        public string Value { get; init; }
        public string CategoryId { get; init; }

        private TransactionSubcategoryModel(string transactionSubcategoryId, string value, string categoryId)
        {
            Value = value;
            TransactionSubcategoryId = transactionSubcategoryId;
            CategoryId = categoryId;
        }

        [JsonConstructor]
        private TransactionSubcategoryModel()
        {
        }

        internal static TransactionSubcategoryModel? FromDomainObject(TransactionSubcategory? domainObject)
        {
            if (domainObject is null)
            {
                return null;
            }

            return new(domainObject.Id.Value.ToString(), domainObject.Value.Value, domainObject.CategoryId.Value.ToString());
        }
    }
}
