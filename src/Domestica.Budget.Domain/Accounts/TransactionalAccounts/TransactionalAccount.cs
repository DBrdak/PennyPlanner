﻿using Domestica.Budget.Domain.Users;
using Money.DB;

#pragma warning disable CS8618

namespace Domestica.Budget.Domain.Accounts.TransactionalAccounts
{
    public sealed class TransactionalAccount : Account
    {
        private TransactionalAccount()
        { }

        public TransactionalAccount(
            AccountName name,
            Currency currency,
            UserIdentityId userId,
            decimal initialBalance = 0) : base(
            name,
            currency,
            initialBalance,
            userId)
        {
        }
    }
}
