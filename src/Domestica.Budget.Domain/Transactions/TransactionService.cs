using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;

namespace Domestica.Budget.Domain.Transactions
{
    public static class TransactionService
    {
        public static Transaction CreateIncomingTransaction(
            Money.DB.Money transactionAmount,
            Account destinationAccount,
            TransactionSender sender,
            IncomeTransactionCategory category,
            DateTime transactionDateTime)
        {
            var transaction = Transaction.CreateIncome(transactionAmount, destinationAccount, sender, category, transactionDateTime);

            destinationAccount.AddTransaction(transaction);
            sender.AddTransaction(transaction);

            return transaction;
        }

        public static Transaction CreateOutgoingTransaction(
            Money.DB.Money transactionAmount,
            Account sourceAccount,
            TransactionRecipient recipient,
            OutcomeTransactionCategory category,
            DateTime transactionDateTime)
        {
            var transaction = Transaction.CreateOutcome(transactionAmount, sourceAccount, recipient, category, transactionDateTime);

            sourceAccount.AddTransaction(transaction);
            recipient.AddTransaction(transaction);

            return transaction;
        }

        public static Transaction[] CreateInternalTransaction(
            Money.DB.Money transactionAmount,
            Account sourceAccount,
            Account destinationAccount,
            DateTime transactionDateTime) 
        {
            var transactions = new
            {
                source = Transaction.CreateInternalOutcome(transactionAmount, sourceAccount, destinationAccount, transactionDateTime),
                destination = Transaction.CreateInternalIncome(transactionAmount, destinationAccount, sourceAccount, transactionDateTime)
            };

            sourceAccount.AddTransaction(transactions.source);
            destinationAccount.AddTransaction(transactions.destination);

            return new []
            {
                transactions.source, 
                transactions.destination
            };
        }

        internal static void CreatePrivateTransaction(
            Money.DB.Money transactionAmount,
            Account account)
        {
            switch (transactionAmount.Amount > 0)
            {
                case true:
                    var equalizingIncomeTransaction = Transaction.CreatePrivateIncome(transactionAmount, account);
                    account.AddTransaction(equalizingIncomeTransaction);
                    break;
                case false:
                    var equalizingOutcomeTransaction = Transaction.CreatePrivateOutcome(transactionAmount, account);
                    account.AddTransaction(equalizingOutcomeTransaction);
                    break;
            }
        }
    }
}
