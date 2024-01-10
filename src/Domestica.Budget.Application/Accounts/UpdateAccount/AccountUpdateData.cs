using Domestica.Budget.Domain.Accounts;

namespace Domestica.Budget.Application.Accounts.UpdateAccount
{
    public sealed record AccountUpdateData(AccountId AccountId, AccountName Name, Money.DB.Money Balance)
    {
    }
}
