using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualBasic.CompilerServices;
using System.IO;

namespace Domestica.Budget.API.Cache
{
    internal sealed record CacheKey
    {
        public string Collection { get; init; }
        public string? UserId { get; init; }

        private CacheKey(string collection, string? userId) => (Collection, UserId) = (collection, userId);

        internal static CacheKey Accounts(string? userId) => new CacheKey("accounts", userId);
        internal static CacheKey BudgetPlans(string? userId) => new CacheKey("budget-plans", userId);
        internal static CacheKey TransactionEntities(string? userId) => new CacheKey("transaction-entities", userId);
        internal static CacheKey Transactions(string? userId) => new CacheKey("transactions", userId);

        internal static IReadOnlyCollection<CacheKey> All(string? userId) =>
            new[]
            {
                Accounts(userId),
                BudgetPlans(userId),
                TransactionEntities(userId),
                Transactions(userId)
            };

        public override string ToString() => $"{UserId}:{Collection}";

        private static void ThrowPathException(string? path) => throw new MissingMethodException($"Cannot find method for path:{path ?? ""}");
    }
}
