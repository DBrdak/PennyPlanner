using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.TransactionCategories
{
    public abstract class TransactionCategory : Entity<TransactionCategoryId>
    {
        public TransactionCategoryValue Value { get; private set; }

        protected TransactionCategory(TransactionCategoryValue value) : base(new TransactionCategoryId())
        {
            Value = value;
        }

        public void UpdateValue(TransactionCategoryValue value)
        {
            Value = value;
        }
    }
}
