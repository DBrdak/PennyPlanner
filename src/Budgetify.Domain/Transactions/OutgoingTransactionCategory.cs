namespace Budgetify.Domain.Transactions
{
    public sealed record OutgoingTransactionCategory : TransactionCategory
    {
        public static readonly OutgoingTransactionCategory Housing = new("Housing");
        public static readonly OutgoingTransactionCategory Transportation = new("Transportation");
        public static readonly OutgoingTransactionCategory Food = new("Food");
        public static readonly OutgoingTransactionCategory Healthcare = new("Healthcare");
        public static readonly OutgoingTransactionCategory Insurance = new("Insurance");
        public static readonly OutgoingTransactionCategory DebtPayments = new("Debt payments");
        public static readonly OutgoingTransactionCategory Childcare = new("Childcare");
        public static readonly OutgoingTransactionCategory Entertainment = new("Entertainment");
        public static readonly OutgoingTransactionCategory Clothing = new("Clothing");
        public static readonly OutgoingTransactionCategory Taxes = new("Taxes");
        public static readonly OutgoingTransactionCategory Miscellaneous = new("Miscellaneous");
        public static readonly OutgoingTransactionCategory Education = new("Education");
        public static readonly OutgoingTransactionCategory Travel = new("Travel");
        public static readonly OutgoingTransactionCategory Home = new("Home");
        public static readonly OutgoingTransactionCategory Charity = new("Charity");
        public static readonly OutgoingTransactionCategory Internal = new("Internal");
        public static readonly OutgoingTransactionCategory Private = new("Private");

        public static readonly IReadOnlyCollection<OutgoingTransactionCategory> All = new[]
        {
            Housing,
            Transportation,
            Food,
            Healthcare,
            Insurance,
            DebtPayments,
            Childcare,
            Entertainment,
            Clothing,
            Taxes,
            Miscellaneous,
            Education,
            Travel,
            Home,
            Charity,
            Internal,
            Private
        };

        public new static OutgoingTransactionCategory FromValue(string code)
        {
            return All.FirstOrDefault(c => c.Value == code) ??
                   throw new ApplicationException("The category of transaction is invalid");
        }

        private OutgoingTransactionCategory(string value) : base(value)
        {
        }
    }
}
