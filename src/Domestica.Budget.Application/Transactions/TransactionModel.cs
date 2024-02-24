using System.Text.Json.Serialization;
using Domestica.Budget.Application.Shared.Models;
using Domestica.Budget.Application.TransactionCategories;
using Domestica.Budget.Application.TransactionSubcategories;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.Transactions
{
    public sealed record TransactionModel
    {
        public string TransactionId { get; init; }
        public string? AccountId { get; init; }
        public string? FromAccountId { get; init; }
        public string? ToAccountId { get; init; }
        public string? SenderId { get; init; }
        public string? RecipientId { get; init; }
        public MoneyModel TransactionAmount { get; init; }
        public TransactionCategoryModel? Category { get; init; }
        public TransactionSubcategoryModel? Subcategory { get; init; }
        public DateTime TransactionDateUtc { get; init; }

        private TransactionModel(
            string? accountId,
            string? fromAccountId,
            string? toAccountId,
            string? senderId,
            string? recipientId,
            MoneyModel transactionAmount,
            TransactionCategoryModel? category,
            DateTime transactionDateUtc,
            string transactionId,
            TransactionSubcategoryModel? subcategory)
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
            Subcategory = subcategory;
        }

        [JsonConstructor]
        private TransactionModel(TransactionSubcategoryModel? subcategory)
        {
            Subcategory = subcategory;
        }

        internal static TransactionModel FromDomainObject(Transaction domainObject)
        {
            var transactionAmount = MoneyModel.FromDomainObject(domainObject.TransactionAmount);

            return new TransactionModel(
                domainObject.AccountId?.Value.ToString(),
                domainObject.FromAccountId?.Value.ToString(),
                domainObject.ToAccountId?.Value.ToString(),
                domainObject.SenderId?.Value.ToString(),
                domainObject.RecipientId?.Value.ToString(),
                transactionAmount,
                TransactionCategoryModel.FromDomainObject(domainObject.Category),
                domainObject.TransactionDateUtc,
                domainObject.Id.Value.ToString(),
                TransactionSubcategoryModel.FromDomainObject(domainObject.Subcategory));
        }
    }
}
