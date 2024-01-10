using Domestica.Budget.Application.TransactionEntities.AddTransactionEntity;
using MongoDB.Bson.IO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using JsonTokenType = System.Text.Json.JsonTokenType;

namespace Domestica.Budget.Application.Converters
{
    public sealed class TransactionEntityTypeConverter : JsonConverter<TransactionEntityType>
    {
        public override TransactionEntityType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();

                if (stringValue == null) throw new JsonException("Unable to parse TransactionEntityType");

                return TransactionEntityType.FromString(stringValue);
            }

            throw new JsonException("Unable to parse TransactionEntityType");
        }

        public override void Write(Utf8JsonWriter writer, TransactionEntityType value, JsonSerializerOptions options)
        {
            string stringValue = value.Value;
            writer.WriteStringValue(stringValue);
        }
    }
}
