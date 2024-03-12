using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Domain.TransactionCategories
{
    public sealed class OutcomeTransactionCategory : TransactionCategory
    {
        public OutcomeTransactionCategory(TransactionCategoryValue value, UserId userId) : base(value, userId)
        {
        }
    }
}
