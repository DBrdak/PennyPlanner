using System.Collections.Immutable;
using System.Transactions;
using CommonAbstractions.DB.Entities;
using Money.DB;

namespace Budgetify.Domain.Accounts
{
    public abstract class Account : Entity
    {
        public ImmutableList<Transaction> Transactions => _transactions.ToImmutableList();
        private readonly List<Transaction> _transactions;
        public AccountName Name { get; private set; }
        public Money.DB.Money Balance { get; private set; }

        protected Account(AccountName name, Currency currency) : base()
        {
            Name = name;
            Balance = new Money.DB.Money(0, currency);
            _transactions = new();
        }
    }
}
