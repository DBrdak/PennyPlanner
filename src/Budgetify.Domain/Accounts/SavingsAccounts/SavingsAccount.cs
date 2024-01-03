using Money.DB;
#pragma warning disable CS8618

namespace Budgetify.Domain.Accounts.SavingsAccounts
{
    public sealed class SavingsAccount : Account
    {
        private SavingsAccount()
        { }

        public SavingsAccount(AccountName name, Currency currency, decimal initialBalance = 0) : base(name, currency, initialBalance)
        {
        }

    }
}
