using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domestica.Budget.Application.Accounts.AddAccount;
using Domestica.Budget.Application.TransactionEntities.AddTransactionEntity;

namespace Domestica.Budget.Application.Converters
{
    public sealed class AccountTypeConverter : JsonConverter<AccountType>
    {
        public override AccountType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();

                if (stringValue == null)
                    throw new JsonException("Unable to parse TransactionEntityType");

                return AccountType.FromString(stringValue);
            }

            throw new JsonException("Unable to parse TransactionEntityType");
        }

        public override void Write(Utf8JsonWriter writer, AccountType value, JsonSerializerOptions options)
        {
            string stringValue = value.Value;
            writer.WriteStringValue(stringValue);
        }
    }
}
