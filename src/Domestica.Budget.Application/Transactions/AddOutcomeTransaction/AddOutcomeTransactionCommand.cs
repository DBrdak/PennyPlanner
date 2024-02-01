using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.Transactions.AddOutcomeTransaction
{
    public sealed record AddOutcomeTransactionCommand(
        string SourceAccountId,
        string RecipientId,
        decimal TransactionAmount,
        string Category,
        DateTime TransactionDateTime) : ICommand<Transaction>
    {
    }
}
