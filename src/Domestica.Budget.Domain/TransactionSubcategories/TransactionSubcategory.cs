using System.Text.Json.Serialization;
using Domestica.Budget.Domain.Shared;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Domain.TransactionSubcategories
{
    public sealed class TransactionSubcategory : IdentifiedEntity<TransactionSubcategoryId>
    {
        public TransactionSubcategoryValue Value { get; private set; }
        public TransactionCategoryId CategoryId { get; private set; }
        public TransactionCategory Category { get; private set; }

        public TransactionSubcategory(TransactionSubcategoryValue value, TransactionCategory category, UserIdentityId userId) : base(userId)
        {
            Value = value;
            Category = category;
            CategoryId = category.Id;
        }

        [JsonConstructor]
        private TransactionSubcategory()
        { }

        public void UpdateValue(TransactionSubcategoryValue value) => Value = value;
    }
}
