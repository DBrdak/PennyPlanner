using CommonAbstractions.DB.Entities;
using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TEntityId>
        where TEntityId : EntityId
        where TEntity : Entity<TEntityId>
    {
        private readonly IUserContext _userContext;
        protected readonly ApplicationDbContext DbContext;
        protected readonly UserIdentityId UserId;

        protected Repository(ApplicationDbContext dbContext, IUserContext userContext)
        {
            DbContext = dbContext;
            _userContext = userContext;
            UserId = new UserIdentityId(_userContext.IdentityId);
        }
        /*
        public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default, bool asNoTracking = false)
        {
            var query = DbContext.Set<TEntity>();

            if (asNoTracking) query.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
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

        public async Task<IEnumerable<TEntity>> BrowseAllIncludeAsync<TProperty>(
            Expression<Func<TEntity, TProperty>> includeExpression, 
            CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>()
                .Include(includeExpression)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        */
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        } 
        
        public void Update(TEntity entity)
        {
            DbContext.Update(entity);
        }
    }
}
