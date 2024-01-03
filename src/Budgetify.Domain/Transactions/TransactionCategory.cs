using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Transactions.IncomingTransactions;
using Budgetify.Domain.Transactions.OugoingTransactions;
// ReSharper disable InvokeAsExtensionMethod

namespace Budgetify.Domain.Transactions
{
    public abstract record TransactionCategory
    {
        public string Value { get; init; }

        protected TransactionCategory(string value) => Value = value;

        public static TransactionCategory FromValue(string code)
        {
            var all = IncomingTransactionCategory.All.Concat<TransactionCategory>(OutgoingTransactionCategory.All);

            return all.FirstOrDefault(c => c.Value == code) ??
                   throw new ApplicationException("The category of transaction is invalid");
        }
    }
}
