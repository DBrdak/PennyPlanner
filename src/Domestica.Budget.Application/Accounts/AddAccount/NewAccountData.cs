using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.Accounts;
using Money.DB;

namespace Domestica.Budget.Application.Accounts.AddAccount
{
    public sealed record NewAccountData(string Type, string Name, MoneyDto InitialBalance)
    {
    }
}
