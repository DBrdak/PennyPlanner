using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Domain.TransactionCategories
{
    public sealed class IncomeTransactionCategory : TransactionCategory
    {
        public IncomeTransactionCategory(TransactionCategoryValue value, UserIdentityId userId) : base(value, userId)
        {
        }
    }
}
