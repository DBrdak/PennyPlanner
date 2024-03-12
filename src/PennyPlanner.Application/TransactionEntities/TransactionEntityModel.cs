using System.Text.Json.Serialization;
using PennyPlanner.Application.TransactionEntities.AddTransactionEntity;
using PennyPlanner.Application.Transactions;
using PennyPlanner.Domain.TransactionEntities.TransactionRecipients;
using PennyPlanner.Domain.TransactionEntities.TransactionSenders;

namespace PennyPlanner.Application.TransactionEntities
{
    public sealed record TransactionEntityModel
    {
        public string TransactionEntityId { get; init; }
        public string Name { get; init; }
        public string TransactionEntityType { get; init; }
        public IEnumerable<TransactionModel> Transactions { get; init; }

        private TransactionEntityModel(
            string transactionEntityId,
            string name,
            string transactionEntityType,
            IEnumerable<TransactionModel> transactions)
        {
            TransactionEntityId = transactionEntityId;
            Name = name;
            TransactionEntityType = transactionEntityType;
            Transactions = transactions;
        }

        [JsonConstructor]
        private TransactionEntityModel()
        {

        }

        internal static TransactionEntityModel FromDomainObject(
            Domain.TransactionEntities.TransactionEntity domainObject)
        {
            TransactionEntityType transactionEntityType;

            if (domainObject is TransactionSender)
            {
                transactionEntityType = AddTransactionEntity.TransactionEntityType.Sender;
            }
            else if (domainObject is TransactionRecipient)
            {
                transactionEntityType = AddTransactionEntity.TransactionEntityType.Recipient;
            }
            else
            {
                throw new ArgumentException("Unknown transaction entity type");
            }

            var transactions = domainObject.Transactions.Select(TransactionModel.FromDomainObject);

            return new TransactionEntityModel(
                domainObject.Id.Value.ToString(),
                domainObject.Name.Value,
                transactionEntityType.Value,
                transactions);
        }
    }
}
