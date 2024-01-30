using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.Accounts;

namespace Domestica.Budget.Application.Accounts.GetAccounts
{
    public sealed record GetAccountsQuery() : IQuery<List<AccountDto>>
    {
    }
}
