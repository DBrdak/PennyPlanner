using System.Collections.Immutable;
using Budgetify.Domain.Transactions;
using Budgetify.Domain.Transactions.IncomingTransactions;
using Budgetify.Domain.Transactions.OugoingTransactions;
using CommonAbstractions.DB.Entities;
using Currency = Money.DB.Currency;
using Money = Money.DB.Money;
using Transaction = Budgetify.Domain.Transactions.Transaction;

#pragma warning disable CS8618

namespace Budgetify.Domain.Accounts
{
    public abstract class Account : Entity<AccountId>
    {
        public ImmutableList<Transaction> Transactions => _transactions.ToImmutableList();
        private readonly List<Transaction> _transactions;
        public AccountName Name { get; private set; }
        public global::Money.DB.Money Balance { get; private set; }

        protected Account()
        { }

        protected Account(AccountName name, Currency currency, decimal initialBalance) : base(new AccountId())
        {
            Name = name;
            Balance = new global::Money.DB.Money(initialBalance, currency);
            _transactions = new();
        }

        public void UpdateAccount(AccountName name, global::Money.DB.Money balance)
        {
            if(balance != Balance)
            {
                AdjustAccountBalance(balance);
            }

            if(name.Value != Name.Value)
            {
                ChangeAccountName(name);
            }
        }

        internal void AddIncomeTransaction(IncomingTransaction transaction)
        {
            _transactions.Add(transaction);
            Balance += transaction.TransactionAmount;
        }

        internal void AddOutgoingTransaction(OutgoingTransaction transaction)
        {
            _transactions.Add(transaction);
            Balance -= transaction.TransactionAmount;
        }
        private void ChangeAccountName(AccountName newName)
        {
            Name = newName;
        }

        private void AdjustAccountBalance(global::Money.DB.Money newBalance)
        {
            if (newBalance.Currency != Balance.Currency)
            {
                Balance = new(Balance.Amount, newBalance.Currency);
            }

            var difference = newBalance - Balance;

            TransactionService.CreateEqualizingTransaction(difference, this);
        }
    }
}
