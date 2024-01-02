using System.Collections.Immutable;
using Budgetify.Domain.Shared.TransactionCategories;
using Budgetify.Domain.Transactions;
using Budgetify.Domain.Transactions.IncomingTransactions;
using Budgetify.Domain.Transactions.OugoingTransactions;
using CommonAbstractions.DB.Entities;
using Money.DB;
#pragma warning disable CS8618

namespace Budgetify.Domain.Accounts
{
    public abstract class Account : Entity
    {
        public ImmutableList<Transaction> Transactions => _transactions.ToImmutableList();
        private readonly List<Transaction> _transactions;
        public AccountName Name { get; private set; }
        public Money.DB.Money Balance { get; private set; }

        protected Account()
        { }

        protected Account(AccountName name, Currency currency) : base()
        {
            Name = name;
            Balance = new Money.DB.Money(0, currency);
            _transactions = new();
        }

        public void ChangeAccountName(AccountName newName)
        {
            Name = newName;
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
    }
}
