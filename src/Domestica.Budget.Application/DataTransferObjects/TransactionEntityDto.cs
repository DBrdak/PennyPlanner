using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Application.TransactionEntities.AddTransactionEntity;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record TransactionEntityDto
    {
        public string TransactionEntityId { get; init; }
        public string Name { get; init; }
        public string TransactionEntityType { get; init; }
        public IEnumerable<TransactionDto> Transactions { get; init; }

        private TransactionEntityDto(
            string transactionEntityId,
            string name,
            string transactionEntityType,
            IEnumerable<TransactionDto> transactions)
        {
            TransactionEntityId = transactionEntityId;
            Name = name;
            TransactionEntityType = transactionEntityType;
            Transactions = transactions;
        }

        internal static TransactionEntityDto FromDomainObject(
            Domestica.Budget.Domain.TransactionEntities.TransactionEntity domainObject)
        {
            TransactionEntityType transactionEntityType;

            if (domainObject is TransactionSender)
            {
                transactionEntityType = TransactionEntities.AddTransactionEntity.TransactionEntityType.Sender;
            }
            else if (domainObject is TransactionRecipient)
            {
                transactionEntityType = TransactionEntities.AddTransactionEntity.TransactionEntityType.Recipient;
            }
            else
            {
                throw new ArgumentException("Unknown transaction entity type");
            }

            var transactions = domainObject.Transactions.Select(TransactionDto.FromDomainObject);

            return new TransactionEntityDto(
                domainObject.Id.Value.ToString(),
                domainObject.Name.Value,
                transactionEntityType.Value,
                transactions);
        }
    }
}
