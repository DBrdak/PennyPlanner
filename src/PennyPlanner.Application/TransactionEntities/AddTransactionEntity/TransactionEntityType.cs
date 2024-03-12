using System.Text.Json.Serialization;
using Exceptions.DB;

namespace PennyPlanner.Application.TransactionEntities.AddTransactionEntity
{
    public sealed record TransactionEntityType
    {
        public string Value { get; init; }

        [JsonConstructor]
        private TransactionEntityType(string value)
        {
            Value = value;
        }

    public static TransactionEntityType FromString(string value) =>
            value.ToLower() switch
            {
                "sender" => Sender,
                "recipient" => Recipient,
                _ => throw new DomainException<TransactionEntityType>($"Unknown transaction entity type: {value}")
            };

        public static readonly TransactionEntityType Sender = new("Sender");
        public static readonly TransactionEntityType Recipient = new("Recipient");

        public static readonly IReadOnlyCollection<TransactionEntityType> All = new[]
        {
            Sender, 
            Recipient
        };
    }
}
