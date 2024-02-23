using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.Transactions;
using Domestica.Budget.Domain.TransactionSubcategories;
using Microsoft.EntityFrameworkCore;

namespace Domestica.Budget.Infrastructure.Repositories
{
    public sealed class AccountRepository : Repository<Account, AccountId>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Account>> BrowseAccounts(CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<Account>()
                .Include(a => a.Transactions)
                .ThenInclude(t => t.Subcategory)
                .Include(a => a.Transactions)
                .ThenInclude(t => t.Category)
                .Where(/*a => a.UserId == userId*/ x=> true)
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
                .FirstOrDefaultAsync( /*a => a.UserId == userId*/
                    a => a.Id == accountId,
                    cancellationToken);
        }
    }
}
