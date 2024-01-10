using System.Text.Json.Serialization;

namespace Domestica.Budget.Application.TransactionEntities.AddTransactionEntity
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
