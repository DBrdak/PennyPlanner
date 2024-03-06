using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domestica.Budget.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default, bool asNoTracking = false);
        Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default, bool asNoTracking = false);
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default, bool asNoTracking = false);

        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
