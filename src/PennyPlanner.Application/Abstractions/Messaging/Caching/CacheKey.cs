using PennyPlanner.Domain.Accounts;
using PennyPlanner.Domain.BudgetPlans;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.TransactionEntities;
using PennyPlanner.Domain.Transactions;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Application.Abstractions.Messaging.Caching
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
        internal static CacheKey Users(string? userId) => new (nameof(User), userId);

        internal static CacheKey? FromString(string collection, string? userId)
        {
            return All(userId).FirstOrDefault(x => x.Collection.ToLower() == collection.ToLower());
        }

        internal static IReadOnlyCollection<CacheKey> All(string? userId) =>
            new[]
            {
                Accounts(userId),
                BudgetPlans(userId),
                TransactionEntities(userId),
                Transactions(userId),
                TransactionCategories(userId),
                Users(userId)
            };

        public override string ToString() => $"{UserId}:{Collection}";

        private static void ThrowPathException(string? path) => throw new MissingMethodException($"Cannot find method for path:{path ?? ""}");
    }
}
