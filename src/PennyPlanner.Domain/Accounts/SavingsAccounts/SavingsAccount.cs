using Money.DB;
using PennyPlanner.Domain.Users;

#pragma warning disable CS8618

namespace PennyPlanner.Domain.Accounts.SavingsAccounts
{
    public sealed class SavingsAccount : Account
    {
        private SavingsAccount()
        { }

        public SavingsAccount(
            AccountName name,
            Currency currency,
            UserId userId,
            decimal initialBalance = 0) : base(
            name,
            currency,
            initialBalance,
            userId)
        {
        }
    }
}
