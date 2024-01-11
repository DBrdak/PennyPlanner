using Domestica.Budget.Domain.Accounts;
using Money.DB;

namespace Domestica.Budget.Application.Accounts.AddAccount
{
    public sealed record NewAccountData(AccountType Type, AccountName Name, Money.DB.Money InitialBalance)
    {
    }
}
