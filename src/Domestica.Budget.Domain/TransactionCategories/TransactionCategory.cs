using System.Text.Json.Serialization;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.TransactionSubcategories;
using Exceptions.DB;

namespace Domestica.Budget.Domain.TransactionCategories
{
    public abstract class TransactionCategory : Entity<TransactionCategoryId>
    {
        public TransactionCategoryValue Value { get; private set; }
        private readonly List<TransactionSubcategory> _subcategories;
        public IReadOnlyCollection<TransactionSubcategory> Subcategories => _subcategories;

        protected TransactionCategory(TransactionCategoryValue value) : base(new TransactionCategoryId())
        {
            Value = value;
            _subcategories = new();
        }

        [JsonConstructor]
        protected TransactionCategory()
        { }

        public void UpdateValue(TransactionCategoryValue value)
        {
            Value = value;
        }

        public void AddSubcategory(TransactionSubcategory subcategory)
        {
            if (_subcategories.Any(sc => string.Equals(sc.Value.Value, subcategory.Value.Value, StringComparison.CurrentCultureIgnoreCase)))
            {
                return;
            }

            _subcategories.Add(subcategory);
        }
    }
}
