﻿using System.Text.Json.Serialization;
using Domestica.Budget.Application.Converters;
using Exceptions.DB;

namespace Domestica.Budget.Application.TransactionEntities.AddTransactionEntity
{
    [JsonConverter(typeof(TransactionEntityTypeConverter))]
    public sealed record TransactionEntityType
    {
        public string Value { get; init; }

        private TransactionEntityType(string value) =>
            Value = value;

        public static TransactionEntityType FromString(string value) =>
            value switch
            {
                "Sender" => Sender,
                "Recipient" => Recipient,
                _ => throw new DomainException<TransactionEntityType>($"Unknown transaction entity type: {value}")
            };

        public static readonly TransactionEntityType Sender = new("Sender");
        public static readonly TransactionEntityType Recipient = new("Recipient");
    }
}
