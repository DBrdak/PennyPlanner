﻿using CommonAbstractions.DB;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PennyPlanner.Domain.Transactions;

namespace PennyPlanner.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
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

        return base.SaveChangesAsync(cancellationToken);
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