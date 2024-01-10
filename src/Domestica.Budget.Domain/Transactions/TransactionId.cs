using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.Transactions
{
    public sealed record TransactionId : EntityId
    {
        public TransactionId(Guid value) : base(value)
        {
            
        }

        public TransactionId() : base(Guid.NewGuid())
        { }
    }
}
