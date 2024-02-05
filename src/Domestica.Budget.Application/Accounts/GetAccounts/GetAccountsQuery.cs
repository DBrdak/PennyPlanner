using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;

namespace Domestica.Budget.Application.Accounts.GetAccounts
{
    public sealed record GetAccountsQuery() : IQuery<List<AccountDto>>
    {
    }
}
