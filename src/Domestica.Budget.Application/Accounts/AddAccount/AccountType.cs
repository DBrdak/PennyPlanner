using System.Text.Json.Serialization;
using Domestica.Budget.Application.Converters;
using Exceptions.DB;

namespace Domestica.Budget.Application.Accounts.AddAccount
{
    [JsonConverter(typeof(AccountTypeConverter))]
    public sealed record AccountType
    {
        public string Value { get; init;  }

        private AccountType(string value) =>
            Value = value;

        public static AccountType FromString(string value) =>
            value switch
            {
                "Savings" => Savings,
                "Transactional" => Transactional,
                _ => throw new DomainException<AccountType>($"Unknown account type: {value}")
            };

        public static readonly AccountType Savings = new("Savings");
        public static readonly AccountType Transactional = new("Transactional");
    }
}
