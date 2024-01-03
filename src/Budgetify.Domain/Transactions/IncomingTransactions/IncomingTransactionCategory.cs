using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Domain.Transactions.IncomingTransactions
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
            Alimony
        };
        public new static IncomingTransactionCategory FromValue(string code)
        {
            return All.FirstOrDefault(c => c.Value == code) ??
                   throw new ApplicationException("The category of transaction is invalid");
        }

        private IncomingTransactionCategory(string value) : base(value)
        {
        }
    }
}
