using CommonAbstractions.DB;
using CommonAbstractions.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Domestica.Budget.Infrastructure;

public sealed class BudgetifyContext : DbContext, IUnitOfWork
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public BudgetifyContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BudgetifyContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync(List<IDomainEvent> domainEvents, CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}