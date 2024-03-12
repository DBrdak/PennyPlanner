using CommonAbstractions.DB.Entities;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Domain.Users;

namespace PennyPlanner.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TEntityId>
        where TEntityId : EntityId
        where TEntity : Entity<TEntityId>
    {
        protected readonly ApplicationDbContext DbContext;
        protected readonly UserId? UserId;

        protected Repository(ApplicationDbContext dbContext, IUserContext userContext)
        {
            DbContext = dbContext;

            var userId = userContext.TryGetIdentityId();

            UserId = userId is not null ?
                new UserId(Guid.Parse(userId)) :
                null;
        } 

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
