﻿using System.IO.Compression;
using System.Linq.Expressions;
using CommonAbstractions.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TEntityId>
        where TEntityId : EntityId
        where TEntity : Entity<TEntityId>
    {
        protected readonly ApplicationDbContext DbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<TEntity?> GetByIdIncludeAsync<TProperty>(
            TEntityId id,
            Expression<Func<TEntity, TProperty>> includeExpression,
            CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>()
                .Include(includeExpression)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        
        public async Task<IEnumerable<TEntity>> BrowseAllAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }
    }
}
