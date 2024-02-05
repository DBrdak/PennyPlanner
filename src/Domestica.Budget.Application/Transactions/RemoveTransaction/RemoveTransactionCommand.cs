using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.Transactions.RemoveTransaction
{
    public sealed record RemoveTransactionCommand(string TransactionId) : ICommand<Transaction>
    {
    }
}
