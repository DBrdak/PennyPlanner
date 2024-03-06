using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Domain.TransactionEntities.TransactionSenders
{
    public sealed class TransactionSender : TransactionEntity
    {
        private TransactionSender()
        { }

        public TransactionSender(TransactionEntityName name, UserIdentityId userId) : base(name, userId)
        {
        }
    }
}
