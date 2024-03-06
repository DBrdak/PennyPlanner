using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public async Task<User?> GetByIdentityIdAsync(UserIdentityId identityId, CancellationToken cancellationToken = default, bool asNoTracking = false)
        {
            var query = DbContext.Set<User>();

            if (asNoTracking)
                query.AsNoTracking();

            return await query.FirstOrDefaultAsync(u => u.IdentityId == identityId, cancellationToken);
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
