using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;

namespace Domestica.Budget.Application.TransactionEntities.GetTransactionEntities
{
    public sealed record GetTransactionEntitiesQuery : IQuery<List<TransactionEntityDto>>
    {
    }
}
