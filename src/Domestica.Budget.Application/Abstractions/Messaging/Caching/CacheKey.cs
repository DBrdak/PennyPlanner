using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.Abstractions.Messaging.Caching
{
    public sealed record CacheKey
    {
        public string Collection { get; init; }
        public string? UserId { get; init; }

        private CacheKey(string collection, string? userId) => (Collection, UserId) = (collection, userId);

        internal static CacheKey Accounts(string? userId) => new (nameof(Account), userId);
        internal static CacheKey BudgetPlans(string? userId) => new (nameof(BudgetPlan), userId);
        internal static CacheKey TransactionEntities(string? userId) => new (nameof(TransactionEntity), userId);
        internal static CacheKey Transactions(string? userId) => new (nameof(Transaction), userId);
        internal static CacheKey TransactionCategories(string? userId) => new (nameof(TransactionCategory), userId);

        internal static IReadOnlyCollection<CacheKey> All(string? userId) =>
            new[]
            {
                Accounts(userId),
                BudgetPlans(userId),
                TransactionEntities(userId),
                Transactions(userId),
                TransactionCategories(userId)
            };

        public override string ToString() => $"{UserId}:{Collection}";

        private static void ThrowPathException(string? path) => throw new MissingMethodException($"Cannot find method for path:{path ?? ""}");
    }
}
