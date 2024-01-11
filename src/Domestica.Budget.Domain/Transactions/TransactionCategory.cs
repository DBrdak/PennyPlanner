﻿

// ReSharper disable InvokeAsExtensionMethod

using System.Text.Json.Serialization;

namespace Domestica.Budget.Domain.Transactions
{
    [JsonConverter(typeof(TransactionCategoryConverter))]
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
