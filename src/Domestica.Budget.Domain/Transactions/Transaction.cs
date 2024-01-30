using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Shared;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;
using Domestica.Budget.Domain.Transactions.DomainEvents;

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
        public TransactionCategory Category { get; init; }
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
            TransactionCategory category) : base(new TransactionId())
        {
            AccountId = account.Id;
            FromAccountId = fromAccount?.Id;
            ToAccountId = toAccount?.Id;
            SenderId = sender?.Id;
            RecipientId = recipient?.Id;
            TransactionAmount = transactionAmount;
            TransactionDateUtc = DateTime.UtcNow;
            Category = category;

            RaiseDomainEvent(new TransactionCreatedDomainEvent(this));
        }

        internal static Transaction CreateIncome(
            Money.DB.Money transactionAmount,
            Account toAccount,
            TransactionSender sender,
            IncomingTransactionCategory category)
        {
            return new(toAccount, null, null, sender, null, transactionAmount, category);
        }

        internal static Transaction CreateOutcome(
            Money.DB.Money transactionAmount,
            Account fromAccount,
            TransactionRecipient recipient,
            OutgoingTransactionCategory category)
        {
            if (transactionAmount.Amount > 0)
            {
                return new(fromAccount, null, null, null, recipient, -transactionAmount, category);
            }

            return new(fromAccount, null, null, null, recipient, transactionAmount, category);
        }

        internal static Transaction CreateInternalIncome(
            Money.DB.Money transactionAmount,
            Account toAccount,
            Account fromAccount)
        {
            return new(toAccount, fromAccount, null, null, null, transactionAmount, IncomingTransactionCategory.Internal);
        }

        internal static Transaction CreateInternalOutcome(
            Money.DB.Money transactionAmount,
            Account fromAccount,
            Account toAccount)
        {
            if (transactionAmount.Amount > 0)
            {
                return new(fromAccount, null, toAccount, null, null, -transactionAmount, OutgoingTransactionCategory.Internal);
            }

            return new(fromAccount, null, toAccount, null, null, transactionAmount, OutgoingTransactionCategory.Internal);
        }

        internal static Transaction CreatePrivateIncome(
            Money.DB.Money transactionAmount,
            Account account)
        {
            return new(account, null, null, null, null, transactionAmount, IncomingTransactionCategory.Private);
        }

        internal static Transaction CreatePrivateOutcome(
            Money.DB.Money transactionAmount,
            Account account)
        {
            if (transactionAmount.Amount > 0)
            {
                return new(account, null, null, null, null, -transactionAmount, OutgoingTransactionCategory.Private);
            }

            return new(account, null, null, null, null, transactionAmount, OutgoingTransactionCategory.Private);
        }
    }
}
