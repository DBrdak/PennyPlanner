using Budgetify.Domain.Transactions.Senders;

namespace Budgetify.Domain.Transactions.Senders;

public record SenderCategory
{
    public static readonly SenderCategory Employment = new("Employment");
    public static readonly SenderCategory SelfEmployment = new("Self-Employment");
    public static readonly SenderCategory Business = new("Business");
    public static readonly SenderCategory Rental = new("Rental");
    public static readonly SenderCategory Investment = new("Investment");
    public static readonly SenderCategory Pension = new("Pension");
    public static readonly SenderCategory SocialSecurity = new("Social Security");
    public static readonly SenderCategory Royalties = new("Royalties");
    public static readonly SenderCategory Alimony = new("Alimony");
    public static readonly SenderCategory Miscellaneous = new("Miscellaneous");

    private SenderCategory(string value) => Value = value;

    public string Value { get; init; }

    public static SenderCategory FromValue(string code)
    {
        return All.FirstOrDefault(c => c.Value == code) ??
               throw new ApplicationException("The category of sender is invalid");
    }

    public static readonly IReadOnlyCollection<SenderCategory> All = new[]
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
}