namespace Domestica.Budget.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdentityIdAsync(UserIdentityId identityId, CancellationToken cancellationToken = default, bool asNoTracking = false);
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default, bool asNoTracking = false);

        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
