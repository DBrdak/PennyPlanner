using Microsoft.EntityFrameworkCore;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Domain.Accounts;

namespace PennyPlanner.Infrastructure.Repositories
{
    public sealed class AccountRepository : Repository<Account, AccountId>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext dbContext, IUserContext userContext) : base(dbContext, userContext)
        {
        }

        public async Task<List<Account>> BrowseAccounts(CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Account>()
                .Include(a => a.Transactions)
                .ThenInclude(t => t.Subcategory)
                .Include(a => a.Transactions)
                .ThenInclude(t => t.Category)
                .Where(a => a.UserId == UserId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Account?> GetAccountByIdAsync(AccountId accountId, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Account>()
                .Include(a => a.Transactions)
                .ThenInclude(t => t.Subcategory)
                .Include(a => a.Transactions)
                .ThenInclude(t => t.Category)
                .FirstOrDefaultAsync(a => a.Id == accountId && a.UserId == UserId, cancellationToken);
        }
    }
}
