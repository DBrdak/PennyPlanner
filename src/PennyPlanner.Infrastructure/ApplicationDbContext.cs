using CommonAbstractions.DB;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PennyPlanner.Domain.Transactions;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private const int unverifiedUserLifetimeDays = 7;
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SafeDeleteTransactions();
        DeleteUnverifiedUsers();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void DeleteUnverifiedUsers()
    {
        Set<User>()
            .Where(
                u => !u.IsEmailVerified &&
                     DateTime.UtcNow - u.CreatedAt > TimeSpan.FromDays(unverifiedUserLifetimeDays))
            .ExecuteDelete();
    }

    private void SafeDeleteTransactions()
    {
        var transactions = ChangeTracker.Entries()
            .Where(
                e => e.Entity is Transaction &&
                     e.State == EntityState.Deleted)
            .Select(e => e.Entity)
            .Cast<Transaction>()
            .ToList();

        transactions.ForEach(t => t.SafeDelete());
    }
}