using Domestica.Budget.Application.Abstractions.Authentication;
using Domestica.Budget.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext, IUserContext userContext) : base(dbContext, userContext)
        {
        }


        public async Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default, bool asNoTracking = false)
        {
            var query = DbContext.Set<User>();

            if (asNoTracking)
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync(u => u.Id== userId, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(Domain.Users.Email email, CancellationToken cancellationToken = default, bool asNoTracking = false)
        {
            var query = DbContext.Set<User>();

            if (asNoTracking)
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
