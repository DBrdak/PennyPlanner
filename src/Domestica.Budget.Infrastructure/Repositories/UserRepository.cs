using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class UserRepository : Repository<User, UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
