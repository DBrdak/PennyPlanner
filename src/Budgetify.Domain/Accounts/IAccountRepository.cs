using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Domain.Accounts
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account);
        Task<List<Account>> GetUserAccounts(/*UserId user, */CancellationToken cancellationToken = default);
        Task<Account?> GetUserAccountByIdAsync(AccountId accountId, /*UserId userId, */ CancellationToken cancellationToken = default);
    }
}
