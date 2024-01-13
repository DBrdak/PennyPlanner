﻿using Domestica.Budget.Domain.Transactions;
using Exceptions.DB;

namespace Domestica.Budget.Domain.TransactionEntities.TransactionSenders
{
    public sealed class TransactionSender : TransactionEntity
    {
        private TransactionSender()
        { }

        public TransactionSender(TransactionEntityName name) : base(name)
        {
        }
    }
}
