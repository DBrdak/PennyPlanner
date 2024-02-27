using System.Text.Json.Serialization;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Shared;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;
using Domestica.Budget.Domain.Transactions.DomainEvents;
using Domestica.Budget.Domain.TransactionSubcategories;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.Transactions
{
    public sealed class Transaction : Entity<TransactionId>
    {
        public AccountId? AccountId { get; init; } // Affected Account (the one which will have added TransactionAmount to balance)
        public AccountId? FromAccountId { get; init; } // Internal Transaction
        public AccountId? ToAccountId { get; init; } // Internal Transaction
        public TransactionEntityId? SenderId { get; init; } // Income Transaction
        public TransactionEntityId? RecipientId { get; init; } // Outcome Transaction
        public Money.DB.Money TransactionAmount { get; init; }
        public TransactionCategoryId? CategoryId { get; init; }
        public TransactionCategory? Category { get; init; }
        public TransactionSubcategory? Subcategory { get; init; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime TransactionDateUtc { get; init; }

        private Transaction()
        { }

        private Transaction(
            Account account,
            Account? fromAccount,
            Account? toAccount,
            TransactionSender? sender,
            TransactionRecipient? recipient,
            Money.DB.Money transactionAmount,
            TransactionSubcategory? subcategory,
            DateTime transactionDateTime) : base(new TransactionId())
        {
            AccountId = account.Id;
            FromAccountId = fromAccount?.Id;
            ToAccountId = toAccount?.Id;
            SenderId = sender?.Id;
            RecipientId = recipient?.Id;
            TransactionAmount = transactionAmount;
            TransactionDateUtc = transactionDateTime.ToUniversalTime();
            CategoryId = subcategory?.CategoryId;
            Category = subcategory?.Category;
            Subcategory = subcategory;

            RaiseDomainEvent(new TransactionCreatedDomainEvent(this));
        }

        internal static Transaction CreateIncome(
            Money.DB.Money transactionAmount,
            Account toAccount,
            TransactionSender sender,
            TransactionSubcategory subcategory,
            DateTime transactionDateTime)
        {
            return new(toAccount, null, null, sender, null, transactionAmount, subcategory, transactionDateTime);
        }

        internal static Transaction CreateOutcome(
            Money.DB.Money transactionAmount,
            Account fromAccount,
            TransactionRecipient recipient,
            TransactionSubcategory subcategory,
            DateTime transactionDateTime)
        {
            if (transactionAmount.Amount > 0)
            {
                return new(fromAccount, null, null, null, recipient, -transactionAmount, subcategory, transactionDateTime);
            }

            return new(fromAccount, null, null, null, recipient, transactionAmount, subcategory, transactionDateTime);
        }

        internal static Transaction CreateInternalIncome(
            Money.DB.Money transactionAmount,
            Account toAccount,
            Account fromAccount,
            DateTime transactionDateTime)
        {
            return new(toAccount, fromAccount, null, null, null, transactionAmount, null, transactionDateTime);
        }

        internal static Transaction CreateInternalOutcome(
            Money.DB.Money transactionAmount,
            Account fromAccount,
            Account toAccount,
            DateTime transactionDateTime)
        {
            if (transactionAmount.Amount > 0)
            {
                return new(fromAccount, null, toAccount, null, null, -transactionAmount, null, transactionDateTime);
            }

            return new(fromAccount, null, toAccount, null, null, transactionAmount, null, transactionDateTime);
        }

        internal static Transaction CreatePrivateIncome(
            Money.DB.Money transactionAmount,
            Account account)
        {
            return new(account, null, null, null, null, transactionAmount, null, DateTime.UtcNow);
        }

        internal static Transaction CreatePrivateOutcome(
            Money.DB.Money transactionAmount,
            Account account)
        {
            if (transactionAmount.Amount > 0)
            {
                return new(account, null, null, null, null, -transactionAmount, null, DateTime.UtcNow);
            }

            return new(account, null, null, null, null, transactionAmount, null, DateTime.UtcNow);
        }

        public void SafeDelete()
        {
            RaiseDomainEvent(new TransactionDeletedDomainEvent(this));
        }
    }
}
