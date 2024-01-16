using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Accounts.SavingsAccounts;
using Domestica.Budget.Domain.Accounts.TransactionalAccounts;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class AccountRepository : Repository<Account, AccountId>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Account>> BrowseUserAccounts(CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Account>()
                .Include(a => a.Transactions)
                .Where(/*a => a.UserId == userId*/ x=> true)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Account?> GetUserAccountByIdAsync(AccountId accountId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Account>()
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync( /*a => a.UserId == userId*/
                    a => a.Id == accountId,
                    cancellationToken);
        }
    }
}
