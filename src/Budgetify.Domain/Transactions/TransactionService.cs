using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using Budgetify.Domain.TransactionEntities.TransactionRecipients;
using Budgetify.Domain.TransactionEntities.TransactionSenders;
using Budgetify.Domain.Transactions.IncomingTransactions;
using Budgetify.Domain.Transactions.OugoingTransactions;

namespace Budgetify.Domain.Transactions
{
    public static class TransactionService
    {
        public static void CreateIncomingTransaction(
            Money.DB.Money transactionAmount,
            Account destinationAccount,
            TransactionSender sender,
            IncomingTransactionCategory category)
        {
            var transaction = IncomingTransaction.Create(transactionAmount, destinationAccount, sender, category);

            destinationAccount.AddIncomeTransaction(transaction);
            sender.AddTransaction(transaction);
        }

        public static void CreateOutgoingTransaction(
            Money.DB.Money transactionAmount,
            Account sourceAccount,
            TransactionRecipient recipient,
            OutgoingTransactionCategory category)
        {
            var transaction = OutgoingTransaction.Create(transactionAmount, sourceAccount, recipient, category);

            sourceAccount.AddOutgoingTransaction(transaction);
            recipient.AddTransaction(transaction);
        }

        public static void CreateInternalTransaction(
            Money.DB.Money transactionAmount,
            Account sourceAccount,
            Account destinationAccount)
        {
            var transactions = new
            {
                source = OutgoingTransaction.CreateInternal(transactionAmount, sourceAccount, destinationAccount),
                destination = IncomingTransaction.CreateInternal(transactionAmount, destinationAccount, sourceAccount)
            };

            sourceAccount.AddOutgoingTransaction(transactions.source);
            destinationAccount.AddIncomeTransaction(transactions.destination);
        }

        internal static void CreateEqualizingTransaction(
            Money.DB.Money transactionAmount,
            Account account)
        {
            switch (transactionAmount.Amount > 0)
            {
                case true:
                    var equalizingIncomeTransaction = IncomingTransaction.CreateEqualizing(transactionAmount, account);
                    account.AddIncomeTransaction(equalizingIncomeTransaction);
                    break;
                case false:
                    var equalizingOutcomeTransaction = OutgoingTransaction.CreateEqualizing(transactionAmount, account)
                    account.AddOutgoingTransaction(equalizingOutcomeTransaction);
                    break;
            }
        }
    }
}
