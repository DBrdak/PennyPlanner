namespace PennyPlanner.Domain.Accounts
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account, CancellationToken cancellationToken);
        Task<List<Account>> BrowseAccounts(/*UserId user, */CancellationToken cancellationToken = default);
        Task<Account?> GetAccountByIdAsync(AccountId accountId, /*UserId userId, */ CancellationToken cancellationToken = default);

        void Remove(Account account);
    }
}
