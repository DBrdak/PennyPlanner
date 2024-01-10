using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.TransactionEntities
{
    public sealed record TransactionEntityId : EntityId
    {
        public TransactionEntityId(Guid value) : base(value)
        {
            
        }

        public TransactionEntityId() : base(Guid.NewGuid())
        { }
    }
}
