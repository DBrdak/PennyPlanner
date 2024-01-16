using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;

namespace Domestica.Budget.Application.TransactionEntities.GetTransactionEntities
{
    public sealed record GetTransactionEntitiesQuery : IQuery<List<TransactionEntity>>
    {
    }
}
