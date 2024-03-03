using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domestica.Budget.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
