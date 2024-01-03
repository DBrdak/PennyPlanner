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
        //Task<IEnumerable<Account>> BrowseForUserAsync(User user);
    }
}
