namespace Domestica.Budget.Domain.Accounts
{
    public sealed record AccountName(string Value)
    {
        public override string ToString() => Value;
    }
}
