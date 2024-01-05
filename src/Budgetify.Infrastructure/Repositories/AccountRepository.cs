using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Accounts;
using Microsoft.EntityFrameworkCore;

namespace Budgetify.Infrastructure.Repositories
{
    public sealed class AccountRepository : Repository<Account, AccountId>, IAccountRepository
    {
        public AccountRepository(BudgetifyContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Account>> GetUserAccounts(CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Account>()
                .Where(/*a => a.UserId == userId*/ x => true)
                .ToListAsync(cancellationToken);
        }

        public async Task<Account?> GetUserAccountByIdAsync(AccountId accountId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Account>()
                .FirstOrDefaultAsync(/*a => a.UserId == userId*/ a => a.Id == accountId, cancellationToken);
        }
    }
}
