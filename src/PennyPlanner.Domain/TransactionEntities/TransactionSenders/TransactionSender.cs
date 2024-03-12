using PennyPlanner.Domain.Users;

namespace PennyPlanner.Domain.TransactionEntities.TransactionSenders
{
    public sealed class TransactionSender : TransactionEntity
    {
        private TransactionSender()
        { }

        public TransactionSender(TransactionEntityName name, UserId userId) : base(name, userId)
        {
        }
    }
}
