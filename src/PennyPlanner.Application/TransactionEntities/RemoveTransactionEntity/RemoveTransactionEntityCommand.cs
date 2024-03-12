using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.TransactionEntities;

namespace PennyPlanner.Application.TransactionEntities.RemoveTransactionEntity
{
    public sealed record RemoveTransactionEntityCommand(string TransactionEntityId) : ICommand<TransactionEntity>
    {
    }
}
