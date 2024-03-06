using Domestica.Budget.Domain.Users;
using Money.DB;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.Accounts.SavingsAccounts
{
    public sealed class SavingsAccount : Account
    {
        private SavingsAccount()
        { }

        public SavingsAccount(
            AccountName name,
            Currency currency,
            UserIdentityId userId,
            decimal initialBalance = 0) : base(
            name,
            currency,
            initialBalance,
            userId)
        {
        }
    }
}
