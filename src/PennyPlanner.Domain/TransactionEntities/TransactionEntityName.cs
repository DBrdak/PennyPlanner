namespace PennyPlanner.Domain.TransactionEntities
{
    public sealed record TransactionEntityName(string Value)
    {
        public override string ToString() => Value;
    }
}
