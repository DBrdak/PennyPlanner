namespace Domestica.Budget.Domain.TransactionEntities
{
    public sealed record TransactionEntityName(string Value)
    {
        public override string ToString() => Value;
    }
}
