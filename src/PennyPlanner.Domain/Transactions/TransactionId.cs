using CommonAbstractions.DB.Entities;

namespace PennyPlanner.Domain.Transactions
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
