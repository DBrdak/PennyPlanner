namespace PennyPlanner.Application.Accounts.UpdateAccount
{
    public sealed record AccountUpdateData(string AccountId, string Name, decimal Balance)
    {
    }
}
