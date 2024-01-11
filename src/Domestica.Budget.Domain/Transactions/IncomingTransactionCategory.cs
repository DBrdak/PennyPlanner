using System.Text.Json.Serialization;

namespace Domestica.Budget.Domain.Transactions
{
    public sealed record IncomingTransactionCategory : TransactionCategory
    {
        public static readonly IncomingTransactionCategory Employment = new("Employment");
        public static readonly IncomingTransactionCategory SelfEmployment = new("Self-Employment");
        public static readonly IncomingTransactionCategory Business = new("Business");
        public static readonly IncomingTransactionCategory Rental = new("Rental");
        public static readonly IncomingTransactionCategory Investment = new("Investment");
        public static readonly IncomingTransactionCategory Pension = new("Pension");
        public static readonly IncomingTransactionCategory SocialSecurity = new("Social Security");
        public static readonly IncomingTransactionCategory Royalties = new("Royalties");
        public static readonly IncomingTransactionCategory Alimony = new("Alimony");
        public static readonly IncomingTransactionCategory Miscellaneous = new("Miscellaneous");
        public static readonly IncomingTransactionCategory Internal = new("Internal");
        public static readonly IncomingTransactionCategory Private = new("Private");

        public static readonly IReadOnlyCollection<IncomingTransactionCategory> All = new[]
        {
            Employment,
            SelfEmployment,
            Business,
            Rental,
            Investment,
            Miscellaneous,
            Pension,
            SocialSecurity,
            Royalties,
            Alimony,
            Internal,
            Private
        };
        public new static IncomingTransactionCategory FromValue(string code)
        {
            return All.FirstOrDefault(c => c.Value == code) ??
                   throw new ApplicationException("The category of transaction is invalid");
        }

        [JsonConstructor]
        private IncomingTransactionCategory(string value) : base(value)
        {
        }
    }
}
