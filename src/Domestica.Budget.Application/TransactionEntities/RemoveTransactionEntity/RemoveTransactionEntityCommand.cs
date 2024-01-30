using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;

namespace Domestica.Budget.Application.TransactionEntities.RemoveTransactionEntity
{
    public sealed record RemoveTransactionEntityCommand(string TransactionEntityId) : ICommand<TransactionEntity>
    {
    }
}
