﻿namespace Domestica.Budget.Domain.Accounts
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account, CancellationToken cancellationToken);
        Task<List<Account>> BrowseUserAccounts(/*UserId user, */CancellationToken cancellationToken = default);
        Task<Account?> GetUserAccountByIdAsync(AccountId accountId, /*UserId userId, */ CancellationToken cancellationToken = default);
    }
}
