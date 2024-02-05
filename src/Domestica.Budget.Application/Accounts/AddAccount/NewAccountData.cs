namespace Domestica.Budget.Application.Accounts.AddAccount
{
    public sealed record NewAccountData(string Type, string Name, decimal InitialBalance)
    {
    }
}
