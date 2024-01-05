using Budgetify.Application.Accounts.AddAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Budgetify.Application.TransactionEntities.AddTransactionEntity
{
    public sealed record TransactionEntityType
    {
        public string Value { get; init; }

        [JsonConstructor]
        private TransactionEntityType(string value) =>
            Value = value;

        public static readonly TransactionEntityType Sender = new("Sender");
        public static readonly TransactionEntityType Recipient = new("Recipient");
    }
}
