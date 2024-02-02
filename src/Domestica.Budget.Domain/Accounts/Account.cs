﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Transactions;
using Exceptions.DB;
using Money.DB;
using Currency = Money.DB.Currency;
using Money = Money.DB.Money;
using Transaction = Domestica.Budget.Domain.Transactions.Transaction;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.Accounts
{
    public abstract class Account : Entity<AccountId>
    {
        public IReadOnlyCollection<Transaction> Transactions => _transactions.OrderByDescending(t => t.TransactionDateUtc).ToList();
        private readonly List<Transaction> _transactions;
        public AccountName Name { get; private set; }
        [NotMapped]
        public global::Money.DB.Money Balance => new(_transactions.Sum(transaction => transaction.TransactionAmount.Amount), Currency);
        public Currency Currency { get; private set; }

        [JsonConstructor]
        protected Account()
        { }

        protected Account(AccountName name, Currency currency, decimal initialBalance) : base(new AccountId())
        {
            Name = name;
            Currency = currency;
            _transactions = new();

            TransactionService.CreatePrivateTransaction(new (initialBalance, currency), this);
        }

        public void UpdateAccount(AccountName name, decimal balance)
        {
            if(balance != Balance.Amount)
            {
                AdjustAccountBalance(balance);
            }

            if(name.Value != Name.Value)
            {
                ChangeAccountName(name);
            }
        }

        internal void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            //Balance += transaction.TransactionAmount;
        }

        private void ChangeAccountName(AccountName newName)
        {
            Name = newName;
        }

        private void AdjustAccountBalance(decimal newBalance)
        {
            var difference = new global::Money.DB.Money(newBalance - Balance.Amount, Balance.Currency);

            TransactionService.CreatePrivateTransaction(difference, this);
        }
    }
}
