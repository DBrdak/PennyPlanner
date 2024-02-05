using System.Text.Json;
using System.Text.Json.Serialization;
using Exceptions.DB;

namespace Domestica.Budget.Domain.Transactions
{
    public sealed class TransactionCategoryConverter : JsonConverter<TransactionCategory>
    {
        public override TransactionCategory? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var categoryValue = JsonDocument.ParseValue(ref reader)
                .RootElement
                .GetProperty("value")
                .GetString();

            var allCategories = Enumerable.Concat<TransactionCategory>(IncomingTransactionCategory.All, OutgoingTransactionCategory.All);

            var category = allCategories.FirstOrDefault(c => c.Value == categoryValue);

            if (category is null)
            {
                throw new DomainException<TransactionCategory>($"Unknown transaction category: {categoryValue}");
            }

            return category;
        }

        public override void Write(Utf8JsonWriter writer, TransactionCategory value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("value", value.Value);
            writer.WriteEndObject();
        }
    }
}
