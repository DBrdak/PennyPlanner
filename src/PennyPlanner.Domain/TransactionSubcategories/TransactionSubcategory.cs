using System.Text.Json.Serialization;
using PennyPlanner.Domain.Shared;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Domain.TransactionSubcategories
{
    public sealed class TransactionSubcategory : IdentifiedEntity<TransactionSubcategoryId>
    {
        public TransactionSubcategoryValue Value { get; private set; }
        public TransactionCategoryId CategoryId { get; private set; }
        public TransactionCategory Category { get; private set; }

        public TransactionSubcategory(TransactionSubcategoryValue value, TransactionCategory category, UserId userId) : base(userId)
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
