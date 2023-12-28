using Money.DB;

namespace Budgetify.Domain.Accounts.SavingsAccounts
{
    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(
            AccountName name,
            Currency currency) : base(
            name,
            currency)
        {
        }

    }
}
