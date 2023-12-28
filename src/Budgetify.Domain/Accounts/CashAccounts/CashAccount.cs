using Money.DB;

namespace Budgetify.Domain.Accounts.CashAccounts
{
    public sealed class CashAccount : Account
    {
        public CashAccount(AccountName name, Currency currency) : base(name, currency)
        {
        }
    }
}
