﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InvokeAsExtensionMethod

namespace Budgetify.Domain.Shared.TransactionCategories
{
    public abstract record TransactionCategory
    {
        public string Value { get; init; }

        protected TransactionCategory(string value) => Value = value;

        public static TransactionCategory FromValue(string code)
        {
            var all = Enumerable.Concat<TransactionCategory>(IncomingTransactionCategory.All, OutgoingTransactionCategory.All);

            return all.FirstOrDefault(c => c.Value == code) ??
                   throw new ApplicationException("The category of transaction is invalid");
        }
    }
}
