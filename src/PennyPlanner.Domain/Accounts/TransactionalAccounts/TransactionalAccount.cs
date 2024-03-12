using Money.DB;
using PennyPlanner.Domain.Users;

#pragma warning disable CS8618

namespace PennyPlanner.Domain.Accounts.TransactionalAccounts
{
    public sealed class TransactionalAccount : Account
    {
        private TransactionalAccount()
        { }

        public TransactionalAccount(
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
