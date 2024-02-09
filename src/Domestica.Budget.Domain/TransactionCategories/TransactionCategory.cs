using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.TransactionCategories
{
    public abstract class TransactionCategory : Entity<TransactionCategoryId>
    {
        public TransactionCategoryValue Value { get; init; }

        protected TransactionCategory(TransactionCategoryValue value) : base(new TransactionCategoryId())
        {
            Value = value;
        }
    }
}
