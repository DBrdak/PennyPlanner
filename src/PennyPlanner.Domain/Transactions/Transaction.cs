﻿using System.Text.Json.Serialization;
using PennyPlanner.Domain.Accounts;
using PennyPlanner.Domain.Shared;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.TransactionEntities;
using PennyPlanner.Domain.TransactionEntities.TransactionRecipients;
using PennyPlanner.Domain.TransactionEntities.TransactionSenders;
using PennyPlanner.Domain.Transactions.DomainEvents;
using PennyPlanner.Domain.TransactionSubcategories;
using PennyPlanner.Domain.Users;

#pragma warning disable CS8618

namespace PennyPlanner.Domain.Transactions
{
    public sealed class Transaction : IdentifiedEntity<TransactionId>
    {
        public AccountId AccountId { get; init; } // Affected Account (the one which will have added TransactionAmount to balance)
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
            DateTime transactionDateTime,
            UserId userId) : base(userId)
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
            return new(toAccount, null, null, sender, null, transactionAmount, subcategory, transactionDateTime, toAccount.UserId);
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
                return new(fromAccount, null, null, null, recipient, -transactionAmount, subcategory, transactionDateTime, fromAccount.UserId);
            }

            return new(fromAccount, null, null, null, recipient, transactionAmount, subcategory, transactionDateTime, fromAccount.UserId);
        }

        internal static Transaction CreateInternalIncome(
            Money.DB.Money transactionAmount,
            Account toAccount,
            Account fromAccount,
            DateTime transactionDateTime)
        {
            return new(toAccount, fromAccount, null, null, null, transactionAmount, null, transactionDateTime, toAccount.UserId);
        }

        internal static Transaction CreateInternalOutcome(
            Money.DB.Money transactionAmount,
            Account fromAccount,
            Account toAccount,
            DateTime transactionDateTime)
        {
            if (transactionAmount.Amount > 0)
            {
                return new(fromAccount, null, toAccount, null, null, -transactionAmount, null, transactionDateTime, fromAccount.UserId);
            }

            return new(fromAccount, null, toAccount, null, null, transactionAmount, null, transactionDateTime, fromAccount.UserId);
        }

        internal static Transaction CreatePrivateIncome(
            Money.DB.Money transactionAmount,
            Account account)
        {
            return new(account, null, null, null, null, transactionAmount, null, DateTime.UtcNow, account.UserId);
        }

        internal static Transaction CreatePrivateOutcome(
            Money.DB.Money transactionAmount,
            Account account)
        {
            if (transactionAmount.Amount > 0)
            {
                return new(account, null, null, null, null, -transactionAmount, null, DateTime.UtcNow, account.UserId);
            }

            return new(account, null, null, null, null, transactionAmount, null, DateTime.UtcNow, account.UserId);
        }

        public void SafeDelete()
        {
            RaiseDomainEvent(new TransactionDeletedDomainEvent(this));
        }
    }
}
