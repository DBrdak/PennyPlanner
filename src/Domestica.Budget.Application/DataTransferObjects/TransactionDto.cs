using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Shared;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record TransactionDto
    {
        public string? AccountId { get; init; }
        public string? FromAccountId { get; init; }
        public string? ToAccountId { get; init; }
        public string? SenderId { get; init; }
        public string? RecipientId { get; init; } 
        public MoneyDto TransactionAmount { get; init; }
        public string Category { get; init; }
        public DateTime TransactionDateUtc { get; init; }

        private TransactionDto(
            string? accountId,
            string? fromAccountId,
            string? toAccountId,
            string? senderId,
            string? recipientId,
            MoneyDto transactionAmount,
            string category,
            DateTime transactionDateUtc)
        {
            AccountId = accountId;
            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
            SenderId = senderId;
            RecipientId = recipientId;
            TransactionAmount = transactionAmount;
            Category = category;
            TransactionDateUtc = transactionDateUtc;
        }

        [JsonConstructor]
        private TransactionDto()
        {
            
        }

        internal static TransactionDto FromDomainObject(Transaction domainObject)
        {
            var transactionAmount = MoneyDto.FromDomainObject(domainObject.TransactionAmount);

            return new TransactionDto(
                domainObject.AccountId?.Value.ToString(),
                domainObject.FromAccountId?.Value.ToString(),
                domainObject.ToAccountId?.Value.ToString(),
                domainObject.SenderId?.Value.ToString(),
                domainObject.RecipientId?.Value.ToString(),
                transactionAmount,
                domainObject.Category.Value,
                domainObject.TransactionDateUtc);
        }
    }
}
