using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.Accounts;

namespace Domestica.Budget.Application.Accounts.UpdateAccount
{
    public sealed record AccountUpdateData(string AccountId, string Name, MoneyDto Balance)
    {
    }
}
