using PennyPlanner.Domain.Users;

namespace PennyPlanner.Domain.TransactionCategories
{
    public sealed class IncomeTransactionCategory : TransactionCategory
    {
        public IncomeTransactionCategory(TransactionCategoryValue value, UserId userId) : base(value, userId)
        {
        }
    }
}
