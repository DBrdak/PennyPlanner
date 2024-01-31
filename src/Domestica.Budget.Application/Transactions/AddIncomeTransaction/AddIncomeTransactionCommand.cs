using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.Transactions.AddIncomeTransaction
{
    public sealed record AddIncomeTransactionCommand(
        string DestinationAccountId,
        string SenderId,
        decimal TransactionAmount,
        string Category) : ICommand<Transaction>
    {
    }
}
