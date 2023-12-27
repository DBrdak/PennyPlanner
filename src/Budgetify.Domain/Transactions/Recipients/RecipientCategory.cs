namespace Budgetify.Domain.Transactions.Recipients;

public record RecipientCategory
{
    public static readonly RecipientCategory Housing = new("Housing");
    public static readonly RecipientCategory Transportation = new("Transportation");
    public static readonly RecipientCategory Food = new("Food");
    public static readonly RecipientCategory Healthcare = new("Healthcare");
    public static readonly RecipientCategory Insurance = new("Insurance");
    public static readonly RecipientCategory DebtPayments = new("Debt payments");
    public static readonly RecipientCategory Childcare = new("Childcare");
    public static readonly RecipientCategory Entertainment = new("Entertainment");
    public static readonly RecipientCategory Clothing = new("Clothing");
    public static readonly RecipientCategory Taxes = new("Taxes");
    public static readonly RecipientCategory Miscellaneous = new("Miscellaneous");
    public static readonly RecipientCategory Education = new("Education");
    public static readonly RecipientCategory Travel = new("Travel");
    public static readonly RecipientCategory Home = new("Home");
    public static readonly RecipientCategory Charity = new("Charity");

    private RecipientCategory(string value) => Value = value;

    public string Value { get; init; }

    public static RecipientCategory FromValue(string code)
    {
        return All.FirstOrDefault(c => c.Value == code) ??
               throw new ApplicationException("The category of recipient is invalid");
    }

    public static readonly IReadOnlyCollection<RecipientCategory> All = new[]
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
        Charity
    };
}