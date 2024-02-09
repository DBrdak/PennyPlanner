using Exceptions.DB;
using System.Text.Json.Serialization;

namespace Domestica.Budget.Application.TransactionCategories.AddTransactionCategory
{
    public sealed record TransactionCategoryType
    {
        public string Value { get; init; }

        [JsonConstructor]
        private TransactionCategoryType(string value)
        {
            Value = value;
        }

        public static TransactionCategoryType FromString(string value) =>
            value switch
            {
                "Outcome" => Outcome,
                "Income" => Income,
                _ => throw new DomainException<TransactionCategoryType>($"Unknown account type: {value}")
            };

        public static readonly TransactionCategoryType Outcome = new("Outcome");
        public static readonly TransactionCategoryType Income = new("Income");

        public static readonly IReadOnlyCollection<TransactionCategoryType> All = new[]
        {
            Outcome,
            Income
        };
    }
}
