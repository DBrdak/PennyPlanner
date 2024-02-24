using System.Text.Json.Serialization;

namespace Domestica.Budget.Application.Shared.Models
{
    public sealed record MoneyModel
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; }

        private MoneyModel(decimal Amount, string Currency)
        {
            this.Amount = Amount;
            this.Currency = Currency;
        }

        [JsonConstructor]
        private MoneyModel()
        {

        }

        internal static MoneyModel FromDomainObject(Money.DB.Money domainObject)
        {
            return new(domainObject.Amount, domainObject.Currency.Code);
        }

        internal Money.DB.Money ToDomainObject()
        {
            return new(Amount, Money.DB.Currency.FromCode(Currency));
        }
    }
}
