using Money.DB;

namespace Budgetify.Domain.Accounts.BankAccounts
{
    public sealed class BankAccount : Account
    {
        public BankAccount(AccountName name, Currency currency) : base(name, currency)
        {
        }
    }
}
