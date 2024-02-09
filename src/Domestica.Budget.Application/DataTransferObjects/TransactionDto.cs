using System.Text.Json.Serialization;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record TransactionDto
    {
        public string TransactionId { get; init; }
        public string? AccountId { get; init; }
        public string? FromAccountId { get; init; }
        public string? ToAccountId { get; init; }
        public string? SenderId { get; init; }
        public string? RecipientId { get; init; } 
        public MoneyDto TransactionAmount { get; init; }
        public TransactionCategoryDto? Category { get; init; }
        public DateTime TransactionDateUtc { get; init; }

        private TransactionDto(
            string? accountId,
            string? fromAccountId,
            string? toAccountId,
            string? senderId,
            string? recipientId,
            MoneyDto transactionAmount,
            TransactionCategoryDto? category,
            DateTime transactionDateUtc,
            string transactionId)
        {
            AccountId = accountId;
            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
            SenderId = senderId;
            RecipientId = recipientId;
            TransactionAmount = transactionAmount;
            Category = category;
            TransactionDateUtc = transactionDateUtc;
            TransactionId = transactionId;
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
                TransactionCategoryDto.FromDomainObject(domainObject.Category),
                domainObject.TransactionDateUtc,
                domainObject.Id.Value.ToString());
        }
    }
}
