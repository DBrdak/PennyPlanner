﻿using Budgetify.Domain.Transactions.IncomingTransactions;
using Money.DB;
#pragma warning disable CS8618

namespace Budgetify.Domain.Accounts.TransactionalAccounts
{
    public sealed class TransactionalAccount : Account
    {
        private TransactionalAccount()
        { }

        public TransactionalAccount(AccountName name, Currency currency, decimal initialBalance = 0) : base(name, currency, initialBalance)
        {
        }
    }
}
