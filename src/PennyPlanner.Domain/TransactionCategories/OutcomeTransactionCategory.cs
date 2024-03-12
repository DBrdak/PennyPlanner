using PennyPlanner.Domain.Users;

namespace PennyPlanner.Domain.TransactionCategories
{
    public sealed class OutcomeTransactionCategory : TransactionCategory
    {
        public OutcomeTransactionCategory(TransactionCategoryValue value, UserId userId) : base(value, userId)
        {
        }
    }
}
